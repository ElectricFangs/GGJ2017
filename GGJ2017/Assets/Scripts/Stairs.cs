using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : InteractableObject {

  public Transform cameraTarget;
  public Transform playerTarget;
  public float useDuration;

  public override IEnumerator Interact(GameObject player) {
    PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
    playerBehavior.isBusy = true;

    Camera.main.GetComponent<MainCamera>().InitiateFade(useDuration / 2, true);
    yield return new WaitForSeconds(useDuration / 2);

    player.transform.Translate(playerTarget.position - player.transform.position);
    Camera.main.transform.Translate(cameraTarget.position - Camera.main.transform.position);

    Camera.main.GetComponent<MainCamera>().InitiateFade(useDuration / 2, false);
    yield return new WaitForSeconds(useDuration / 2);

    playerBehavior.isBusy = false;
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
