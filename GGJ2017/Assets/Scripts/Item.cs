using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableObject {
  public int value;
  public float weight;
  public float pickupTime;
  public float pickupSoundWaves;

  private float fadeStart;
  private float fadeDuration;
  private SpriteRenderer spriteRenderer;

  public override IEnumerator Interact(GameObject player) {
    PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
    Animator playerAnimator = player.GetComponent<Animator>();
    SoundWaves playerWaves = player.GetComponent<SoundWaves>();

    playerBehavior.isBusy = true;
    playerAnimator.SetBool("isUsing", true);
    FadeOut(pickupTime);
    player.GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Managers").GetComponent<SoundManager>().GetQuietLaughSound());

    yield return new WaitForSeconds(pickupTime);

    playerBehavior.carryingItems.Add(gameObject);
    playerWaves.AddWaveCount(pickupSoundWaves);
    gameObject.SetActive(false);

    playerBehavior.isBusy = false;
    playerAnimator.SetBool("isUsing", false);
  }

  private void FadeOut(float duration) {
    fadeDuration = fadeStart = duration;
  }

	// Use this for initialization
	void Start () {
    spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
    fadeDuration = Mathf.Max(-1, fadeDuration - Time.deltaTime);
    if (fadeStart > 0) {
      spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, fadeDuration / fadeStart);
    }
	}
}
