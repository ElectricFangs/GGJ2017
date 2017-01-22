using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

  public List<GameObject> nearbyObjects = new List<GameObject>();
  public List<GameObject> carryingItems = new List<GameObject>();
  public bool isBusy = false;

  public int score;
  public int lastHaulScore;

  private SoundWaves playerWaves;

  public void Speak(string text) {
    GetComponent<SpeechBubbleHandler>().Speak(text);
  }

  public void StopSpeak() {
    GetComponent<SpeechBubbleHandler>().StopSpeak();
  }

  public void ScoreLoot() {
    lastHaulScore = 0;
    foreach (GameObject loot in carryingItems) {
      lastHaulScore += loot.GetComponent<Item>().value;
    }
    carryingItems.Clear();
    playerWaves.SetWaveCount(Constants.playerDefaultWaves);
    score += lastHaulScore;

    Speak(Constants.textHadEnough[0] + lastHaulScore + Constants.textHadEnough[1] + score + Constants.textHadEnough[2]);
  }

  // Use this for initialization
  void Start () {
    playerWaves = GetComponent<SoundWaves>();
    playerWaves.AddWaveCount(Constants.playerDefaultWaves);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
