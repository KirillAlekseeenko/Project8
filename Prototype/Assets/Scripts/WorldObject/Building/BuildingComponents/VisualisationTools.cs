using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualisationTools : MonoBehaviour {

	[SerializeField]
	private Mesh Level1Model; 
	[SerializeField]
	private Mesh Level2Model;
	[SerializeField]
	private Mesh Level3Model;

	private Renderer objRenderer;
	private Color mainColor;
	private IEnumerator blink;

	//private GameObject relatedBuilding;

	private void Awake(){
		blink = Blink ();
		objRenderer = GetComponent<MeshRenderer> ();
		SetModel (0);
	}

	public void SetModel(int level){
		switch (level) {
			case 0:{
				gameObject.GetComponent<MeshFilter> ().mesh = Level1Model;
				break;
			}
			case 1:{
				gameObject.GetComponent<MeshFilter> ().mesh = Level2Model;
				break;
			}
			case 2:{
				gameObject.GetComponent<MeshFilter> ().mesh = Level3Model;
				break;
			}
		}
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
			//objRenderer.material.color = gameObject.GetComponent<MeshRenderer>().material.color;
			objRenderer.material.SetColor("_Color", new Color(gameObject.GetComponent<MeshRenderer>().material.color.r,
				gameObject.GetComponent<MeshRenderer>().material.color.g,
				gameObject.GetComponent<MeshRenderer>().material.color.b,
				1));
		}
	}
}
