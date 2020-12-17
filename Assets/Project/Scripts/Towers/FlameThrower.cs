using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : TowerThrow
{
    protected override void UpStats()
    {
        attDamage *= 1.15f;
        if (vsub == 0) {
            view += 0.2f;
            anticipation -= 0.2f;
        }
    }

    protected override void UpdateStats()
    {
        float nextView = view;
        if (vsub == (FREQ_VERSION - 1) && v < 2) nextView += 0.2f;

        stats = "Damage : " + Math.Round(attDamage, 2) + " -> " + Math.Round(attDamage * 1.15, 2) +
              "\nRange : " + view + "m -> " + nextView;
    }
}
