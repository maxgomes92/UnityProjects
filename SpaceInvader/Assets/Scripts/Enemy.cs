﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float health = 100;

  void Update() {
        
  }

  private void OnTriggerEnter2D(Collider2D other) {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer) {
    health -= damageDealer.GetDamage();

    if (health <= 0) {
      Destroy(gameObject);
    }
  }
}
