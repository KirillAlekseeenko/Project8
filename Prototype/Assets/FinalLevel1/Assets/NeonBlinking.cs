using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonBlinking : MonoBehaviour {

	[SerializeField]
	private BlinkingType blinkType; 
	[SerializeField]
	private Renderer sign;
	[SerializeField]
	private float timeBetweenBlinks;

	private enum BlinkingType{
		ONOFF,
		RANDOM
	};
	private Color signColor; 

	void Awake(){
		signColor = sign.material.GetColor ("_EmissionColor");
		if (blinkType == BlinkingType.RANDOM) {
			StartCoroutine (randomBlinking());
		} else if (blinkType == BlinkingType.ONOFF) {
			StartCoroutine (onOffBlinking());
		}
	}

	private IEnumerator randomBlinking(){
		while (true) {
			sign.material.SetColor ("_EmissionColor", new Color (0, 0, 0, 0));
			yield return new WaitForSeconds (Random.Range (0f, timeBetweenBlinks));
			sign.material.SetColor ("_EmissionColor", signColor);
			yield return new WaitForSeconds (Random.Range (0f, timeBetweenBlinks));
		}
	}

	private IEnumerator onOffBlinking(){
		while (true) {
			sign.material.SetColor ("_EmissionColor", new Color (0, 0, 0, 0));
			yield return new WaitForSeconds (timeBetweenBlinks);
			sign.material.SetColor ("_EmissionColor", signColor);
			yield return new WaitForSeconds (timeBetweenBlinks);
		}
	}
}
