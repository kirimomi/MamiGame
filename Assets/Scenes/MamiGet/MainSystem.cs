using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour {

	public static int Score;

	public static float Counter;

	public Text ScoreText;

	public Text CounterText;

	public Text HighScoreText;

	public GameObject ReplayButton;
	public GameObject BackGround;

	int m_highScore;

	public const float PLAY_TIME = 30f;

	// Use this for initialization
	void Start () {
		ReplayButton.SetActive (false);
		BackGround.SetActive (false);

		Score = 0;
		Counter = PLAY_TIME;

		m_highScore = LoadHighScore ();
		if (m_highScore < 0) {
			m_highScore = 0;
		}
		HighScoreText.text = "High : " + m_highScore.ToString ();
	}


	bool m_isGameEnd = false;

	void Update () {

		CounterText.text = Counter.ToString ("00.00");
		ScoreText.text = Score.ToString ();

		if (!m_isGameEnd) {
			Counter -= Time.deltaTime;
			if (Counter < 0) {
				Counter = 0;
				m_isGameEnd = true;
				ReplayButton.SetActive (true);
				BackGround.SetActive (true);
				if (m_highScore < Score) {
					SaveHighScore (Score);
					Debug.Log ("high score updated : " + Score.ToString ());
				}
			}
		}
	}

	public void OnButtonReplay(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	const string HIGH_SCORE_KEY = "highScore";

	void SaveHighScore(int score){
		PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
		PlayerPrefs.Save();
	}

	int LoadHighScore(){
		Debug.Log ("high score is " + PlayerPrefs.GetInt (HIGH_SCORE_KEY, -1).ToString ());
		return PlayerPrefs.GetInt(HIGH_SCORE_KEY, -1);
	}

}
