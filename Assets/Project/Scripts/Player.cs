using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player inst;

    public float view = 3;
    public SightEnemy sight;

    public float tShot = 0.25f;
    public Bullet bulletPrefab;
    bool shotLeft = true;
    float tNextShot = 0;

    public float t_overload = 5;
    public float overloadShot = 0.2f;
    float overload = 0;
    bool reload = false;
    public RawImage overloadBar;
    public TMP_Text overloadPercent;
    public Gradient overloadGradient;

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateOverload();

        if (WaveMan.inWave) {
            if (Input.GetMouseButton(0) && !reload && Time.time > tNextShot) {
                tNextShot = Time.time + tShot;
                overload += overloadShot;
                if (overload >= t_overload)
                    reload = true;
                Shot();
            }
        }
    }


    void UpdateOverload() {
        overload = Mathf.Max(overload - Time.deltaTime, 0);
        if (overload == 0) reload = false;

        float progress = Tool.Progress(overload, t_overload);
        progress = Mathf.Clamp(progress, 0, 1);
        overloadPercent.text = (int)(progress * 100)+"%";
        progress = Mathf.Max(progress, 0.01f);
        Vector3 scale = overloadBar.rectTransform.localScale;
        scale.x = progress;
        overloadBar.rectTransform.localScale = scale;
        if (reload) overloadBar.color = overloadGradient.Evaluate(1);
        else overloadBar.color = overloadGradient.Evaluate(progress);
    }

    void Shot()
    {
        Transform camTrans = ProjectMan.inst.cam.transform;
        float right = 0.03f;
        if (shotLeft) right *= -1;
        shotLeft = !shotLeft;

        Vector3 pos = camTrans.position + right * camTrans.right - 0.015f * camTrans.up;
        Quaternion rot;
        if (sight.target != null)
        {
            Enemy enemy = sight.target;
            Vector3 target = enemy.gravity.position;
            target += enemy.transform.forward * enemy.speed * Tool.Dist(pos, enemy) * 0.28f;
            rot = Quaternion.LookRotation(Tool.Dir(pos, target, false));
        }
        else rot = camTrans.rotation;
        Bullet bullet = Instantiate(bulletPrefab, pos, rot);
        AudioSource audio = bullet.GetComponent<AudioSource>();
        audio.pitch = 1 + 0.6f * Tool.Progress(overload, t_overload);
    }
}
