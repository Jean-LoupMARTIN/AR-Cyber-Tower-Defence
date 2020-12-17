using System;

public class RailGun : TowerThrow
{
    protected override void UpStats()
    {
        attDamage *= 1.2f;
        view *= 1.05f;
    }

    protected override void UpdateStats()
    {
        float nextView = view;
        if (vsub == (FREQ_VERSION - 1) && v < 3) nextView += 0.2f;

        stats="Damage : " + Math.Round(attDamage, 2) + " -> " + Math.Round(attDamage * 1.2, 2) +
            "\nView : " + Math.Round(view, 1) + "m -> " + Math.Round(view * 1.05, 1);
    }
}
