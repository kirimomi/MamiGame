using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnButtonMamiGet(){
		SceneManager.LoadScene ("MamiGet");
	}

	public void OnButtonIkariMami(){
		SceneManager.LoadScene ("IkariMami");
	}
}
