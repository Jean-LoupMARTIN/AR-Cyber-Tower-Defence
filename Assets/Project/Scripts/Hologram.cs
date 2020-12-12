using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    public GameObject hologram;

    public MeshRenderer[] mrsBody, mrsHologram;
    public Transform[] adds;
    Vector3[] addsScales;
    public AudioSource audio_dissolve, audio_hologram, audio_add;

    bool dissolving = false, middle = false;
    float t = 0;

    void Awake()
    {
        addsScales = new Vector3[adds.Length];
        for (int i = 0; i < adds.Length; i++) {
            Transform add = adds[i];
            addsScales[i] = add.localScale;
            add.localScale = Vector3.zero;
        }
    }

    void Update()
    {
        if (dissolving)
        {
            t += Time.deltaTime;
            float progress = Tool.Progress(t, ProjectMan.inst.dissolve_t);

            foreach (MeshRenderer mr in mrsHologram)
                foreach (Material mat in mr.materials)
                    mat.SetFloat("_progress", progress);

            foreach (MeshRenderer mr in mrsBody)
                foreach (Material mat in mr.materials)
                    mat.SetFloat("_progress", progress);

            


            if (progress > 0.5) {

                if (!middle) {
                    middle = true;

                    audio_hologram.Stop();
                    if (audio_add) audio_add.Play();

                    foreach (Transform add in adds)
                        add.gameObject.SetActive(true);
                }

                float progressAdds = Tool.Progress(progress-0.5f, 0.5f);
                for (int i = 0; i < adds.Length; i++)
                {
                    Transform add = adds[i];
                    Vector3 scale = addsScales[i];
                    add.localScale = progressAdds * scale;
                }
            }



            if (progress >= 1) {

                foreach (MeshRenderer mr in mrsHologram)
                    foreach (Material mat in mr.materials)
                        mat.SetFloat("_progress", 1);

                foreach (MeshRenderer mr in mrsBody)
                    foreach (Material mat in mr.materials)
                        mat.SetFloat("_progress", 2);

                for (int i = 0; i < adds.Length; i++)
                {
                    Transform add = adds[i];
                    Vector3 scale = addsScales[i];
                    add.localScale = scale;
                }

                Destroy(hologram);
                Destroy(this);
                return;
            }
        }
    }

    public void Dissolve()
    {
        audio_dissolve.Play();
        dissolving = true;
    }
}
