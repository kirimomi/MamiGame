using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serihu : MonoBehaviour {

	float m_countDown = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		m_countDown -= Time.deltaTime;
		if (m_countDown < 0) {
			Destroy (gameObject);
		}
	}
}
