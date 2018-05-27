using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaveAdvices : MonoBehaviour {

	[SerializeField] private List<string> advices;
	[SerializeField] private Unit mave;
	[SerializeField] private Text textPanel;

	private AudioSource audioSource;

	void Awake(){
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	public void PutAdvice(int adviceNum){
		if (!mave.isAttacking ()) {
			textPanel.text = advices [adviceNum];
			audioSource.PlayOneShot (audioSource.clip);
		}
	}

	public void HideText(){
		textPanel.text = "";	
	}
}
