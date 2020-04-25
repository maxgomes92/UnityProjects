using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
  Animator animator;
  Rigidbody2D rigidBody;
  [SerializeField] float speed = 300f;

  private bool playerFacingRight = true;
  void Start() {
    rigidBody = transform.GetComponent<Rigidbody2D>();
    animator = transform.GetComponent<Animator>();
  }

  
  void Update() {
    Move();
  }

  void Move() {
    Vector2 velocity = rigidBody.velocity;
    animator.SetFloat("speed", Mathf.Abs(velocity.x));

    if (Input.GetKey(KeyCode.RightArrow)) {
      velocity.x = speed * Time.deltaTime;
      transform.localRotation = Quaternion.Euler(0, 0, 0);
    } else if (Input.GetKey(KeyCode.LeftArrow)) {
      velocity.x = -speed * Time.deltaTime;
      transform.localRotation = Quaternion.Euler(0, 180, 0);
    } else {
      velocity.x = 0;
    }

    rigidBody.velocity = velocity;
  }

  private void Flip() {
    // Switch the way the player is labelled as facing.
    playerFacingRight = !playerFacingRight;

    // Multiply the player's x local scale by -1.
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }
}
