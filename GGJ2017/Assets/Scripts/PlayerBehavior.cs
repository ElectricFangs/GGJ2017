using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

  public List<GameObject> nearbyObjects = new List<GameObject>();
  public List<GameObject> carryingItems = new List<GameObject>();
  public bool isBusy = false;

  private SoundWaves playerWaves;

  // Use this for initialization
  void Start () {
    playerWaves = GetComponent<SoundWaves>();
    playerWaves.AddWaveCount(Constants.playerDefaultWaves);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
