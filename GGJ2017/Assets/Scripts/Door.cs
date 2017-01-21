using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject {

  public float scoreLootDuration;

  public override IEnumerator Interact(GameObject player) {
    PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
    playerBehavior.isBusy = true;
    yield return new WaitForSeconds(scoreLootDuration);
    playerBehavior.ScoreLoot();
    playerBehavior.isBusy = false;
  }

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
