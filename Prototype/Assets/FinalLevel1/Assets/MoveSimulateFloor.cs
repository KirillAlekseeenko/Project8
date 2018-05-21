using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSimulateFloor : MonoBehaviour {

	[SerializeField]
	private Renderer floor;
	[SerializeField]
	private float timeBetweenMoves;


	void Awake(){
		StartCoroutine (move ());	
	}

	private IEnumerator move(){
		while (true) {
			floor.material.SetTextureScale ("_MainTex", new Vector2 (1.2f, 1.4f));
			yield return new WaitForSeconds (timeBetweenMoves);
			floor.material.SetTextureScale ("_MainTex", new Vector2 (1f, 1.4f));
			yield return new WaitForSeconds (timeBetweenMoves);
		}
	}
}
