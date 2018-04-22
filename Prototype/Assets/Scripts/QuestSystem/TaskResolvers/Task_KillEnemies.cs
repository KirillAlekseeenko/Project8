using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework.Constraints;

public class Task_KillEnemies : Task_ObjectsAndNumbers {

	protected override int getNecessaryNumber (){
		return LevelStatistics.instance.DeadEnemyInitsIDs.FindAll (x => x == checkedUnitID).Count;
	}
}
