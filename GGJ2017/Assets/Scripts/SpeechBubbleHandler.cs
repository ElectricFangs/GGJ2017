using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpeechBubbleHandler : MonoBehaviour {

  public SpeechBubble bubbleObject;
  public GameObject handle;
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

  public void Speak(string text, float duration = 15000) {
    bubbleDuration = duration;
    bubbleObject.GetComponentInChildren<Text>().text = text;
    bubbleObject.gameObject.SetActive(true);
  }

  public void StopSpeak() {
    bubbleDuration = 0;
    bubbleObject.gameObject.SetActive(false);
  }
}
