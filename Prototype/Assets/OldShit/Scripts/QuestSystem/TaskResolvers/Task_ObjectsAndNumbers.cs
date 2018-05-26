using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.Xml.Linq;

public class Task_ObjectsAndNumbers : TaskResolver {

	protected enum Comparison{
		LESS,
		EQUALS,
		MORE
	};

	[SerializeField]
	protected Comparison comparison;
	[SerializeField]
	protected int checkedUnitID;
	[SerializeField]
	protected int necessaryNumber;

	protected int num;

	void Start(){
		StartCoroutine (checkCondition ());
	}

	protected virtual int getNecessaryNumber(){return 0;}

	protected IEnumerator checkCondition(){
		while(true){
			num = getNecessaryNumber ();
			if (comparison == Comparison.EQUALS && num == necessaryNumber)
				break;
			else if (comparison == Comparison.LESS && num < necessaryNumber)
				break;
			else if (comparison == Comparison.MORE && num > necessaryNumber)
				break;
			yield return new WaitForSeconds (2);
		}
		completeTask ();
		yield return null;
	}
}
