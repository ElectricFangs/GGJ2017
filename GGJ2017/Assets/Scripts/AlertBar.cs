using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBar : MonoBehaviour {

  private Vector3 basePosition;
  private float width;

  public void SetAlert(float percentage) {
    transform.localPosition = new Vector3(basePosition.x - width * (1 - percentage),
                                          basePosition.y, basePosition.z);
  }

  // Use this for initialization
  void Start() {
    basePosition = transform.localPosition;
    width = this.GetComponent<RectTransform>().rect.width;
  }

  // Update is called once per frame
  void Update() {
  }
}
