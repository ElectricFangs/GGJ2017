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

  public const float wavesSizeIncrement = 0.30f;
  public const float wavesSpeed = 0.15f;

  public const float enemiesMaxAlert = 5;
  public const float enemiesAlertDecay = 0.25f;
  public const float enemiesAlertPerWave = 1.0f;
  public const float enemiesSpeedUnit = 0.1f;
  public const float enemiesSpeechDuration = 1.5f;
  public const float enemiesMadIdleDuration = 0.5f;
  public static readonly string[] enemiesRandomLines = {
    "Why did we buy a house this big?!",
    "The house sure is quiet today.",
    "I just hope I won't have to go\nup those bloody stairs today.",
    "Brrrr... we should think about heating..."
  };
  public static readonly string[] enemiesRandomLinesAlerted = {
    "I keep hearing some weird sounds...",
    "Did I forget to turn off the TV?",
    "Hmm... I thought I heard something.\nMust've been the wind.",
    "What's that weird sound?",
    "Hmm..."
  };
  public static readonly string[] enemiesRandomLinesMad = {
    "AHA! I KNEW IT!",
    "I HEAR A THIEF!",
    "THIEF!!!",
    "CALL THE POLICE!"
  };

  public const string resourceWave = "Wave";

  public const string tagsWave = "Wave";
  public const string tagsItem = "Item";
  public const string tagsFloorOrigin = "Floor origin";
  public const string tagsMinimapOrigin = "Minimap origin";
  public const string tagsFloorDimensions = "FloorDimensions";
  public const string tagsMinimapFloorDimensions = "MinimapDimensions";
  public const string tagsMinimapCamera = "MinimapCamera";
  public const string tagsMainCameraFade = "MainCameraFade";

  public const string namesNotificationsObject = "Notifications";
  public const string namesLocationManager = "LocationManager";

  public const float eps = 0.01f;

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
