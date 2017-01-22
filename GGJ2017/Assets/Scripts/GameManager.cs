using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

  public GameObject prompt;

  public void EndGame(int totalScore, bool won = false) {
    Time.timeScale = 0;
    prompt.SetActive(true);
    Text text = prompt.GetComponentInChildren<Text>();
    if (!won) {
      text.text = Constants.textEndGame[0] + totalScore + Constants.textEndGame[1];
    } else {
      text.text = Constants.textEndGameWon[0] + totalScore + Constants.textEndGameWon[1];
    }
  }

  public void EndGameWon() {
    EndGame(GameObject.Find("Player").GetComponent<PlayerBehavior>().score, true);
  }

  public void RestartGame() {
    Time.timeScale = 1;
    prompt.SetActive(false);
    SceneManager.LoadScene(0);
  }

  public void QuitGame() {
   // UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
  }

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
