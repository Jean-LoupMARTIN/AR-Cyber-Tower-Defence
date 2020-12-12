using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base inst;
    public SmoothTranslate smoothTranslate;
    public Hologram hologram;
    public Transform gravity;
    public RectTransform lifeRT;
    public TMP_Text lifePercent;
    public Animator bloodyScreen;

    public float view = 2;
    public int life;
    float crtLife;

    void Awake()
    {
        crtLife = life;
        inst = this;
    }


    public void TakeDamage(float damage)
    {
        crtLife = Mathf.Max(0, crtLife - damage);
        float progress = Tool.Progress(crtLife, life);
        lifeRT.localScale = new Vector3(progress, 1, 1);
        lifePercent.text = (int)(progress * 100) + "%";
        bloodyScreen.Play("Damage");
    }
}
