using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

  public bool keyboardMovementEnabled = false;
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

    if (keyboardMovementEnabled) {
      Vector2 velocity = new Vector2(0, 0);
      float currentSpeed = playerMovement.GetPlayerSpeed();
      if (Input.GetKey(Constants.controlsLeft)) {
        velocity.x = -currentSpeed;
      }
      else if (Input.GetKey(Constants.controlsRight)) {
        velocity.x = currentSpeed;
      }
      if (Input.GetKey(Constants.controlsUp)) {
        velocity.y = currentSpeed;
      }
      else if (Input.GetKey(Constants.controlsDown)) {
        velocity.y = -currentSpeed;
      }
      if (Mathf.Abs(velocity.magnitude - currentSpeed) > Constants.eps) {
        velocity /= 2;
      }
      playerMovement.SetVelocity(velocity);
    }

    if (Input.GetMouseButtonUp(1)) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D[] hits;
      hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
      GameObject topClickedObject = null;
      int topClickedOrder = -1000;
      foreach (RaycastHit2D hit in hits) {
        if (hit.collider != null && playerBehavior.nearbyObjects.Contains(hit.collider.gameObject) &&
          (topClickedObject == null || hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder > topClickedOrder)) {
          topClickedObject = hit.collider.gameObject;
          topClickedOrder = topClickedObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
      }
      if (topClickedObject != null) {
        StartCoroutine(topClickedObject.GetComponent<InteractableObject>().Interact(playerBehavior.gameObject));
      }
    }

    if (!keyboardMovementEnabled) {
      if (Input.GetMouseButton(0)) {
        Vector3 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - playerMovement.transform.position;
        if (direction.magnitude > Constants.eps) {
          float currentSpeed = playerMovement.GetPlayerSpeed();
          playerMovement.SetVelocity(new Vector2(direction.normalized.x * currentSpeed, direction.normalized.y * currentSpeed));
        } else {
          playerMovement.SetVelocity(new Vector2(0, 0));
        }
        if (direction.x > 0) {
          playerMovement.SetFlipX(false);
        } else if (direction.x < 0) {
          playerMovement.SetFlipX(true);
        }
      } else {
        playerMovement.SetVelocity(new Vector2(0, 0));
      }
    }
  }
}
