using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
  public enum EnemyState {
    ES_DEFAULT,
    ES_MAD
  }

  public float currentAlert = 0;
  public EnemyState state = EnemyState.ES_DEFAULT;

  public int collidingWavesCount = 0;

  public Sprite spriteMadNotification;
  private GameObject objectMadNotification;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == Constants.tagsWave) {
      collidingWavesCount++;
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.tag == Constants.tagsWave) {
      collidingWavesCount--;
    }
  }

  // Use this for initialization
  void Start () {
    objectMadNotification = transform.FindChild(Constants.namesNotificationsObject).gameObject;
    objectMadNotification.SetActive(false);
	}

  // Update is called once per frame
  void Update() {
    if (collidingWavesCount > 0) {
      currentAlert += collidingWavesCount * Constants.enemiesAlertPerWave * Time.deltaTime;
      if (currentAlert >= Constants.enemiesMaxAlert) {
        currentAlert = Constants.enemiesMaxAlert;
        state = EnemyState.ES_MAD;
        objectMadNotification.GetComponent<SpriteRenderer>().sprite = spriteMadNotification;
        objectMadNotification.SetActive(true);
      }
    } else if (state == EnemyState.ES_DEFAULT) {
      currentAlert = Mathf.Max(0, currentAlert - Constants.enemiesAlertDecay * Time.deltaTime);
    }
	}
}
