using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour {	
	// Use this for initialization
	
	private string taskNameText = null;
	private string taskObjText = null;
	public GameObject taskNamePref;
	public GameObject taskObjPref;
	protected bool missionObject;
	private GameObject newTaskName;
	
	private GameObject newTaskObject;
	protected string objectTag;
	private int objNumber;
	public int ObjNumber{
		set{
			objNumber = value;
		}
	}
	public string TaskNameText{
		set{
			taskNameText = value;
		}
	}
	void Start(){
				
	}
	void OnGUI(){
		if(taskNameText!="" && taskNameText!=null){	
			
			newTaskName = Instantiate(taskNamePref, parent:gameObject.transform) as GameObject;
			newTaskObject = Instantiate(taskObjPref, parent: gameObject.transform) as GameObject;
			newTaskObject.GetComponent<Text>().text = "1/5";
			newTaskName.GetComponent<Text>().text = taskNameText;
			taskNameText = null;
		}
		
	}
}
