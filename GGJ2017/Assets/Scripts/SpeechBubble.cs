using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour {

  public GameObject talkingObject;
  public GameObject bubbleHandle;

  private Canvas canvas;

	// Use this for initialization
	void Start () {
    talkingObject = transform.parent.gameObject;
    SpeechBubbleHandler handler = talkingObject.GetComponent<SpeechBubbleHandler>();
    handler.bubbleObject = this;
    bubbleHandle = handler.handle;

    transform.SetParent(GameObject.Find("CanvasMain").transform);
    canvas = transform.parent.GetComponent<Canvas>();
    gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
    RectTransform rectTransform = GetComponent<RectTransform>();
    rectTransform.localPosition = CalculatePlacement() + new Vector3(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / 2);
	}

  private Vector3 CalculatePlacement() {
    float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
    float height = canvas.GetComponent<RectTransform>().sizeDelta.y;
    float x = Camera.main.WorldToScreenPoint(bubbleHandle.transform.position).x / Screen.width;
    float y = Camera.main.WorldToScreenPoint(bubbleHandle.transform.position).y / Screen.height;
    return new Vector3(width * x - width / 2, y * height - height / 2);
  }
}
