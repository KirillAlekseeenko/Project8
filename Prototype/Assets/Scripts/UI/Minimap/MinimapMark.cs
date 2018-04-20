using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMark : MonoBehaviour {

	
	private GameObject minimapMark;
	public GameObject markPref;
	
	void OnEnable(){
		Unit.OnStart +=createMark;
		FieldOfViewHandler.OnUnitHide += markStatus;		
	}
	void OnDisable(){
		Unit.OnStart -=createMark;
		FieldOfViewHandler.OnUnitHide -= markStatus;
	}
	public void createMark(GameObject unit){
		

		minimapMark = Instantiate(markPref, unit.transform) as GameObject;
		minimapMark.transform.SetSiblingIndex(0);
		minimapMark.transform.localPosition = new Vector3(0, 5, 0);
		minimapMark.transform.localScale = new Vector3(2,2,2);
		minimapMark.GetComponent<MeshRenderer>().material = unit.GetComponent<MeshRenderer>().material;
		minimapMark.GetComponent<MeshRenderer>().material.color = unit.GetComponent<Unit>().Owner.Color;
	
	}
	public void markStatus(GameObject unit, bool isHide){
		if(isHide==true)
			unit.transform.GetChild(0).gameObject.SetActive(false);
		else
			unit.transform.GetChild(0).gameObject.SetActive(true);
	}
}
