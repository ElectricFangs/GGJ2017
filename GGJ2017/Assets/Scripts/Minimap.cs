using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {
  public enum MinimapOrientation {
    MO_FRONT,
    MO_SIDE
  }

  public MinimapOrientation orientation = MinimapOrientation.MO_FRONT;

  private List<GameObject> minimapOrigins = new List<GameObject>();
  private LocationManager locationManager;
  private Vector2 floorDimensions;

  // Use this for initialization
  void Start () {
    minimapOrigins.AddRange(GameObject.FindGameObjectsWithTag(Constants.tagsMinimapOrigin));
    if (minimapOrigins.Count > 1) {
      minimapOrigins.Sort(delegate (GameObject a, GameObject b) {
        return (a.transform.position.y).CompareTo(b.transform.position.y);
      });
    }

    locationManager = GameObject.Find("Managers").GetComponent<LocationManager>();

    GameObject floorDimensionsObject = GameObject.FindGameObjectWithTag(Constants.tagsMinimapFloorDimensions);
    floorDimensions = new Vector2(floorDimensionsObject.transform.localPosition.x, floorDimensionsObject.transform.localPosition.y);
  }
	
	// Update is called once per frame
	void Update () {
    MinimapMarker[] children = GetComponentsInChildren<MinimapMarker>();
    foreach (MinimapMarker child in children) {
      Vector3 positionRatio = locationManager.GetFloorLocation(child.trackTarget);
      Vector3 positionMinimapFloor = minimapOrigins[(int)positionRatio[2]].transform.position + new Vector3(floorDimensions.x * positionRatio.x, floorDimensions.y * positionRatio.y, 0);
      child.transform.Translate(positionMinimapFloor - child.transform.position);
    }
	}
}
