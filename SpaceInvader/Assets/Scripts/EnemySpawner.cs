using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;

  private int startingWave = 1;
  void Start() {
    var currentWave = waveConfigs[startingWave];
    StartCoroutine(SpawnAllEnemiesInWave(currentWave));
  }

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
    var numberOfWaves = waveConfigs.Count;

    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++) {
      var enemy = Instantiate(
        waveConfig.GetEnemyPrefab(),
        waveConfig.GetWaypoints()[0].transform.position,
        Quaternion.identity
      );

      enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
    }

    startingWave++;
    if (startingWave == numberOfWaves) {
      startingWave = 0;
      Start();
    }
  }
}
