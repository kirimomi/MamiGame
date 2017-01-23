using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IkariMamiMainSystem : MonoBehaviour
{

	public GameObject MamiPrefab;

	public static AudioSource Audio;
	public AudioClip BGM;
	public AudioClip END;

	public AudioClip Explosion;

	public GameObject Telop;

	public Text TimeText;
	public Text RecordText;
	public GameObject Buttons;

	public GameObject LocatorRoot;
	public Transform [] Locators;

	struct WaveDef {
		public int MamiNum;
		public int NormalNum;
		public int IkariNum;
		public int NorioNum;
	}

	int m_wave = 1;
	const int WAVE_MAX = 10;

	float m_time = 0;

	WaveDef GetWaveDefs(int wave){
		Assert.IsTrue(1<=wave && wave <=WAVE_MAX);

		WaveDef ret = new WaveDef();
		switch (wave) {
		case 1:
			ret.MamiNum = 3;
			ret.NormalNum = 1;
			ret.IkariNum = 0;
			ret.NorioNum = 0;
			break;
		case 2:
			ret.MamiNum = 3;
			ret.NormalNum = 1;
			ret.IkariNum = 2;
			ret.NorioNum = 0;
			break;
		case 3:
			ret.MamiNum = 5;
			ret.NormalNum = 2;
			ret.IkariNum = 0;
			ret.NorioNum = 1;
			break;
		case 4:
			ret.MamiNum = 5;
			ret.NormalNum = 1;
			ret.IkariNum = 1;
			ret.NorioNum = 1;
			break;
		case 5:
			ret.MamiNum = 8;
			ret.NormalNum = 0;
			ret.IkariNum = 0;
			ret.NorioNum = 4;
			break;
		case 6:
			ret.MamiNum = 8;
			ret.NormalNum = 2;
			ret.IkariNum = 3;
			ret.NorioNum = 0;
			break;
		case 7:
			ret.MamiNum = 8;
			ret.NormalNum = 1;
			ret.IkariNum = 4;
			ret.NorioNum = 0;
			break;
		case 8:
			ret.MamiNum = 11;
			ret.NormalNum = 3;
			ret.IkariNum = 5;
			ret.NorioNum = 0;
			break;
		case 9:
			ret.MamiNum = 11;
			ret.NormalNum = 4;
			ret.IkariNum = 2;
			ret.NorioNum = 1;
			break;
		case 10:
			ret.MamiNum = 11;
			ret.NormalNum = 0;
			ret.IkariNum = 11;
			ret.NorioNum = 0;
			break;
		}

		return ret;

	}



	float m_fastestTime;

	// Use this for initialization
	void Start ()
	{
		Audio = GetComponent<AudioSource> ();
		Audio.clip = BGM;
		Audio.Play ();

		Telop.SetActive (false);
		Buttons.SetActive (false);

		m_fastestTime = LoadFastestTime ();
		if (100f <= m_fastestTime ) {
			RecordText.text = "";
		} else {
			RecordText.text = "Record : " + m_fastestTime.ToString ("00.00");
		}

		MakeWave (m_wave);

		StartCoroutine (Main ());

	}


	bool m_isMainStart = false;

	IEnumerator Main(){
		if (m_isMainStart) {
			yield break;
		}
		m_isMainStart = true;
			

		Debug.Log ("Main Start");
		while (true) {

			bool input = false;
			Vector2 worldPoint = new Vector2();
			if (Input.GetMouseButtonDown (0)) {
				worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				input = true;
			}

			/*
			if(0 < Input.touchCount){
			Touch touch = Input.GetTouch(0);
					worldPoint = Camera.main.ScreenToWorldPoint (touch.position);
					input = true;
			}*/

			if(input){
				//Debug.Log ("worldPoint :x" + worldPoint.x + "y:" + worldPoint.y);

				//タッチをした位置にオブジェクトがあるかどうかを判定
				Collider2D overlaped = Physics2D.OverlapPoint(worldPoint);
				if (overlaped) {
					if (overlaped.transform.gameObject.CompareTag("IkariMami_Mami")) {
						overlaped.transform.gameObject.GetComponent<IkariMami_Mami> ().Touched ();
					}
				}



				/*
				//タッチをした位置にオブジェクトがあるかどうかを判定
				RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
				if (hit) {
					Bounds rect = hit.collider.bounds;
					//オブジェクトがある場合は、そのオブジェクトを消している
					if (rect.Contains (worldPoint)) {
						if (hit.collider.gameObject.tag == "IkariMami_Mami") {
							hit.collider.gameObject.GetComponent<IkariMami_Mami> ().Touched ();
						}
					}
				}*/
			}


			TimeText.text = m_time.ToString ("00.00");


			if (CheckClear ()) {
				Debug.Log ("Cleared!");
				yield return ClearWave ();
				m_wave++;
				if (WAVE_MAX < m_wave) {
					yield return ClearGame ();
				} else {
					MakeWave (m_wave);
				}
			}

			m_time += Time.deltaTime;
			yield return null;
		}
	}


	GameObject [] m_mamis;
	bool m_isMamiMade = false;

	bool CheckClear(){
		if (!m_isMamiMade) {
			return false;
		}

		/*if (m_mamis == null) {
			return false;
		}*/

		for(int i=0; i < m_mamis.Length; i++){
			if (m_mamis [i]) {
				if (m_mamis [i].GetComponent<IkariMami_Mami> ().Mode != IkariMami_Mami.MamiMode.Warai) {
					return false;
				}
			}
		}
		return true;
	}


	void MakeWave(int wave){
		LocatorRoot.transform.position = Vector3.zero;

		WaveDef def = GetWaveDefs (wave);
		m_mamis = new GameObject[def.MamiNum];
		for(int i = 0; i < def.MamiNum; i++){
			m_mamis[i] = Instantiate (MamiPrefab, Locators[i].position, Quaternion.identity);
			m_mamis [i].transform.parent = LocatorRoot.transform;
		}
		m_isMamiMade = true;
	}

	const float CLEAR_MOVE_SPEED = 20f;

	//WAVEクリア演出
	IEnumerator ClearWave(){

		Telop.SetActive (true);
		Telop.GetComponent<Text> ().text = "Good!";

		yield return new WaitForSeconds(0.2f);//少し待つ
		Audio.PlayOneShot (Explosion);
	
		while (LocatorRoot.transform.position.y < 9) {
			LocatorRoot.transform.position += Vector3.up * CLEAR_MOVE_SPEED * Time.deltaTime;
			yield return null;
		}

		//消す
		for(int i=0; i < m_mamis.Length; i++){
			if (m_mamis [i]) {
				Destroy (m_mamis [i].gameObject);
			}
		}

		Telop.SetActive (false);
	}

	IEnumerator ClearGame(){

		yield return new WaitForSeconds(0.2f);//少し待つ

		Audio.Stop ();
		Audio.PlayOneShot (END);
		Buttons.SetActive (true);


		Telop.GetComponent<Text> ().text = "Cleared!";
		Telop.SetActive(true);


		if (m_time < m_fastestTime) {
			SaveFastestTime (m_time);
		}

		while (true) {
			yield return null;
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



	const string FASTEST_TIME_KEY = "fastestTime";

	void SaveFastestTime (float time)
	{
		PlayerPrefs.SetFloat (FASTEST_TIME_KEY, time);
		PlayerPrefs.Save ();
	}

	float LoadFastestTime ()
	{
		return PlayerPrefs.GetFloat (FASTEST_TIME_KEY, 9999999f);
	}

}
