using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	[SerializeField] private GameObject loadingPanel;
	public void StartGame(){
		SceneManager.LoadScene(1);
		loadingPanel.SetActive(true);
		gameObject.SetActive(false);

	}
	public void ExitGame(){
		Application.Quit();
	}
	
}
