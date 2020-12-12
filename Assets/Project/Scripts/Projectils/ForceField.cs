﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float damage = 10, life = 1f, rayMax = 1;
    float crtLife = 0;
    public AnimationCurve rayCurve;

    Material mat;
    public Color color;
    public float startIntensity = 5, endIntensity = 0;

    [HideInInspector] public Tower tower;


    void Awake()
    {
        transform.localScale = Vector3.zero;

        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        float progress = Tool.Progress(crtLife, life);

        float ray = rayCurve.Evaluate(progress);
        transform.localScale = Vector3.one * ray;

        progress = Tool.Progress(ray, rayMax);
        float intensity = endIntensity + (startIntensity - endIntensity) * (1 - progress);
        float factor = Mathf.Pow(2, intensity);
        Color newColor = color * factor;
        mat.SetColor("_emission", newColor);
        
        crtLife += Time.deltaTime;
        if (crtLife > life) Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        Transform parent = other.transform.parent;
        if (parent) {
            Enemy enemy = parent.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(damage, tower);
        }
    }
}
