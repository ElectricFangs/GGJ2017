using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableObject {
  public int value;
  public float weight;
  public float pickupTime;
  public float pickupSoundWaves;

  public override IEnumerator Interact(GameObject player) {
    PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
    SoundWaves playerWaves = player.GetComponent<SoundWaves>();

    playerBehavior.isBusy = true;
    
    yield return new WaitForSeconds(pickupTime);

    playerBehavior.carryingItems.Add(gameObject);
    playerWaves.AddWaveCount(pickupSoundWaves);
    gameObject.SetActive(false);

    playerBehavior.isBusy = false;
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
