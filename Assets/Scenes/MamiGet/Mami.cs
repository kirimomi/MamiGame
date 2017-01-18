using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mami : MonoBehaviour {

	Vector3 m_speed = Vector3.zero;

	public GameObject SerihuPrefab;
	public AudioClip GetSe;
	public AudioClip DropSe;

	void Start () {
		if (m_speed == Vector3.zero) {
			m_speed = new Vector3 (2f, -4f, 0f);
		}
	}

	// Update is called once per frame
	void Update () {

		if (0 < MainSystem.Counter) {
			transform.position += m_speed * Time.deltaTime;
			if (transform.position.y < -4.4f) {
				MainSystem.Audio.PlayOneShot (DropSe);
				MakeSerihu ("あーれー");
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (MainSystem.Counter <= 0) {
			return;
		}else if (col.tag == "Player") {
			MainSystem.Audio.PlayOneShot (GetSe);
			MakeSerihu ("マミーン");
			MainSystem.Score ++;
			Destroy (gameObject);
		} else if (col.tag == "Wall") {
			m_speed.x = -m_speed.x;
		}
	}

	public void SetSpeed(Vector3 spd){
		m_speed = spd;
		m_speed.z = 0;
	}

	void MakeSerihu(string txt){
		GameObject obj = Instantiate (SerihuPrefab, transform.position ,Quaternion.identity);
		obj.transform.FindChild("TextMesh").GetComponent<TextMesh>().text = txt;
	}

}
