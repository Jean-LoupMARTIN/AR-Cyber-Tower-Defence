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
}
