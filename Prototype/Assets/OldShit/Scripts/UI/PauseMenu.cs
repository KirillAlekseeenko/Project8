using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	private bool pause;
	public GameObject PausePanel;
	
	// Use this for initialization
	void Awake(){
		pause = false;
		
	}
	public void AppQuite(){
		Application.Quit();
		
	}
	
	// Update is called once per frame
	public void EnterPause(){
		Time.timeScale = 0.0f;
		PausePanel.SetActive(true);
		pause = true;
	}
	public void RestartScene(){
		Application.LoadLevel(0);
		Time.timeScale = 1.0f;
		PausePanel.SetActive(false);
		pause = false;
	}
	public void ExitPause(){
		Time.timeScale = 1.0f;
		PausePanel.SetActive(false);
		pause = false;
			
	}
	void Update () {
		if(!pause && Input.GetButtonDown("Cancel")){
			EnterPause();			
		}
		else if(pause && Input.GetButtonDown("Cancel")){
			ExitPause();

		}
		
	}
}
