using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  BoxCollider2D boxCollider2d;
  
  [SerializeField] LayerMask platformsLayerMask;
  [SerializeField] Rigidbody2D rigidBody;
  [SerializeField] Animator animator;
  [SerializeField] float speed = 5;
  [SerializeField] float thrust = 8.0f;

  void Start()
  {
    rigidBody = transform.GetComponent<Rigidbody2D>();
    boxCollider2d = transform.GetComponent<BoxCollider2D>();
  }

  void Update()
  {
    Move();
    Animate();
  }

  void Animate() {
    if (rigidBody.velocity.y > 0.2) {
      animator.SetBool("rising", true);
      animator.SetBool("falling", false);
      animator.SetBool("walking", false);
    } else if (rigidBody.velocity.y < -0.2) {
      animator.SetBool("rising", false);
      animator.SetBool("falling", true);
      animator.SetBool("walking", false);
    } else {
      animator.SetBool("rising", false);
      animator.SetBool("falling", false);

      if (Mathf.Abs(rigidBody.velocity.x) < 0.1) {
        animator.SetBool("walking", false);
      } else {
        animator.SetBool("walking", true);
      }
    }
  }

  void Move() {
    var velocity = rigidBody.velocity;

    if (Input.GetKey(KeyCode.RightArrow)) {
      velocity.x = speed;
      transform.localRotation = Quaternion.Euler(0, 0, 0);
    } else if (Input.GetKey(KeyCode.LeftArrow)) {
      velocity.x = -speed;
      transform.localRotation = Quaternion.Euler(0, 180, 0);
    } else {
      velocity.x = 0;
    }

    if (isGrounded() && Input.GetKeyDown(KeyCode.Space)) {
      velocity.y = thrust;
    }

    rigidBody.velocity = velocity;
  }

  private bool isGrounded () {
    RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
    return raycastHit2d.collider != null;
  }
}
