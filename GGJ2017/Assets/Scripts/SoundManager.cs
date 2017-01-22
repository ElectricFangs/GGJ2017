using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
  public AudioClip[] loudStepSounds;
  public AudioClip[] quietLaughSounds;
  public AudioClip[] gaspSounds;

  public AudioClip GetLoudStepSound() {
    if (loudStepSounds.Length == 0) {
      return null;
    }

    return loudStepSounds[Random.Range(0, loudStepSounds.Length - 1)];
  }

  public AudioClip GetQuietLaughSound() {
    if (quietLaughSounds.Length == 0) {
      return null;
    }

    return quietLaughSounds[Random.Range(0, quietLaughSounds.Length - 1)];
  }

  public AudioClip GetGaspSound() {
    if (gaspSounds.Length == 0) {
      return null;
    }

    return gaspSounds[Random.Range(0, gaspSounds.Length - 1)];
  }

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
