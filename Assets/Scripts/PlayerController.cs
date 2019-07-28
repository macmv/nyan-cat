using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

  public FartController fart;
  private int animationFrame;
  private bool dir = true;
  private bool isGrounded = false;
  private Rigidbody2D rb;
  private List<GameObject> currentCollisions = new List<GameObject>();
  private int boost = 100;

  public void Awake() {
    rb = GetComponent<Rigidbody2D>();
  }

  public void Update() {
    var pos = this.transform.position;
    int fartDir;
    if (dir) {fartDir = 1;} else {fartDir = -1;}
    fart.transform.position = new Vector3(pos.x - (fartDir * animationFrame % (fart.colorWidth * 2) / 20f), pos.y, pos.z);
    animationFrame += 2;

    fart.spriteRender.flipX = dir;
    GetComponent<SpriteRenderer>().flipX = !dir;

    Debug.Log(1.0f / Time.deltaTime);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    Vector2 normal = col.contacts[0].normal;
    var angle = Vector2.Angle(new Vector2(1, 0), normal);
    if (angle < 110 && angle > 70) {
      isGrounded = true;
    }
    currentCollisions.Add(col.gameObject);
  }

  private void OnCollisionExit2D(Collision2D col) {
    currentCollisions.Remove(col.gameObject);
    if (currentCollisions.Count == 0) {
      isGrounded = false;
    }
  }

  public void FixedUpdate() {
    var pos = this.transform.position;
    float speed = 1000;
    bool oldDir = dir;
    if (Input.GetKey("a")) {
      rb.AddForce(new Vector2(-speed * Time.deltaTime, 0), ForceMode2D.Force);
      dir = false;
    }
    if (Input.GetKey("d")) {
      rb.AddForce(new Vector2(speed * Time.deltaTime, 0), ForceMode2D.Force);
      dir = true;
    }
    if (Input.GetKey("w") && isGrounded) {
      rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
      isGrounded = false;
    }
    if (isGrounded) {
      boost += 5;
    }
    if (boost > 100) {
      boost = 100;
    }
    if (Input.GetKey("space") && boost > 0) {
      rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
      boost -= 1;
      showFart();
    } else {
      rb.constraints = RigidbodyConstraints2D.FreezeRotation;
      hideFart();
    }
  }

  private void showFart() {
    fart.show();
  }

  private void hideFart() {
    fart.hide();
  }

  public int getBoost() {
    return boost;
  }
}
