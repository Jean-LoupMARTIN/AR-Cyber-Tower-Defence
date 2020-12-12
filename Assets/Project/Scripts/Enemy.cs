using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static HashSet<Enemy> enemies = new HashSet<Enemy>();

    public string name;
    public int life;
    [HideInInspector] public float crtLife;
    public float speed;
    public int money;

    public Transform gravity;

    public MeshRenderer mr;
    bool dissolve = true;
    float t_dissolve = 0;

    private void Awake()
    {
        crtLife = life;
        enemies.Add(this);
    }

    private void Update()
    {
        if (dissolve) Dissolve();


        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (Tool.Dist(transform, Base.inst.gravity) < 0.1) {
            Base.inst.TakeDamage(crtLife);
            Die();
        }
    }

    private void Dissolve()
    {
        t_dissolve += Time.deltaTime;
        float progress = Tool.Progress(t_dissolve, ProjectMan.inst.dissolve_t);

        if (progress >= 1)
        {
            mr.material.SetFloat("_progress", 2);
            dissolve = false;
        }
        else mr.material.SetFloat("_progress", progress);

    }

    public void TakeDamage(float damage, Tower tower) {
        if(tower) tower.totDamage += damage;

        crtLife -= damage;
        if (crtLife <= 0)
            Die();
    }

    void Die() {
        Shop.inst.AddMoney(money);
        enemies.Remove(this);
        Destroy(gameObject);
    }

    public static (Enemy enemy, float dist) GetClosest(Vector3 target) {
        Enemy output = null;
        float dist = 1000;
        foreach (Enemy e in enemies) {
            float crtDist = Tool.Dist(e.gravity, target);
            if (crtDist < dist) {
                dist = crtDist;
                output = e;
            }
        }
        return (output, dist);
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "TowerThrow AttParticle") {
            Transform trans = other.transform;
            while (trans != null) {
                trans = trans.parent;
                TowerThrow towerThrow = trans.GetComponent<TowerThrow>();
                if (towerThrow) {
                    TakeDamage(towerThrow.attDamage, towerThrow);
                    return;
                }
            }
        }
    }
}
