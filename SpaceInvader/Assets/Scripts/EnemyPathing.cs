using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
  WaveConfig waveConfig;
  List<Transform> waypoints;
  private int targetWaypointIndex = 0;

  void Start() {
    waypoints = waveConfig.GetWaypoints();
    transform.position = waypoints[targetWaypointIndex].transform.position;
  }

  void Update() {
    Move();
  }

  public void SetWaveConfig(WaveConfig waveConfig) {
    this.waveConfig = waveConfig;
  }

  private void Move() {
    if (targetWaypointIndex < waypoints.Count) {
      var step = waveConfig.GetMoveSpeed() * Time.deltaTime;
      var target = waypoints[targetWaypointIndex];


      var destiny = Vector2.MoveTowards(
        transform.position,
        target.position,
        step
      );

      transform.position = destiny;

      if (Equals(transform.position, target.position)) {
        targetWaypointIndex++;
      }
    } else {
      Destroy(gameObject);
    }
  }
}
