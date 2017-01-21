using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaves : MonoBehaviour {

  private List<GameObject> waves = new List<GameObject>();
  private float waveCount;

  private void SetWaveSize(int i, float newSize) {
    waves[i].transform.localScale = new Vector3(newSize, newSize, 1);

    if (newSize >= (waves.Count - 1) * Constants.wavesSizeIncrement) {
      SpriteRenderer spriteRenderer = waves[i].GetComponent<SpriteRenderer>();
      Color color = spriteRenderer.color;
      color.a = 1 - (newSize - (waves.Count - 1) * Constants.wavesSizeIncrement) / Constants.wavesSizeIncrement;
      spriteRenderer.color = color;
    } else {
      SpriteRenderer spriteRenderer = waves[i].GetComponent<SpriteRenderer>();
      Color color = spriteRenderer.color;
      color.a = 1;
      spriteRenderer.color = color;
    }
  }

  private void ResetWaves() {
    for (int i = 0; i < waves.Count; i++) {
      SetWaveSize(i, Constants.wavesSizeIncrement * i);
    }
  }

  public void AddWaveCount(float addCount) {
    waveCount += addCount;
    AddWaves((int)(waveCount - waves.Count));
  }

  public void SetWaveCount(float setCount) {
    foreach (GameObject wave in waves) {
      Destroy(wave);
    }
    waves.Clear();
    waveCount = setCount;
    AddWaveCount(0);
  }

  private void AddWaves(int addCount) {
    if (addCount == 0) {
      return;
    }

    ResetWaves();
    for (int i = 0; i < addCount; i++) {
      GameObject wave = Instantiate(Resources.Load(Constants.resourceWave), transform.position, Quaternion.identity) as GameObject;
      waves.Add(wave);
      wave.transform.parent = transform;
      SetWaveSize(waves.Count - 1, Constants.wavesSizeIncrement * (waves.Count - 1));
    }
  }

	// Use this for initialization
	void Start () {
  }
	
	// Update is called once per frame
	void Update () {
    float radiusAdd = Time.deltaTime * (Constants.wavesSpeed);
    for (int i = 0; i < waves.Count; i++) {
      float newSize = waves[i].transform.localScale.x + radiusAdd;
      if (newSize >= waves.Count * Constants.wavesSizeIncrement) {
        SetWaveSize(i, 0);
      } else {
        SetWaveSize(i, newSize);
      }
    }
  }
}
