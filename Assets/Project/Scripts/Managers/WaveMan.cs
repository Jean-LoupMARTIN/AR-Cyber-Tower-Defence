using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveMan : MonoBehaviour
{
    public static WaveMan inst;


    public List<Enemy> enemiesPrefabs;
    int countEnemy;
    public float dtEnemy = 0.3f;
    float tNextEnemy;

    public TMP_Text waveInfo, waveNumber;
    public UnityEvent startWaveEvent, endWaveEvent;
    public static bool inWave = false;
    int wave = 1;

    void Awake()
    {
        inst = this;
    }

    

    void Update()
    {
        if (inWave) {
            if (countEnemy > 0) {
                if (Time.time > tNextEnemy) {
                    tNextEnemy += dtEnemy;
                    countEnemy--;
                    SpawnEnemy();
                }
            }
            else if (Enemy.enemies.Count == 0)
                EndWave();
        }
    }

    void SpawnEnemy() {
        Enemy prefab = Tool.Rand(enemiesPrefabs);
        Enemy enemy = Instantiate(prefab, Base.inst.gravity.position, Quaternion.identity);
        float oy = Tool.Rand(360);
        float ox = Random.Range(-5, 5);
        enemy.transform.Rotate(ox,oy,0);
        enemy.transform.Translate(Vector3.back * Base.inst.view);
    }

    public void StartWave()
    {
        countEnemy =  2 * wave;
        tNextEnemy = Time.time;

        inWave = true;
        startWaveEvent.Invoke();
    }

    public void EndWave()
    {
        wave++;
        waveInfo.text = "Wave " + wave;
        waveNumber.text = wave.ToString();
        inWave = false;
        endWaveEvent.Invoke();
    }
}
