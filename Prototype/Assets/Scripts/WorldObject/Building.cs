using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : WorldObject {

	private Renderer objRenderer;
	private Color mainColor;
	private bool exit;

	private GameObject[] unitsInside;
	private int maxUnitsAllowed;
	private int buildingLevel;

	public bool buildingMenuOpened;

	private void Awake(){
		base.Awake ();
		objRenderer = GetComponent<Renderer> ();
		mainColor = objRenderer.material.color;
	}

	private void Update(){
		base.Update ();
	}

	private void OnMouseEnter(){
		exit = false;
	}

	private void OnMouseDown(){
		StartCoroutine(Blink());
		buildingMenuOpened = true;
	}

	private void OnMouseExit(){
		exit = true;
		objRenderer.material.color = mainColor;
	}

	private IEnumerator Blink() {
		float maxAlpha = 1f;
		float minAlpha = 0.4f;
		float alphaDelta = 0.01f;
		while(true){
			if (exit)
				yield break;
			if (objRenderer.material.color.a <= minAlpha || objRenderer.material.color.a >= maxAlpha)
				alphaDelta = -alphaDelta;
			objRenderer.material.SetColor("_Color", new Color(objRenderer.material.color.r,
				objRenderer.material.color.g,
				objRenderer.material.color.b,
				objRenderer.material.color.a + alphaDelta));
			yield return new WaitForSeconds(0.02f);
		}
	}

}