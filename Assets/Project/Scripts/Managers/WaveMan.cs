using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveMan : MonoBehaviour
{
    public static WaveMan inst;


    public List<Enemy> enemiesPrefabs;
    public float moneyTotEnemies = 100, coefCost = 1.3f;
    float crtMoneyEnemies;
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
            if (crtMoneyEnemies != 0) {
                if (Time.time > tNextEnemy) {

                    List<Enemy> enemiesAvailable = new List<Enemy>();
                    foreach (Enemy e in enemiesPrefabs)
                        if ((e == enemiesPrefabs[0] && e.money <= crtMoneyEnemies) ||
                            (e != enemiesPrefabs[0] && e.money <= crtMoneyEnemies / 2))
                            enemiesAvailable.Add(e);

                    if (enemiesAvailable.Count == 0)
                        crtMoneyEnemies = 0;

                    else {
                        tNextEnemy += dtEnemy;
                        Enemy enemyPrefab = Tool.Rand(enemiesAvailable);
                        crtMoneyEnemies -= enemyPrefab.money;
                        SpawnEnemy(enemyPrefab);
                    }
                }
            }

            else if (Enemy.enemies.Count == 0 && crtMoneyEnemies == 0)
                EndWave();
        }
    }

    void SpawnEnemy(Enemy enemyPrefab) {
        Enemy enemy = Instantiate(enemyPrefab, Base.inst.gravity.position, Quaternion.identity);
        float oy = Tool.Rand(360);
        float ox = Random.Range(-5, 5);
        enemy.transform.Rotate(ox,oy,0);
        enemy.transform.Translate(Vector3.back * Base.inst.view);
    }

    public void StartWave()
    {
        crtMoneyEnemies = moneyTotEnemies;
        tNextEnemy = Time.time;

        inWave = true;
        startWaveEvent.Invoke();
    }

    public void EndWave()
    {
        wave++;
        moneyTotEnemies *= coefCost;
        waveInfo.text = "Wave " + wave;
        waveNumber.text = wave.ToString();
        inWave = false;
        endWaveEvent.Invoke();
    }
}
