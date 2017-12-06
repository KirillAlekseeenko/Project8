using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour {

	public void OnStartButtonClick()
	{
		SceneManager.LoadScene ("mainScene");
	}
	public void OnQuitButtonClick()
	{
		Application.Quit ();
	}

}
