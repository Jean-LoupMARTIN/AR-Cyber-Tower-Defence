using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    public static Base inst;
    public SmoothTranslate smoothTranslate;
    public Hologram hologram;
    public Transform gravity;
    public RectTransform lifeRT;
    public TMP_Text lifePercent, scoreDead;
    public Animator bloodyScreen;
    public GameObject exploPrefab, scale;

    public UnityEvent deadEvent;

    public float view = 2;
    public int life;
    float crtLife;
    [HideInInspector] public bool died = false;

    void Awake()
    {
        crtLife = life;
        inst = this;
    }


    public void TakeDamage(float damage)
    {
        if (!died)
        {
            crtLife = Mathf.Max(0, crtLife - damage);
            float progress = Tool.Progress(crtLife, life);
            lifeRT.localScale = new Vector3(progress, 1, 1);
            lifePercent.text = (int)(progress * 100) + "%";
            bloodyScreen.Play("Damage");
            if (crtLife <= 0) Die();
        }
    }


    void Die() {
        died = true;
        StartCoroutine(Explose());

        //Decompose(scale.transform);
        //Destroy(scale);

        scoreDead.text = "Score : " + WaveMan.inst.wave;
        deadEvent.Invoke();
    }

    void Decompose(Transform trans) {
        Rigidbody rg = trans.GetComponent<Rigidbody>();
        if (rg)
        {
            rg.transform.parent = scale.transform.parent;
            rg.useGravity = true;
            rg.isKinematic = false;
        }
        foreach (Transform child in trans)
            Decompose(child);
    }

    IEnumerator Explose(int nbExplo = 10)
    {
        if (nbExplo > 0) {
            Destroy(Instantiate(
                exploPrefab,
                gravity.position + 0.05f * new Vector3(Random.RandomRange(-1, 1), Random.RandomRange(-1, 1), Random.RandomRange(-1, 1)),
                Quaternion.identity), 1);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
            StartCoroutine(Explose(nbExplo-1));
        }
    }
}
