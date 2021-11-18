using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++) 
        { 
        var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        
       // for (int waveIndex = startingWave; waveIndex< waveConfigs.Count; waveIndex++)
       while(true)
        {
            int waveIndex = Random.Range(0, waveConfigs.Count - 1);
            var currentWave = waveConfigs[waveIndex];
            StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            yield return new WaitForSeconds(currentWave.GetTimeTillNextWave());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
