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
  public CircleCollider2D movingCollider;
  public BoxCollider2D interactCollider;
  public AlertBar alertBar;
  public Canvas alertCanvas;
  private Marker currentMarker;
  private Marker lastMarker;
  private Rigidbody2D enemyRigidbody;
  private Animator enemyAnimator;
  private SpriteRenderer enemyRenderer;
  private GameObject playerTarget;

  public int collidingWavesCount = 0;

  public Sprite spriteMadNotification;
  private GameObject objectMadNotification;

  private SpeechBubbleHandler speechHandler;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == Constants.tagsWave) {
      collidingWavesCount++;
    }
  }

  void OnTriggerStay2D(Collider2D other) {
    if (other.tag == Constants.tagsPlayer) {
      TouchedPlayer(playerTarget.GetComponent<PlayerBehavior>());
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.tag == Constants.tagsWave) {
      collidingWavesCount--;
    }
  }

  public void TouchedPlayer(PlayerBehavior playerBehavior) {
    if (state == EnemyState.ES_MAD_FLY) {
      playerBehavior.isBusy = true;
      GameObject.Find("Managers").GetComponent<GameManager>().EndGame(playerBehavior.score);
    } else if (state == EnemyState.ES_DEFAULT) {
      currentAlert = Constants.enemiesMaxAlert;
      state = EnemyState.ES_MAD_IDLE;
      StartCoroutine(GetMad());
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
    playerTarget = GameObject.FindGameObjectWithTag(Constants.tagsPlayer);
	}

  // Update is called once per frame
  void Update() {
    if (state != EnemyState.ES_DEFAULT) {
      alertCanvas.gameObject.SetActive(false);
    } else {
      alertBar.SetAlert(currentAlert / Constants.enemiesMaxAlert);
    }

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
      movingCollider.offset = new Vector2((enemyRenderer.flipX ? -0.02f : 0.02f), movingCollider.offset.y);
    } else if (state == EnemyState.ES_MAD_FLY) {
      Vector3 moveDirection = (playerTarget.transform.position - transform.position).normalized;
      enemyRigidbody.velocity = new Vector2(moveDirection.x * Constants.enemiesFlySpeedUnit, moveDirection.y * Constants.enemiesFlySpeedUnit);
      enemyRenderer.flipX = moveDirection.x < 0;
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
      speechHandler.Speak(text, Constants.enemiesSpeechDuration);
    }
  }

  IEnumerator GetMad() {
    enemyRigidbody.velocity = new Vector2(0, 0);
    enemyAnimator.SetInteger("State", (int)state);
    objectMadNotification.GetComponent<SpriteRenderer>().sprite = spriteMadNotification;
    objectMadNotification.SetActive(true);
    GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Managers").GetComponent<SoundManager>().GetGaspSound());
    enemyRenderer.flipX = (playerTarget.transform.position - transform.position).x < 0;

    yield return new WaitForSeconds(Constants.enemiesMadIdleDuration);

    movingCollider.enabled = false;
    interactCollider.enabled = true;
    state = EnemyState.ES_MAD_FLY;
    enemyAnimator.SetInteger("State", (int)state);
  }

  IEnumerator WaitAtMarker(float waitTime) {
    yield return new WaitForSeconds(waitTime);
    standingStill = false;
  }
}
