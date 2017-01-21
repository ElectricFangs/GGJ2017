using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : InteractableObject {

  public Transform cameraTarget;
  public Transform playerTarget;

  public override IEnumerator Interact(GameObject player) {
    player.transform.Translate(playerTarget.position - player.transform.position);
    Camera.main.transform.Translate(cameraTarget.position - Camera.main.transform.position);
    yield return null;
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
