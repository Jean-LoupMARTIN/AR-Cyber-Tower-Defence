

using System;
using TMPro;
using UnityEngine;

public class SightTower : Sight
{
    public static SightTower inst;

    public float view = 1;
    public TMP_Text name, totDamage, distText, stats, upButtText, sellButtText;
    [HideInInspector] public Tower target;
    public Color colorUpButtDesable;
    Color colorUpButt;


    void Awake()
    {
        colorUpButt = upButtText.color;
        inst = this;
    }

    void Update()
    {
        target = null;
        float distTarget = 1000;
        Vector3 screenPointTarget = Vector3.zero;
        Camera cam = ProjectMan.inst.cam;

        if (!WaveMan.inWave && !GrabMan.inst.tower)
        {
            foreach (Tower t in Tower.towers)
            {
                Vector3 screenPoint = cam.WorldToScreenPoint(t.gravity.position);
                float dist = Tool.Dist(cam.transform, t.gravity);
                if (screenPoint.z > 0 && dist < distTarget && dist <= view)
                {
                    float dx = screenPoint.x - Screen.width / 2;
                    float dy = screenPoint.y - Screen.height / 2;

                    if (dx * dx + dy * dy < ray)
                    {
                        target = t;
                        distTarget = dist;
                        screenPointTarget = screenPoint;
                    }
                }
            }
        }



        if (target)
        {
            screenPointTarget.z = 0;
            float sizef = 150f / distTarget;
            Vector2 size = new Vector2(sizef, sizef);

            name.text = target.name + "  " + (target.v+1) + "." + target.vsub;
            totDamage.text = "Tot Damage : " + target.totDamage;
            distText.text = Math.Round(distTarget, 2) + " m";
            stats.text = target.stats;

            upButtText.text = "[ UP:"+ target.upCost + "€ ]";
            if (target.upCost < Shop.inst.money)
                 upButtText.color = colorUpButt;
            else upButtText.color = colorUpButtDesable;

            sellButtText.text = "[ SELL:" + (int)(Tower.COEF_SELL * target.cost) + "€ ]";

            if (!display.active)
            {
                rt.sizeDelta = size;
                rt.position = screenPointTarget;
                display.SetActive(true);
            }
            else
            {
                Vector2 dsize = size - rt.sizeDelta;
                dsize *= 0.5f;
                rt.sizeDelta += dsize;

                Vector3 dp = screenPointTarget - rt.position;
                dp *= 0.5f;
                rt.position += dp;
            }
        }
        else if (display.active) display.SetActive(false);
    }


    public void UpTower() {
        if (target && Shop.inst.money >= target.upCost) target.Up();
    }

    public void SellTower() {
        if (target) {
            Shop.inst.AddMoney((int)(Tower.COEF_SELL * target.cost));
            Destroy(target.gameObject);
        }
    }
}
