using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Kill_Concrete_Enemies : TaskResolver {

	[SerializeField]
	private List<GameObject> enemiesToBeMurdered;

	void Start(){
		StartCoroutine (checkCondition ());
	}

	protected int getNecessaryNumber(){return 0;}

	protected IEnumerator checkCondition(){
		while(true){
			foreach (GameObject unit in enemiesToBeMurdered) {
				if (unit.GetComponent<Unit>().HP < 0)
					enemiesToBeMurdered.Remove (unit);
			}
			//Если все враги из этого списка были убиты, то задание выполнено
			if (enemiesToBeMurdered.Count == 0)
				break;
			yield return new WaitForSeconds (1);
		}
		completeTask ();
		yield return null;
	}

}
