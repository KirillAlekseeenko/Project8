using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {

	private bool isGamePaused = false;

	public delegate void PauseAction();

	public static event PauseAction pause;
	public static event PauseAction unpause;


	public bool IsGamePaused {
		get {
			return isGamePaused;
		}
	}
		
	public void pauseGame()
	{
		if (isGamePaused)
			return;
		else {
			isGamePaused = true;
			if (pause != null)
				pause ();
			Time.timeScale = 0; 
		}
	}
	public void unpauseGame()
	{
		if (isGamePaused) {
			isGamePaused = false;
			if (unpause != null)
				unpause ();
			Time.timeScale = 1;
		}
	}

}
