﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerLook : Tower
{
    public float rotSpeed = 5;
    public float viewAngle = 5f;

    public Transform[] rotsY, rotsX, firePoints;
    protected Transform rotY, rotX, firePoint;

    Enemy target;





    protected override void Awake() {
        base.Awake();
        UpdateRot();
    }



    protected virtual void Update()
    {
        target = null;
        Vector3 lookPoint = transform.position + 10 * transform.forward;

        if (WaveMan.inWave) {
            target = SearchEnemy();
            if (target) { lookPoint = target.gravity.position; }
        }

        else if (!hologram && GrabMan.inst.tower != this) {
            float distPlayer = Tool.Dist(gravity, ProjectMan.inst.cam.transform);
            if (distPlayer < 0.8f && distPlayer > 0.2f)
                lookPoint = ProjectMan.inst.cam.transform.position;
        }

        Look(lookPoint);



        if (target && Vector3.Angle(firePoint.forward, Tool.Dir(firePoint, lookPoint)) < viewAngle)
             Attack();
        else NoAttack();
    }


    protected abstract void Attack();
    protected virtual void NoAttack() {}


    public override void Up()
    {
        base.Up();
        if (vsub == 0) UpdateRot();
    }

    void UpdateRot()
    {
        rotY = rotsY[v];
        rotX = rotsX[v];
        firePoint = firePoints[v];
    }






    void Look(Vector3 target)
    {
        // rotY
        Vector3 targetProjY = target;
        targetProjY.y = rotY.position.y;
        Vector3 dirY = Tool.Dir(rotY, targetProjY);
        float angleY = Vector3.Angle(dirY, rotY.forward);

        rotY.rotation = Quaternion.RotateTowards(
            rotY.rotation,
            Quaternion.LookRotation(dirY),
            rotSpeed * (angleY + 15) * Time.deltaTime);


        // rotX
        float distProjY = dirY.magnitude;
        float height = target.y - rotY.position.y;
        Vector3 targetProjRotY = rotY.position + rotY.forward * distProjY + Vector3.up * height;
        targetProjRotY += rotX.position - firePoint.position;
        Vector3 dirX = Tool.Dir(rotX, targetProjRotY);
        float angleX = Vector3.Angle(dirX, rotX.forward);

        rotX.rotation = Quaternion.RotateTowards(
            rotX.rotation,
            Quaternion.LookRotation(dirX),
            rotSpeed * (angleX + 15) * Time.deltaTime);
    }
}
