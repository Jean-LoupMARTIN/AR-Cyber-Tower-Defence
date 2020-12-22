

using System;
using TMPro;
using UnityEngine;

public class SightEnemy : Sight
{
    public Transform lifeBar;
    public TMP_Text percentText, distText, nameText;
    [HideInInspector] public Enemy target;




    void Update()
    {
        target = null;
        float distTarget = 1000;
        Vector3 screenPointTarget = Vector3.zero;
        Camera cam = ProjectMan.inst.cam;

        if (WaveMan.inWave && !Base.inst.died) {
            foreach (Enemy e in Enemy.enemies)
            {
                Vector3 screenPoint = cam.WorldToScreenPoint(e.gravity.position);
                float dist = Tool.Dist(cam.transform, e.gravity);

                if (screenPoint.z > 0 && dist < distTarget && dist <= Player.inst.view)
                {
                    float dx = screenPoint.x - Screen.width / 2;
                    float dy = screenPoint.y - Screen.height / 2;

                    if (dx * dx + dy * dy < ray)
                    {
                        target = e;
                        distTarget = dist;
                        screenPointTarget = screenPoint;
                    }
                }
            }
        }



        if (target) {

            screenPointTarget.z = 0;
            float sizef = 130f / distTarget;
            Vector2 size = new Vector2(sizef, sizef);

            distText.text = Math.Round(distTarget, 2) + " m";
            Vector3 scale = lifeBar.localScale;
            float progress = Tool.Progress(target.crtLife, target.life);
            scale.x = progress;
            lifeBar.localScale = scale;
            percentText.text = (int)(progress * 100) + "%";
            nameText.text = target.name;

            if (!display.active)
            {
                rt.sizeDelta = size;
                rt.position = screenPointTarget;
                display.SetActive(true);
            }
            else {
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
}
