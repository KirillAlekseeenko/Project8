using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour {

	[SerializeField]
	private GameStateHandler gameStateHandler;

	[SerializeField]
	private GameObject pausePanel;

	void Awake()
	{
		GameStateHandler.pause += showPauseMenu;
		GameStateHandler.unpause += hidePauseMenu;
	}

	public void showPauseMenu()
	{
		pausePanel.SetActive (true);
	}
	public void hidePauseMenu()
	{
		pausePanel.SetActive (false);
	}


	// pauseMenu

	public void OnResumeButtonClick()
	{
		gameStateHandler.unpauseGame ();
	}
	public void OnOptionsButtonClick()
	{
		
	}
	public void OnMainMenuButtonClick()
	{
		SceneManager.LoadScene ("mainMenuScene");
	}

	// selection 

}
