using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarker : MonoBehaviour {

  public GameObject trackTarget;
  public SpriteRenderer spriteRenderer;
  public SpriteRenderer targetSpriteRenderer;

	// Use this for initialization
	void Start () {
    trackTarget = transform.parent.gameObject;
    targetSpriteRenderer = trackTarget.GetComponent<SpriteRenderer>();
    transform.SetParent(GameObject.FindGameObjectWithTag(Constants.tagsMinimapCamera).transform);
    spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
