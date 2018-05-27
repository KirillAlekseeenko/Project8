using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Kill_Concrete_Enemies : TaskResolver {

	[SerializeField]
	private List<Unit> enemiesToBeMurdered;

	void Start(){
		StartCoroutine (checkCondition ());
	}

	protected int getNecessaryNumber(){return 0;}

	protected IEnumerator checkCondition(){
		while(true){
			foreach (Unit unit in enemiesToBeMurdered) {
				if (unit.HP < 0)
					enemiesToBeMurdered.Remove (unit);
			}
			//Если все враги из этого списка были убиты, то задание выполнено
			if (enemiesToBeMurdered.Count == 0)
				break;
		}
		completeTask ();
		yield return null;
	}

}
