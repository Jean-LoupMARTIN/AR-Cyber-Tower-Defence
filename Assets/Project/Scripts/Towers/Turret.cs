using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : TowerShot<Bullet>
{
    public ParticleSystem[] bulletShells;


    protected override void Shot()
    {
        base.Shot();
        bulletShells[v].Play();
    }

    protected override void UpStats()
    {
        damageProj *= 1.1f;
        dtShot /= 1.1f;
        view *= 1.1f;
        animator.SetFloat("shotSpeed", 1.1f / dtShot);
    }

    protected override void UpdateStats()
    {
        stats = "Damage : " + (int)damageProj + " /shot -> " + (int)(damageProj * 1.1f) +
              "\nShot : " + Math.Round(1f / dtShot, 1) + " /s -> " + Math.Round(1.1f / dtShot, 1) +
              "\nView : " + Math.Round(view, 1) + "m -> " + Math.Round(1.1 * view, 1);
    }
}
