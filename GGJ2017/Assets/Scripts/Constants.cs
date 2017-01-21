using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

  public const string controlsLeft = "a";
  public const string controlsUp = "w";
  public const string controlsDown = "s";
  public const string controlsRight = "d";

  public const float movementPlayerSpeedUnit = 0.5f;
  public const float movementPlayerWeightFactor = 0.01f;

  public const float playerDefaultWaves = 1;

  public const float wavesSizeIncrement = 0.15f;
  public const float wavesSpeed = 0.05f;

  public const float enemiesMaxAlert = 5;
  public const float enemiesAlertDecay = 0.3f;
  public const float enemiesAlertPerWave = 1.0f;
  public const float enemiesSpeedUnit = 0.1f;

  public const string resourceWave = "Wave";

  public const string tagsWave = "Wave";
  public const string tagsItem = "Item";

  public const string namesNotificationsObject = "Notifications";

  public const float eps = 0.01f;

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
