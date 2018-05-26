using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommCenter : MonoBehaviour {

	[SerializeField]
	private float HackedTime;

	private bool isHacked;

	private void ResetHacking(){
		isHacked = false;
	}
		
	public bool Hacked{
		get{return isHacked;}
		set{
			if(!isHacked)
				Invoke ("ResetHacking", HackedTime);
			isHacked = value;}
	}

}
