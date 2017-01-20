using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

  private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    playerMovement = player.GetComponent<PlayerMovement>();
  }
	
	// Update is called once per frame
	void Update () {
    Vector2 velocity = new Vector2(0, 0);
    if (Input.GetKey(Constants.controlsLeft)) {
      velocity.x = -Constants.movementPlayerSpeedUnit;
    } else if (Input.GetKey(Constants.controlsRight)) {
      velocity.x = Constants.movementPlayerSpeedUnit;
    }
    if (Input.GetKey(Constants.controlsUp)) {
      velocity.y = Constants.movementPlayerSpeedUnit;
    } else if (Input.GetKey(Constants.controlsDown)) {
      velocity.y = -Constants.movementPlayerSpeedUnit;
    }
    playerMovement.SetVelocity(velocity);
  }
}
