using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour {

  public enum FadingState {
    FS_NONE,
    FS_FADING_OUT,
    FS_FADING_IN
  }

  public Image fadeImage;
  private float fadeStart;
  private float fadeTime = -1;
  private FadingState fading = FadingState.FS_NONE;

  public void InitiateFade(float fadeDuration, bool isFadeOut) {
    fadeTime = fadeStart = fadeDuration;
    if (fadeDuration > 0) {
      fading = isFadeOut ? FadingState.FS_FADING_OUT : FadingState.FS_FADING_IN;
    }
  }

	// Use this for initialization
	void Start () {
    fadeImage = GameObject.FindGameObjectWithTag(Constants.tagsMainCameraFade).GetComponent<Image>();
    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
  }
	
	// Update is called once per frame
	void Update () {
    fadeTime = Mathf.Max(-1, fadeTime - Time.deltaTime);
    if (fadeTime <= 0) {
      fading = FadingState.FS_NONE;
    }

    if (fading == FadingState.FS_FADING_OUT) {
      fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1 - fadeTime / fadeStart);
    } else if (fading == FadingState.FS_FADING_IN) {
      fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeTime / fadeStart);
    }
  }
}
