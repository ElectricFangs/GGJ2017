using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  private Rigidbody2D playerRigidbody;

  public void SetVelocity(Vector2 velocity) {
    playerRigidbody.velocity = velocity;
  }

    // Use this for initialization
  void Start () {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerRigidbody.velocity = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
