using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : TowerShot<Rocket>
{
    int iFirePoint = 0;

    protected override void Shot()
    {
        Transform fp = firePoint.GetChild(iFirePoint);
        iFirePoint++;
        iFirePoint %= firePoint.childCount;

        Projectil p = Instantiate(projectil, fp.position, fp.rotation);
        p.damage = damageProj;
        p.tower = this;
        animator.SetTrigger("shot");

        ParticleSystem sp = Instantiate(shotParticle, fp.position, fp.rotation);
        Destroy(sp.gameObject, 1);
    }
}
