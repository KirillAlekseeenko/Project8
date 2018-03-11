using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualisationTools : MonoBehaviour {

	private Renderer objRenderer;
	private Color mainColor;
	private IEnumerator blink;

	//private GameObject relatedBuilding;

	private void Awake(){
		blink = Blink ();
		objRenderer = GetComponent<MeshRenderer> ();
	}

	// Update is called once per frame
	private void Update () {
		
	}

	private void tryToCatchMe(){
	}

	private IEnumerator Blink() {
		float maxAlpha = 1f;
		float minAlpha = 0.4f;
		float alphaDelta = 0.01f;
		while(true){
			if (objRenderer.material.color.a <= minAlpha || objRenderer.material.color.a >= maxAlpha)
				alphaDelta = -alphaDelta;
			objRenderer.material.SetColor("_Color", new Color(objRenderer.material.color.r,
				objRenderer.material.color.g,
				objRenderer.material.color.b,
				objRenderer.material.color.a + alphaDelta));
			yield return new WaitForSeconds(0.02f);
		}
	}

	public void SetBlinking(bool blinking){
		if (blinking)
			StartCoroutine (blink);
		else {
			StopCoroutine (blink);
			objRenderer.material.color = gameObject.GetComponent<MeshRenderer>().material.color;
		}
	}
}
