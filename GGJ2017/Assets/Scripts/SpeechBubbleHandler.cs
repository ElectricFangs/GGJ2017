using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpeechBubbleHandler : MonoBehaviour {

  public SpeechBubble bubbleObject;
  private float bubbleDuration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    bubbleDuration = Mathf.Max(-1, bubbleDuration - Time.deltaTime);
    if (bubbleDuration <= 0) {
      bubbleObject.gameObject.SetActive(false);
    }
	}

  public void Speak(string text) {
    bubbleDuration = Constants.enemiesSpeechDuration;
    bubbleObject.GetComponentInChildren<Text>().text = text;
    bubbleObject.gameObject.SetActive(true);
  }
}
