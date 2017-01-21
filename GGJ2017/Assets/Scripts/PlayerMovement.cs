using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public bool playerMoving;
  public Vector2 lastVelocity;
  private Rigidbody2D playerRigidbody;
  private PlayerBehavior playerBehavior;

  public void SetVelocity(Vector2 velocity) {
    playerRigidbody.velocity = velocity;
    lastVelocity = velocity;
    playerMoving = velocity.magnitude > 0;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.GetComponent<InteractableObject>() != null) {
      playerBehavior.nearbyObjects.Add(collision.gameObject);
    }
  }

  void OnCollisionExit2D(Collision2D collision) {
    if (collision.gameObject.GetComponent<InteractableObject>() != null) {
      playerBehavior.nearbyObjects.Remove(collision.gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.GetComponent<InteractableObject>() != null) {
      playerBehavior.nearbyObjects.Add(other.gameObject);
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.GetComponent<InteractableObject>() != null) {
      playerBehavior.nearbyObjects.Remove(other.gameObject);
    }
  }

  public float GetPlayerSpeed() {
    if (playerBehavior.isBusy) {
      return 0;
    }

    float totalAddedWeight = 0;
    foreach (GameObject item in playerBehavior.carryingItems) {
      totalAddedWeight += item.GetComponent<Item>().weight;
    }

    return Constants.movementPlayerSpeedUnit - Constants.movementPlayerWeightFactor * totalAddedWeight;
  }

  // Use this for initialization
  void Start () {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerRigidbody.velocity = new Vector2(0, 0);
    playerBehavior = GetComponent<PlayerBehavior>();
  }
	
	// Update is called once per frame
	void Update () {
	}
}
