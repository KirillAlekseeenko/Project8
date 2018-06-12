using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_KillEnemies : Task_ObjectsAndNumbers {

	protected override int getNecessaryNumber (){
		return LevelStatistics.instance.DeadEnemyInitsID.FindAll (x => x == checkedUnitID).Count;
	}
}
