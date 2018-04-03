using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTask : MonoBehaviour {
	
	protected string taskText;
	public GameObject taskPanel;
	protected bool missionObject;
	protected string objectTag;

	
	void OnGui(){
		
		GUI.Label(new Rect(130,-18,240,25),taskText);
		
	}
}
