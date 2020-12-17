

using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public static readonly float COEF_UP = 0.3f;
    public static readonly float COEF_SELL = .9f;

    public static readonly int FREQ_VERSION = 4;

    public static List<Tower> towers = new List<Tower>();
    private void OnDestroy() { towers.Remove(this); }



    public string name;
    public int cost;

    public Hologram hologram;
    public SmoothTranslate smoothTranslate;
    public Transform gravity;
    public GameObject[] versions;
    public ParticleSystem upgradeParticle;

    [HideInInspector] public int v = 0, vsub = 0, upCost;
    [HideInInspector] public float totDamage = 0;
    [HideInInspector] public string stats;


    protected virtual void Awake() {
        UpdateStats();
        upCost = (int)(COEF_UP * cost);
        towers.Add(this);
    }






    public virtual void Up()
    {
        vsub++;
        if (v < 2 && vsub == FREQ_VERSION) {
            vsub = 0;
            versions[v].SetActive(false);
            v++;
            versions[v].SetActive(true);
        }

        Shop.inst.AddMoney(-upCost);
        cost += upCost;
        upCost = (int)(COEF_UP * cost);

        UpStats();
        UpdateStats();
        upgradeParticle.Play();
    }

    protected abstract void UpStats();
    protected abstract void UpdateStats();
}
