﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] float padding = 1f;
  [SerializeField] GameObject laserPrefab;
  [SerializeField] float laserSpeed = 10f;
  [SerializeField] float laserFiringPeriod = 0.1f;

  private float xMin, xMax, yMin, yMax;
  private Coroutine firingCoroutine;
  
  // Start is called before the first frame update
  void Start() {
    SetUpMoveBoundaries();
  }

  // Update is called once per frame
  void Update() {
    Move();
    Fire();
  }

  IEnumerator FireContinuously()
  {
    while (true)
    {
      GameObject laser = Instantiate(
        laserPrefab,
        transform.position,
        Quaternion.identity
      ) as GameObject;

      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

      yield return new WaitForSeconds(laserFiringPeriod);
    }
  }

  private void Fire() {
    if (Input.GetButtonDown("Fire1")) {
      firingCoroutine = StartCoroutine(FireContinuously());
    }

    if (Input.GetButtonUp("Fire1")) {
      StopCoroutine(firingCoroutine);
    }
  }

  private void Move() {
    var position = transform.position;

    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

    var newXPos = Mathf.Clamp(position.x + deltaX, xMin, xMax);
    var newYPos = Mathf.Clamp(position.y + deltaY, yMin, yMax);

    transform.position = new Vector2(newXPos, newYPos);
  }

  private void SetUpMoveBoundaries() {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
  }
}
