using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	[SerializeField] private GameObject loadingPanel;
	[SerializeField] private GameObject comicsPanel;
	[SerializeField] private List<ComicsStrip> comicsStrips;
	[SerializeField] private AudioSource audioSource;

	private void startShowing(){
		comicsPanel.SetActive (true);
		comicsPanel.GetComponent<AudioSource> ().Play ();
		audioSource.Stop();
		audioSource.loop = false;
		StartCoroutine (showComics ());
	}

	private IEnumerator showComics(){
		for(int i = 0; i < comicsStrips.Count; i++){
			comicsStrips [i].stripImage.gameObject.SetActive (true);
			if (comicsStrips [i].clip != null)
				audioSource.PlayOneShot (comicsStrips [i].clip);
			else {
				if(i< 9)
					audioSource.Stop ();
			}
			if (i == 5) {
				for (int j = 0; j < 5; j++) {
					comicsStrips [j].stripImage.gameObject.SetActive (false);
				}
			}
			StartCoroutine (lighten (comicsStrips [i].stripImage));
			yield return new WaitForSecondsRealtime (comicsStrips [i].showTime);
		}
		loadingPanel.SetActive(true);
		gameObject.SetActive(false);
		comicsPanel.SetActive (false);
		SceneManager.LoadScene(1);
	}

	private IEnumerator lighten(Image comicsImage){
		while (comicsImage.color.a < 1) {
			comicsImage.color = new Color (
				comicsImage.color.r,
				comicsImage.color.g,
				comicsImage.color.b,  comicsImage.color.a + 0.05f);
			yield return new WaitForSecondsRealtime (0.01f);

		}
	}

	public void StartGame(){
		startShowing ();
		//SceneManager.LoadScene(1);
		//loadingPanel.SetActive(true);
		//gameObject.SetActive(false);

	}
	public void ExitGame(){
		Application.Quit();
	}
}


[System.Serializable]
public class ComicsStrip{
	public Image stripImage;
	public float showTime;
	public AudioClip clip;
}
