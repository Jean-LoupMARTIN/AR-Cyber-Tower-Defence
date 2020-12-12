using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandSound : MonoBehaviour
{
    public List<AudioSource> sounds;

    private void Awake()
    {
        foreach (AudioSource s in sounds) { s.enabled = false; }
        Tool.Rand(sounds).enabled = true;
    }
}
