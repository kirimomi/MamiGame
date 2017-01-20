using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{

	public GameObject MamiPrefab;


	//パラメータを一気に返すための
	struct MamiParam {
		public float mamiPerSec;
		public float speedMaxX;
		public float speedMinY;
		public float speedMaxY;
	}

	//その秒数に応じたパラメータを返す
	MamiParam GetMamiParam(float count){
		MamiParam ret = new MamiParam();
		if (20f < count) {
			ret.mamiPerSec = 6f;
			ret.speedMaxX = 3.0f;
			ret.speedMinY = -3f;
			ret.speedMaxY = -2f;
		}else if (15f < count) {
			ret.mamiPerSec = 7f;
			ret.speedMaxX = 3.0f;
			ret.speedMinY = -4f;
			ret.speedMaxY = -3f;
		} else if (10f < count) {
			ret.mamiPerSec = 8f;
			ret.speedMaxX = 5f;
			ret.speedMinY = -6f;
			ret.speedMaxY = -4f;
		} else if (5f < count) {
			ret.mamiPerSec = 9f;
			ret.speedMaxX = 8f;
			ret.speedMinY = -8f;
			ret.speedMaxY = -6f;
		} else {
			ret.mamiPerSec = 10f;
			ret.speedMaxX = 14f;
			ret.speedMinY = -20f;
			ret.speedMaxY = -12f;
		}
		return ret;
	}


	// Use this for initialization
	void Start ()
	{
		MakeFirstMami ();
	}
	
	// Update is called once per frame

	const float MAMI_PER_SEC_MIN = 1f;
//最小秒間出現（初期値）
	const float MAMI_PER_SEC_MAX = 15f;
//最大秒間出現
	const float MAMI_PER_SEC_MAX_TIME = 25f;
//この秒数でMAX

	float m_mamiCounter = 0;

	void Update ()
	{

		/*
		float elaspedTime = (MainSystem.PLAY_TIME - MainSystem.Counter);
		float mamiPerSec = MAMI_PER_SEC_MIN + (MAMI_PER_SEC_MAX - MAMI_PER_SEC_MIN) / MAMI_PER_SEC_MAX_TIME * elaspedTime;
		if (MAMI_PER_SEC_MAX < mamiPerSec) {
			mamiPerSec = MAMI_PER_SEC_MAX;
		}*/

		MamiParam mp = GetMamiParam (MainSystem.Counter);

		if (0 < MainSystem.Counter && 1.0f / mp.mamiPerSec < m_mamiCounter) {
			MakeMami ();
			m_mamiCounter = 0;
		}

		m_mamiCounter += Time.deltaTime;
	}

	const float MAMI_START_POS_Y = 6.3f;
	const float MAMI_START_POS_X = 2.2f;


	void MakeMami ()
	{
		Vector3 pos = Vector3.up * MAMI_START_POS_Y;
		pos.x = Random.Range (-MAMI_START_POS_X, MAMI_START_POS_X);
		GameObject obj = Instantiate (MamiPrefab, pos, Quaternion.identity);


		//Vector3 spd = Vector3.up * -3.0f;
		MamiParam mp = GetMamiParam(MainSystem.Counter);

		Vector3 spd = Vector3.zero;

		spd.x = Random.Range (-mp.speedMaxX, mp.speedMaxX);
		spd.y = Random.Range (mp.speedMinY, mp.speedMaxY);

		/*
		if (15f < MainSystem.Counter) {
			spd.x = Random.Range (-3.0f, 3.0f);
			spd.y = -1.5f;
		} else if (10f < MainSystem.Counter) {
			spd.x = Random.Range (-5.0f, 5.0f);
			spd.y = Random.Range (-4f, -1.5f);
		} else if (5f < MainSystem.Counter) {
			spd.x = Random.Range (-8.0f, 8.0f);
			spd.y = Random.Range (-6f, -3f);
		} else {
			spd.x = Random.Range (-14.0f, 14.0f);
			spd.y = Random.Range (-15f, -5f);
		}*/
		obj.GetComponent<Mami> ().SetSpeed (spd);
	}


	//初期マミ
	const int FIRST_MAMI_NUM = 3;
	void MakeFirstMami ()
	{
		MamiParam mp = GetMamiParam (MainSystem.PLAY_TIME);

		for (int i = 0; i < FIRST_MAMI_NUM; i++) {
			Vector3 pos = Vector3.up * (MAMI_START_POS_Y + (mp.speedMinY * 1f / mp.mamiPerSec  * (float)i));

			pos.x = Random.Range (-MAMI_START_POS_X, MAMI_START_POS_X);
			GameObject obj = Instantiate (MamiPrefab, pos, Quaternion.identity);

			Vector3 spd = Vector3.zero;
			spd.x = Random.Range (-mp.speedMaxX, mp.speedMaxX);
			spd.y = Random.Range(mp.speedMinY, mp.speedMaxY);
			obj.GetComponent<Mami> ().SetSpeed (spd);
		}
	}
}
