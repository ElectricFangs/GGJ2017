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
  public Marker startMarker;
  public bool standingStill;
  private Marker currentMarker;
  private Marker lastMarker;
  private Rigidbody2D enemyRigidbody;

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

    currentMarker = startMarker;
    enemyRigidbody = GetComponent<Rigidbody2D>();
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

    if (state == EnemyState.ES_DEFAULT && !standingStill && currentMarker != null) {
      float testl = (currentMarker.transform.position - transform.position).magnitude;
      if ((currentMarker.transform.position - transform.position).magnitude > Constants.eps) {
        Vector3 moveDirection = (currentMarker.transform.position - transform.position).normalized;
        float length = moveDirection.magnitude;
        enemyRigidbody.velocity = new Vector2(moveDirection.x / length * Constants.enemiesSpeedUnit, moveDirection.y / length * Constants.enemiesSpeedUnit);
      // this marker requires waiting and we didn't already wait at this marker
      } else if (currentMarker.waitTime > 0 && currentMarker != lastMarker) {
        enemyRigidbody.velocity = new Vector2(0, 0);
        standingStill = true;
        lastMarker = currentMarker;
        StartCoroutine(WaitAtMarker(currentMarker.waitTime));
      } else {
        lastMarker = currentMarker;
        currentMarker = currentMarker.nextMarker;
        if (currentMarker != null) {
          Vector3 moveDirection = (currentMarker.transform.position - transform.position).normalized;
          float length = moveDirection.magnitude;
          enemyRigidbody.velocity = new Vector2(moveDirection.x / length * Constants.enemiesSpeedUnit, moveDirection.y / length * Constants.enemiesSpeedUnit);
        }
      }
    }
	}

  IEnumerator WaitAtMarker(float waitTime) {
    yield return new WaitForSeconds(waitTime);
    standingStill = false;
  }
}
