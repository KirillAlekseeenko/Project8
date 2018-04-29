using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatistics : MonoBehaviour {

	public static LevelStatistics instance;

	private List<int> deadPlayerUnitsIds;
	private List<int> deadEnemyUnitsIds;

	void Awake(){
		if (instance == null)
			instance = this;
		if (instance != this)
			Destroy (gameObject);
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
