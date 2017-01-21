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
  private SpriteRenderer renderer;

  public override IEnumerator Interact(GameObject player) {
    PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
    SoundWaves playerWaves = player.GetComponent<SoundWaves>();

    playerBehavior.isBusy = true;
    FadeOut(pickupTime);

    yield return new WaitForSeconds(pickupTime);

    playerBehavior.carryingItems.Add(gameObject);
    playerWaves.AddWaveCount(pickupSoundWaves);
    gameObject.SetActive(false);

    playerBehavior.isBusy = false;
  }

  private void FadeOut(float duration) {
    fadeDuration = fadeStart = duration;
  }

	// Use this for initialization
	void Start () {
    renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
    fadeDuration = Mathf.Max(-1, fadeDuration - Time.deltaTime);
    if (fadeStart > 0) {
      renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, fadeDuration / fadeStart);
    }
	}
}
