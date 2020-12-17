using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigtheningGun : Tower
{
    public ForceField forceField;
    public AudioSource forceFieldSound;

    public float dt_shot = 1, damage = 50, ray = 0.5f;
    protected float t_nextShot;

    public Transform firePoint;

    void Update()
    {
        if (WaveMan.inWave)
        {
            Enemy target;
            float dist;
            (target, dist) = Enemy.GetClosest(transform.position);

            if (target && dist < ray)
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


    protected override void UpStats()
    {
        damage *= 1.08f;
        dt_shot /= 1.06f;
        ray *= 1.04f;
    }

    protected override void UpdateStats()
    {
        stats = "Damage : " + ((int)damage) + " -> " + ((int)(damage * 1.08)) +
              "\nFreq : " + Math.Round(1f/dt_shot, 1) + " /s -> " + Math.Round(1.06f/dt_shot, 1) +
              "\nRay : " + Math.Round(ray, 1) + "m -> " + Math.Round(ray*1.04, 1);
    }
}
