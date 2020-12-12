

using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public static readonly int FREQ_VERSION = 4;

    public static List<Tower> towers = new List<Tower>();
    protected virtual void Awake() => towers.Add(this);
    private void OnDestroy() { towers.Remove(this); }





    public string name;
    public int cost;
    public float view = 1.5f;

    public Hologram hologram;
    public SmoothTranslate smoothTranslate;
    public Transform gravity;
    public GameObject[] versions;
    public ParticleSystem upgradeParticle;

    [HideInInspector] public int v = 0, vsub = 0;
    [HideInInspector] public float totDamage = 0;





    protected Enemy SearchEnemy()
    {
        Enemy enemy;
        float dist;
        (enemy, dist) = Enemy.GetClosest(gravity.position);
        if (enemy && dist < view) return enemy;
        else return null;
    }


    public virtual void Up()
    {
        upgradeParticle.Play();
        vsub++;
        if (v < 2 && vsub == FREQ_VERSION) {
            vsub = 0;
            versions[v].SetActive(false);
            v++;
            versions[v].SetActive(true);
        }
    }
}
