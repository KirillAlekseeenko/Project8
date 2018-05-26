using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRecieve : MonoBehaviour {
	
	public GameObject taskMark;
	public GameObject taskPanel;
	
	private string taskText = "Go to point";
	private int objNumber;
	private bool isEnter = false;	
	
	void Start(){
		
	}
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.GetComponent<Unit>() != null && isEnter == false){
			isEnter = true;
			taskMark.SetActive(true);			
		    taskPanel.GetComponent<TaskDisplay>().TaskNameText = taskText;		
			taskPanel.GetComponent<TaskDisplay>().ObjNumber = objNumber;
			
		}
		
	}
}
