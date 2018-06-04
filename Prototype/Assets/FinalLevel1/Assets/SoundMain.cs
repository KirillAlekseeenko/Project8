using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : MonoBehaviour {

	public static SoundMain instance;
	private AudioSource audioSource;

	void Awake(){
		if (instance == null)
			instance = this;
		if (instance != this)
			Destroy (gameObject);

		audioSource = gameObject.GetComponent<AudioSource> ();
	}
	public void Play(AudioClip clip){
		audioSource.PlayOneShot (clip);
	}
}
