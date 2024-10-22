﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour
{

	public static int Score;

	public static float Counter;

	public Text ScoreText;

	public Text CounterText;

	public Text HighScoreText;
	public Text LowScoreText;

	public GameObject Buttons;
	public GameObject BackGround;

	public static AudioSource Audio;

	public AudioClip BGM;
	public AudioClip END;

	public SpriteRenderer BG;


	int m_highScore;
	int m_lowScore;

	public const float PLAY_TIME = 30f;

	Color m_startColor, m_endColor;

	// Use this for initialization
	void Start ()
	{
		Audio = GetComponent<AudioSource> ();
		Audio.clip = BGM;
		Audio.Play ();
		Buttons.SetActive (false);
		BackGround.SetActive (false);

		m_startColor = new Color (195f / 256f, 195f / 256f, 195f / 256f, 1f);
		m_endColor = new Color (94f / 256f, 94f / 256f, 163f / 256f, 1f);

		Score = 0;
		Counter = PLAY_TIME;

		m_highScore = LoadHighScore ();
		if (m_highScore < 0) {
			HighScoreText.text = "";
		} else {
			HighScoreText.text = "High : " + m_highScore.ToString ();
		}

		m_lowScore = LoadLowScore ();
		if (1000 < m_lowScore) {
			LowScoreText.text = "";
		} else {
			LowScoreText.text = "Low : " + m_lowScore.ToString ();
		}



	}


	bool m_isGameEnd = false;

	void Update ()
	{

		CounterText.text = Counter.ToString ("00.00");
		ScoreText.text = Score.ToString ();

		if (!m_isGameEnd) {

			//背景色
			float h, s, v;
			Color.RGBToHSV (m_endColor, out h, out s, out v);
			h += Counter * 0.5f;
			h %= 1;
			Color trueEndColor = Color.HSVToRGB (h, s, v);

			Color bgColor = trueEndColor * (PLAY_TIME - Counter) / PLAY_TIME + m_startColor * Counter / PLAY_TIME;

			//Color bgColor = m_endColor * (PLAY_TIME - Counter) / PLAY_TIME + m_startColor * Counter / PLAY_TIME;
			//Color bgColor = m_endColor;

			BG.color = bgColor;

			Counter -= Time.deltaTime;
			if (Counter < 0) {
				Counter = 0;
				m_isGameEnd = true;
				Buttons.SetActive (true);
				BackGround.SetActive (true);

				Audio.Stop ();
				Audio.PlayOneShot (END);
				if (m_highScore < Score) {
					SaveHighScore (Score);
				}
				if (Score < m_lowScore) {
					SaveLowScore (Score);
				}
			}
		}
	}

	public void OnButtonReplay ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void OnButtonReturn ()
	{
		SceneManager.LoadScene ("Selector");
	}

	const string HIGH_SCORE_KEY = "highScore";
	const string LOW_SCORE_KEY = "lowScore";

	void SaveHighScore (int score)
	{
		PlayerPrefs.SetInt (HIGH_SCORE_KEY, score);
		PlayerPrefs.Save ();
	}

	int LoadHighScore ()
	{
		//Debug.Log ("high score is " + PlayerPrefs.GetInt (HIGH_SCORE_KEY, -1).ToString ());
		return PlayerPrefs.GetInt (HIGH_SCORE_KEY, -1);
	}

	void SaveLowScore (int score)
	{
		PlayerPrefs.SetInt (LOW_SCORE_KEY, score);
		PlayerPrefs.Save ();
	}

	int LoadLowScore ()
	{
		return PlayerPrefs.GetInt (LOW_SCORE_KEY, 99999);
	}

}
