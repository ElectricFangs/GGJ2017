using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  private Rigidbody2D playerRigidbody;
  private PlayerBehavior playerBehavior;

  public void SetVelocity(Vector2 velocity) {
    playerRigidbody.velocity = velocity;
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
