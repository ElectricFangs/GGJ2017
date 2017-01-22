using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
  public enum EnemyState {
    ES_DEFAULT,
    ES_MAD_IDLE,
    ES_MAD_FLY
  }

  public float currentAlert = 0;
  public EnemyState state = EnemyState.ES_DEFAULT;
  public Marker startMarker;
  public bool standingStill;
  private Marker currentMarker;
  private Marker lastMarker;
  private Rigidbody2D enemyRigidbody;
  private Animator enemyAnimator;
  private SpriteRenderer enemyRenderer;

  public int collidingWavesCount = 0;

  public Sprite spriteMadNotification;
  private GameObject objectMadNotification;

  private SpeechBubbleHandler speechHandler;

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
    enemyAnimator = GetComponent<Animator>();
    enemyRenderer = GetComponent<SpriteRenderer>();
    speechHandler = GetComponent<SpeechBubbleHandler>();
	}

  // Update is called once per frame
  void Update() {
    if (state == EnemyState.ES_DEFAULT && collidingWavesCount > 0) {
      currentAlert += collidingWavesCount * Constants.enemiesAlertPerWave * Time.deltaTime;
      if (currentAlert >= Constants.enemiesMaxAlert) {
        currentAlert = Constants.enemiesMaxAlert;
        state = EnemyState.ES_MAD_IDLE;
        StartCoroutine(GetMad());
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
        SpeakLine();
        StartCoroutine(WaitAtMarker(currentMarker.waitTime));
      } else {
        if (lastMarker != currentMarker) {
          lastMarker = currentMarker;
          SpeakLine();
        }
        currentMarker = currentMarker.nextMarker;
        if (currentMarker != null) {
          Vector3 moveDirection = (currentMarker.transform.position - transform.position).normalized;
          float length = moveDirection.magnitude;
          enemyRigidbody.velocity = new Vector2(moveDirection.x / length * Constants.enemiesSpeedUnit, moveDirection.y / length * Constants.enemiesSpeedUnit);
        }
      }
      enemyAnimator.SetBool("isWalking", !standingStill);
      float dirX = (currentMarker.transform.position - transform.position).x;
      enemyRenderer.flipX = dirX < 0;
    }
	}

  private void SpeakLine() {
    if (speechHandler == null) {
      return;
    }
    string text = currentMarker.speechLine;
    if (currentMarker.useRandomSpeech) {
      if (currentAlert < Constants.enemiesMaxAlert / 2.0f) {
        text = Constants.enemiesRandomLines[Random.Range(0, Constants.enemiesRandomLines.Length)];
      } else {
        text = Constants.enemiesRandomLinesAlerted[Random.Range(0, Constants.enemiesRandomLinesAlerted.Length)];
      }
    }

    if (!string.IsNullOrEmpty(text)) {
      speechHandler.Speak(text);
    }
  }

  IEnumerator GetMad() {
    enemyRigidbody.velocity = new Vector2(0, 0);
    enemyAnimator.SetInteger("State", (int)state);
    objectMadNotification.GetComponent<SpriteRenderer>().sprite = spriteMadNotification;
    objectMadNotification.SetActive(true);
    GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Managers").GetComponent<SoundManager>().GetGaspSound());
    yield return new WaitForSeconds(Constants.enemiesMadIdleDuration);
  }

  IEnumerator WaitAtMarker(float waitTime) {
    yield return new WaitForSeconds(waitTime);
    standingStill = false;
  }
}
