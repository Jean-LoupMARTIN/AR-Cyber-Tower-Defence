using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : TowerShot<Rocket>
{
    int iFirePoint = 0;
    public float rocketView = 0.5f;

    protected override void Shot()
    {
        Transform fp = firePoint.GetChild(iFirePoint);
        iFirePoint++;
        iFirePoint %= firePoint.childCount;

        Rocket r = Instantiate(projectil, fp.position, fp.rotation);
        r.damage = damageProj;
        r.tower = this;
        r.view = rocketView;
        animator.SetTrigger("shot");

        ParticleSystem sp = Instantiate(shotParticle, fp.position, fp.rotation);
        Destroy(sp.gameObject, 1);
    }


    protected override void UpStats()
    {
        damageProj *= 1.1f;
        dtShot /= 1.1f;
        rocketView *= 1.1f;
        animator.SetFloat("shotSpeed", 1.1f / dtShot);
    }

    protected override void UpdateStats()
    {
        stats = "Damage : " + (int)damageProj + " /shot -> " + (int)(damageProj * 1.1f) +
              "\nShot : " + Math.Round(1f / dtShot, 1) + " /s -> " + Math.Round(1.1f / dtShot, 1) +
              "\nRocket View : " + Math.Round(rocketView, 1) + "m -> " + Math.Round(1.1 * rocketView, 1);
    }
}
