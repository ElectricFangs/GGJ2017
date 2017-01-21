using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour {

  private List<GameObject> floorOrigins = new List<GameObject>();
  private Vector2 floorDimensions;

  public Vector3 GetFloorLocation(GameObject targetObject) {
    int floorIndex = 0;
    while (floorIndex < floorOrigins.Count && floorOrigins[floorIndex].transform.position.x < targetObject.transform.position.x) {
      floorIndex++;
    }
    floorIndex--;

    return new Vector3(
      (targetObject.transform.position.x - floorOrigins[floorIndex].transform.position.x) / floorDimensions.x,
      (targetObject.transform.position.y - floorOrigins[floorIndex].transform.position.y) / floorDimensions.y,
      floorIndex + 0.05f);
  }

	// Use this for initialization
	void Start () {
    floorOrigins.AddRange(GameObject.FindGameObjectsWithTag(Constants.tagsFloorOrigin));
    Debug.Log("Floor manager detected " + floorOrigins.Count + " floors.");

    if (floorOrigins.Count > 1) {
      floorOrigins.Sort(delegate (GameObject a, GameObject b) {
        return (a.transform.position.x).CompareTo(b.transform.position.x);
      });
    }

    GameObject floorDimensionsObject = GameObject.FindGameObjectWithTag(Constants.tagsFloorDimensions);
    floorDimensions = new Vector2(floorDimensionsObject.transform.localPosition.x, floorDimensionsObject.transform.localPosition.y);
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
