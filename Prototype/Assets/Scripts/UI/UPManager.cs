using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UPManager : MonoBehaviour {
		

	
	private float distance;
	public GameObject UnitIconPref;
	
	
	private GameObject newIcon;
	
	
	
	
	void Start () {		
			
		UnitIconPref.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
	
	}
	void OnEnable(){

		SelectionHandler.OnUnitSelected += AddIcon;
		
		
		SelectionHandler.OnDragCancel +=DeleteIcon;
	}
	void OnDisable(){
		SelectionHandler.OnUnitSelected -= AddIcon;
		
		
		SelectionHandler.OnDragCancel -=DeleteIcon;
	}
	
	public void DeleteIcon(){
		foreach(Transform child in gameObject.transform)			
			Destroy(child.gameObject);		
		
	}
	public void DeleteIcon(int unitClassID){
		
		for(int childCount=gameObject.transform.childCount; childCount > 0; childCount--){
			if(gameObject.transform.GetChild(childCount-1).name == unitClassID.ToString()){
				if(int.Parse(gameObject.transform.GetChild(childCount-1).GetChild(1).GetComponent<Text>().text) > 1)
					gameObject.transform.GetChild(childCount-1).GetChild(1).GetComponent<Text>().text = (int.Parse(gameObject.transform.GetChild(childCount-1).GetChild(1).GetComponent<Text>().text) -1).ToString();	
				else
					Destroy(gameObject.transform.GetChild(childCount-1).gameObject);
			}
		}
	}
	
	public void AddIcon(int unitClassID, Sprite UnitIconSprite){
		bool isEqual = false;	
		foreach(Transform child in transform){
			if(child.gameObject.name == unitClassID.ToString()){
				
				child.GetChild(1).GetComponent<Text>().text = (int.Parse(child.GetChild(1).GetComponent<Text>().text) + 1).ToString();
				isEqual = true;
			}			
		}
		if(isEqual==false){
			newIcon = Instantiate(UnitIconPref, parent:gameObject.transform) as GameObject;	
			newIcon.name = (unitClassID).ToString();		
			newIcon.transform.GetChild(0).GetComponent<Image>().sprite = UnitIconSprite;
			newIcon.transform.GetChild(1).GetComponent<Text>().text = "1";
			newIcon.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.5f);
			newIcon.GetComponent<RectTransform>().pivot = new Vector2(0,0);
		}
		isEqual=false;
	}
	
}
