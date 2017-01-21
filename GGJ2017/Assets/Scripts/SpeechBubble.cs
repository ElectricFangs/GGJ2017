using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour {

  public GameObject talkingObject;

  private Canvas canvas;

	// Use this for initialization
	void Start () {
    talkingObject = transform.parent.gameObject;
    talkingObject.GetComponent<SpeechBubbleHandler>().bubbleObject = this;

    transform.SetParent(GameObject.Find("Canvas").transform);
    canvas = transform.parent.GetComponent<Canvas>();
    gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
    GetComponent<RectTransform>().localPosition = CalculatePlacement();
	}

  private Vector3 CalculatePlacement() {
    float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
    float height = canvas.GetComponent<RectTransform>().sizeDelta.y;
    float x = Camera.main.WorldToScreenPoint(talkingObject.transform.position).x / Screen.width;
    float y = Camera.main.WorldToScreenPoint(talkingObject.transform.position).y / Screen.height;
    return new Vector3(width * x - width / 2, y * height - height / 2);
  }
}
