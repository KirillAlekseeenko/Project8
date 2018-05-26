using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour {

	private bool menuOpened;
	private short clicks;

	private GameObject UIBuilding;
	private GameObject objBuilding;

	private GameObject incomeField;

	private GameObject scientistImage;
	private GameObject hackerImage;
	private GameObject warriorImage;

	private GameObject scientistsCount;
	private GameObject hackersCount;
	private GameObject warriorsCount;

	/*void Start () {
		UIBuilding = GameObject.Find ("BuildingPanel");
		incomeField = GameObject.Find ("ResourceWidget");

		scientistImage = GameObject.Find ("ScientistsImage");
		scientistImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 255);
		hackerImage = GameObject.Find ("HackersImage");
		hackerImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 255);
		warriorImage = GameObject.Find ("WarriorsImage");
		warriorImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 255);

		scientistsCount = GameObject.Find ("ScientistsCount");
		hackersCount = GameObject.Find ("HackersCount");
		warriorsCount = GameObject.Find ("WarriorsCount");
		MenuClose ();
	}

	private void FixedUpdate () {
		try{
			incomeField.GetComponent<Text>().text = objBuilding.GetComponent<Building>().Money.ToString ();
		}catch(Exception e){}
		if (Input.GetMouseButtonDown (0) && clicks < 2) {
			clicks++;
		}
		if (clicks > 1 && !RectTransformUtility.RectangleContainsScreenPoint(
			UIBuilding.GetComponent<RectTransform>(), 
			Input.mousePosition)) {
			clicks = 0;
			MenuClose ();
		}

		if(UIBuilding.activeSelf){
			UpdateUnitsInsideInfo ();
		}
	}

	private void UpdateUnitsInsideInfo(){
		if (objBuilding.GetComponent<Building>().ScientistsInside > 0)
			scientistImage.GetComponent<RawImage>().color = new Color(1, 1, 1, 1); 
		else
			scientistImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
		if (objBuilding.GetComponent<Building>().HackersInside > 0)
			hackerImage.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
		else
			hackerImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
		if (objBuilding.GetComponent<Building>().WarriorsInside > 0)
			warriorImage.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
		else
			warriorImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
		scientistsCount.GetComponent<Text>().text = "(" + objBuilding.GetComponent<Building>().ScientistsInside.ToString() + ")";
		hackersCount.GetComponent<Text>().text = "(" + objBuilding.GetComponent<Building>().HackersInside.ToString() + ")";
		warriorsCount.GetComponent<Text>().text = "(" + objBuilding.GetComponent<Building>().WarriorsInside.ToString() + ")"; 
	}

	public void MenuOpen(GameObject bld){
		objBuilding = bld;
		UIBuilding.SetActive (true);
		UpdateUnitsInsideInfo ();
	} 

	public void MenuClose(){
		UIBuilding.SetActive (false);
		try{
			objBuilding.GetComponent<Building>().IsSelected = false;
		}catch(Exception e){}
	}

	public void UpgradeBuilding(){
		objBuilding.GetComponent<Building>().CurrentLevel = objBuilding.GetComponent<Building>().CurrentLevel + 1;
	}
		
	public void RemoveAll(){
		objBuilding.GetComponent<Building>().RemoveAllUnits ();
	}

	public void BanishScientist(){
		objBuilding.GetComponent<Building> ().RemoveUnit (0);
	}

	public void BanishHacker(){
		objBuilding.GetComponent<Building> ().RemoveUnit (1);
	}

	public void BanishWarrior(){
		objBuilding.GetComponent<Building> ().RemoveUnit (2);
	}
	*/
}
