using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigtheningGun : Tower
{
    public ForceField forceField;
    public AudioSource forceFieldSound;

    public float dt_shot = 1, damage = 50, ray = 1;
    protected float t_nextShot;

    public Transform firePoint;

    void Update()
    {
        if (WaveMan.inWave)
        {
            Enemy target;
            float dist;
            (target, dist) = Enemy.GetClosest(transform.position);

            if (target && dist < view)
            {
                if (Time.time > t_nextShot)
                {
                    t_nextShot = Time.time + dt_shot;
                    ForceField ff = Instantiate(forceField, firePoint.position, Quaternion.identity);
                    ff.damage = damage;
                    ff.rayMax = ray;
                    ff.tower = this;
                    forceFieldSound.Play();
                }
            }
        }
    }
}
