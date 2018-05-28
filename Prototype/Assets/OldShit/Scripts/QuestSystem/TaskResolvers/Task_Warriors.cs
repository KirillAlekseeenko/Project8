using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Warriors : TaskResolver{

	protected enum Comparison{
		LESS,
		EQUALS,
		MORE
	};

	[SerializeField]
	protected Comparison comparison;
	[SerializeField]
	protected int necessaryNumber;
	[SerializeField]
	protected SelectionHandler sHandler;


	protected int num;

	void Start(){
		StartCoroutine (checkCondition ());
	}		

	protected int getNecessaryNumber (){
		int n = 0;
		foreach (Unit unit in sHandler.AllUnits) {
			//If not scientist not hacker and not agitator
			if (unit.UnitClassID != 1 && unit.UnitClassID != 3 && unit.UnitClassID != 4 && unit.UnitClassID != 5)
				n++;
		}
		return n;
	}

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
