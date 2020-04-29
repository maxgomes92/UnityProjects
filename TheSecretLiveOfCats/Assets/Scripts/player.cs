using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  bool isGrounded = true;
  bool isFacingRight = true;
  Animator animator;
  Rigidbody2D rigidBody;
  [SerializeField] float speed = 300f;
  [SerializeField] float jumpForce = 500f;
  [SerializeField] LayerMask groundLayers;

  void Start() {
    rigidBody = transform.GetComponent<Rigidbody2D>();
    animator = transform.GetComponent<Animator>();
  }

  void Update() {
    isGrounded = Physics2D.OverlapArea(
      new Vector2(transform.position.x - 1.5f, transform.position.y - 1.3f),
      new Vector2(transform.position.x + 1.5f, transform.position.y - 1.3f),
      groundLayers
    );

    // Update animator parameters
    Vector2 velocity = rigidBody.velocity;
    animator.SetFloat("speed", Mathf.Abs(velocity.x));

    Move();
    Jump();
  }

  void Jump() {
    if (isGrounded && Input.GetKey(KeyCode.Space)) {
      animator.SetBool("isPreJumping", true);
    } else if (isGrounded && Input.GetKeyUp(KeyCode.Space)) {
      animator.SetBool("isPreJumping", false);
      animator.SetBool("isJumping", true);
      rigidBody.AddForce(new Vector2(0f, jumpForce));
    } else if (isGrounded) {
      animator.SetBool("isJumping", false);
    }
  }

  void Move() {
    Vector2 velocity = rigidBody.velocity;

    if (animator.GetBool("isPreJumping")) {
      velocity.x = 0;
    } else if (Input.GetKey(KeyCode.RightArrow)) {
      velocity.x = speed * Time.deltaTime;

      if (!isFacingRight) {
        transform.Rotate(0, 180, 0, Space.Self);
      }

      isFacingRight = true;
    } else if (Input.GetKey(KeyCode.LeftArrow)) {
      velocity.x = -speed * Time.deltaTime;

      if (isFacingRight) {
        transform.Rotate(0, 180, 0, Space.Self);
      }

      isFacingRight = false;
    } else {
      velocity.x = 0;
    }    

    rigidBody.velocity = velocity;
  }

  //private void OnDrawGizmos() {
  //  Gizmos.color = new Color(0, 1, 0, 1);
  //  Gizmos.DrawCube(
  //    new Vector2(transform.position.x, transform.position.y - 1.3f),
  //    new Vector2(3, 0.1f)
  //  );
  //}
}
