

using System;
using UnityEngine;

public abstract class Sight : MonoBehaviour
{
    public GameObject display;
    protected RectTransform rt;
    protected float ray;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        ray = 300f / 1600 * Screen.width;
        ray *= ray;
    }

}
