using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatistics : MonoBehaviour {

	public static event System.Action AddGradePenaltyEvent_Fighting;
	public static event System.Action RemoveGradePenaltyEvent_Fighting;

	public static LevelStatistics instance;

	private List<int> deadPlayerUnitsIds;
	private List<int> deadEnemyUnitsIds;

	private SelectionHandler sHandler;
	private bool fight;
	private bool alert;

	private RevealGrade rGrade;

	void Awake(){
		if (instance == null)
			instance = this;
		if (instance != this)
			Destroy (gameObject);

		sHandler = GameObject.FindObjectOfType<SelectionHandler> ();
		rGrade = GameObject.FindObjectOfType<RevealGrade> ();
	}

	void Start(){
		StartCoroutine (checkFighting ());
	}

	private IEnumerator checkFighting(){
		while (true) {
			foreach (Unit unit in sHandler.AllUnits) {
				if (!unit.Owner.IsHuman && unit.isAttacking () && !fight && !alert) {
					alert = true;
					rGrade.HandleInstantEvent (10);//Вас заметили
				}
				if (unit.Owner.IsHuman && unit.isAttacking ()) {
					if (!fight) {
						if(AddGradePenaltyEvent_Fighting != null) AddGradePenaltyEvent_Fighting ();
						fight = true;
						yield return new WaitForSeconds (1);
					}
				}
			}
			if (fight) {
				if (RemoveGradePenaltyEvent_Fighting != null)
					RemoveGradePenaltyEvent_Fighting ();
				fight = false;
				alert = false;
			}
			yield return new WaitForSeconds (1);
		}
	}

	public void AddToDeadList(Unit unit){
		if (unit.Owner.IsHuman)
			deadPlayerUnitsIds.Add (unit.UnitClassID);
		else
			deadEnemyUnitsIds.Add (unit.UnitClassID);
	}

	public void LevelCompleteEvent(bool success){
		//Show level complete panel
	}

	public List<int> DeadPlayerInitsIDs{
		get{ return deadPlayerUnitsIds;}
	} 

	public List<int> DeadEnemyInitsIDs{
		get{ return deadEnemyUnitsIds;}
	} 
}
