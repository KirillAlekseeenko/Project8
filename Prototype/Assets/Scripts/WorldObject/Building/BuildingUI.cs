using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour {

	private GameObject UIBuilding;
	private GameObject objBuilding;
	private Building buildingScript;

	private GameObject incomeField;

	private GameObject scientistImage;
	private GameObject hackerImage;
	private GameObject warriorImage;

	private GameObject scientistsCount;
	private GameObject hackersCount;
	private GameObject warriorsCount;

	void Start () {
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
			incomeField.GetComponent<Text>().text = buildingScript.Money.ToString ();
		}catch(Exception e){
		}
	}

	public void MenuOpen(GameObject bld){
		objBuilding = bld;
		buildingScript = bld.GetComponent<Building>();
		UIBuilding.SetActive (true);
		scientistsCount.GetComponent<Text>().text = "(" + buildingScript.ScientistsInside.ToString() + ")";
		hackersCount.GetComponent<Text>().text = "(" + buildingScript.HackersInside.ToString() + ")";
		warriorsCount.GetComponent<Text>().text = "(" + buildingScript.WarriorsInside.ToString() + ")";
	} 

	public void MenuClose(){
		UIBuilding.SetActive (false);
		try{
			buildingScript.IsSelected = false;
		}catch(Exception e){}
	}

	public void AddUnit(GameObject bld, GameObject unit){
		objBuilding = bld;
		buildingScript = bld.GetComponent<Building>();

		if (unit.gameObject.GetComponent<Scientist>() != null) {
			scientistImage.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
			scientistsCount.GetComponent<Text>().text = "(" + buildingScript.ScientistsInside.ToString() + ")";
		} 
		else if (unit.gameObject.GetComponent<Hacker>() != null) {
			hackerImage.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
			hackersCount.GetComponent<Text>().text = "(" + buildingScript.HackersInside.ToString() + ")";
		}
		else if (unit.gameObject.GetComponent<Unit>() != null) {
			warriorImage.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
			warriorsCount.GetComponent<Text>().text = "(" + buildingScript.WarriorsInside.ToString() + ")";
		}
	}

	public void RemoveAll(){
		scientistImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 255);
		scientistsCount.GetComponent<Text>().text = "(0)";
		hackerImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 255);
		hackersCount.GetComponent<Text>().text = "(0)";
		warriorImage.GetComponent<RawImage>().color = new Color(0, 0, 0, 255);
		warriorsCount.GetComponent<Text>().text = "(0)";
		buildingScript.RemoveAllUnits ();
	}
}
