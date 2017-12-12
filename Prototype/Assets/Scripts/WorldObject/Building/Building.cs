using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : WorldObject {

	private Renderer objRenderer;
	private Color mainColor;
	private bool exit;

	private HashSet<GameObject> unitsInside;
	private int buildingLevel;

	private bool buildingMenuOpened;

	private GameObject UIPanel;
	private GameObject incomeField;
	private GameObject[] rawImages;

	private int resourcesCount;

	public int increaseResourcesByUnit;
	public int maxUnitsAllowed;

	private void Awake(){
		base.Awake ();

		objRenderer = GetComponent<Renderer> ();
		mainColor = objRenderer.material.color;

		unitsInside = new HashSet<GameObject> ();

		resourcesCount = 0;
	}

	private void Start(){
		UIPanel = GameObject.Find ("BuildingPanel");
		incomeField = GameObject.Find ("ResourceWidget");
		rawImages = GameObject.FindGameObjectsWithTag ("UIPictures");
		foreach(GameObject obj in rawImages){
			obj.SetActive(false);
		}
		MenuClose ();
	}

	private void Update(){
		base.Update ();
	}

	private void OnMouseDown(){
		exit = false;
		StartCoroutine(Blink());
		MenuOpen();
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

	private IEnumerator IncreaseResources() {
		while(true){
			if (unitsInside.Count == 0)
				yield break;
			resourcesCount += unitsInside.Count * increaseResourcesByUnit;
			incomeField.GetComponent<Text>().text = resourcesCount.ToString ();
			yield return new WaitForSeconds(1);
		}
	}

	public void AddUnit(GameObject unit){
		if (unitsInside.Count < maxUnitsAllowed) {
			rawImages[rawImages.Length - 1 - unitsInside.Count].SetActive(true);
			unitsInside.Add (unit);
			unit.SetActive (false);
			if (unitsInside.Count == 1)
				StartCoroutine (IncreaseResources ());
		}
	}

	public void RemoveUnit(GameObject unit){
		unitsInside.Remove (unit);
	}

	public void RemoveAllUnits(){
		for(int i = 0; i < 5; i++)
		{
			rawImages[i].SetActive(false);
		}
		foreach(GameObject unit in unitsInside){
			unit.SetActive (true);
			unit.transform.position = new Vector3(unit.transform.position.x, 
												  unit.transform.position.y + 0.5f,
												  unit.transform.position.z);
		}
		unitsInside.Clear ();
	}

	//UI
	public void MenuOpen(){
		UIPanel.SetActive (true);
	} 

	public void MenuClose(){
		UIPanel.SetActive (false);
		exit = true;
		objRenderer.material.color = mainColor;
	}
}