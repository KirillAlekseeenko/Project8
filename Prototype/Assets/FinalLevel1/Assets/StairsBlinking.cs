using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsBlinking : MonoBehaviour {

	[SerializeField]
	private float timeBetweenBlinks;

	[SerializeField]
	private List<Renderer> stairs;

	private Color currentElColor;
	private int pointer;

	void Awake(){
		currentElColor = stairs [0].material.GetColor ("_EmissionColor");
		StartCoroutine (stairsBlink ());
	}

	private IEnumerator stairsBlink(){
		while (true) {
			stairs[pointer].material.SetColor ("_EmissionColor", currentElColor);
			currentElColor = stairs [pointer+1].material.GetColor ("_EmissionColor");
			stairs[pointer+1].material.SetColor ("_EmissionColor", new Color (0, 0, 0, 0));
			yield return new WaitForSeconds (timeBetweenBlinks);
			pointer++;
			if (pointer > stairs.Count - 2) {
				stairs[pointer].material.SetColor ("_EmissionColor", currentElColor);
				pointer = 0;
			}
		}
	}
}
