using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarker : MonoBehaviour {

  public GameObject trackTarget;

	// Use this for initialization
	void Start () {
    trackTarget = transform.parent.gameObject;
    transform.SetParent(GameObject.FindGameObjectWithTag(Constants.tagsMinimapCamera).transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
