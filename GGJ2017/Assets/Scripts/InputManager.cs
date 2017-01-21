using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

  private PlayerMovement playerMovement;
  private PlayerBehavior playerBehavior;

	// Use this for initialization
	void Start () {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    playerMovement = player.GetComponent<PlayerMovement>();
    playerBehavior = player.GetComponent<PlayerBehavior>();
  }
	
	// Update is called once per frame
	void Update () {
    if (playerBehavior.isBusy) {
      playerMovement.SetVelocity(new Vector2(0, 0));
      return;
    }

    Vector2 velocity = new Vector2(0, 0);
    if (Input.GetKey(Constants.controlsLeft)) {
      velocity.x = -playerMovement.GetPlayerSpeed();
    } else if (Input.GetKey(Constants.controlsRight)) {
      velocity.x = playerMovement.GetPlayerSpeed();
    }
    if (Input.GetKey(Constants.controlsUp)) {
      velocity.y = playerMovement.GetPlayerSpeed();
    } else if (Input.GetKey(Constants.controlsDown)) {
      velocity.y = -playerMovement.GetPlayerSpeed();
    }
    playerMovement.SetVelocity(velocity);

    if (Input.GetMouseButtonDown(0)) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D[] hits;
      hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
      foreach (RaycastHit2D hit in hits) {
        if (hit.collider != null && playerBehavior.nearbyObjects.Contains(hit.collider.gameObject)) {
          StartCoroutine(hit.collider.gameObject.GetComponent<InteractableObject>().Interact(playerBehavior.gameObject));
          break;
        }
      }
    }
  }
}
