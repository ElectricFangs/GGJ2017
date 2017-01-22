using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public bool playerMoving;
  public Vector2 lastVelocity;

  private Rigidbody2D playerRigidbody;
  private PlayerBehavior playerBehavior;
  private AudioSource playerAudio;
  private Animator playerAnimator;
  private SpriteRenderer playerRenderer;
  private CircleCollider2D playerHandsCollider;
  private SoundManager soundManager;

  public void SetVelocity(Vector2 velocity) {
    playerRigidbody.velocity = velocity;
    lastVelocity = velocity;
    playerMoving = velocity.magnitude > 0;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.GetComponent<InteractableObject>() != null) {
      playerBehavior.nearbyObjects.Add(collision.gameObject);
    }
    SetVelocity(new Vector2(0, 0));
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

    return Mathf.Max(Constants.movementPlayerSpeedUnit - Constants.movementPlayerWeightFactor * totalAddedWeight, 0.01f);
  }

  // Use this for initialization
  void Start () {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerRigidbody.velocity = new Vector2(0, 0);
    playerBehavior = GetComponent<PlayerBehavior>();
    playerAudio = GetComponent<AudioSource>();
    playerAnimator = GetComponent<Animator>();
    playerRenderer = GetComponent<SpriteRenderer>();
    playerHandsCollider = GetComponent<CircleCollider2D>();
    soundManager = GameObject.Find("Managers").GetComponent<SoundManager>();
  }
	
	// Update is called once per frame
	void Update () {
    if (playerRigidbody.velocity.magnitude > Constants.eps) {
      playerAnimator.SetBool("isWalking", true);
      if (!playerAudio.isPlaying) {
        playerAudio.PlayOneShot(soundManager.GetLoudStepSound());
      }
      playerRenderer.flipX = playerRigidbody.velocity.x < 0;
      playerHandsCollider.offset = new Vector2((playerRenderer.flipX ? -0.08f : 0.08f), playerHandsCollider.offset.y);
    } else {
      playerAnimator.SetBool("isWalking", false);
    }
  }
}
