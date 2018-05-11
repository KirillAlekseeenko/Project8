using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Battle : MonoBehaviour{
	//Result: 1-attacker wins, 0-defender wins
	private delegate void EndBattle(int result, Player newOwner, List<Unit> units);
	private event EndBattle endBattleEvent;

	private int roundChecker;

	private List<Unit> attackers;
	private List<Unit> defenders;

	private Player buildingOwner;
	private Player attackerOwner;

	private bool startBattle;

	private int attackersPower;
	private int defendersPower;
	private ArrayList defendersDamageDistribution;
	private ArrayList attackersDamageDistribution;

	private void Awake(){
		Building thisBuilding = gameObject.GetComponent <Building>();
		endBattleEvent = new EndBattle(thisBuilding.EndAutoBattle);

		attackers = new List<Unit> ();
		defenders = new List<Unit> ();

		attackersDamageDistribution = new ArrayList();
		defendersDamageDistribution = new ArrayList ();
	}

	private void PlayRound(){
		int i;

		attackerOwner = attackers [0].Owner;

		attackersPower = 0;
		defendersPower = 0;
		attackersDamageDistribution.Clear ();
		defendersDamageDistribution.Clear ();

		foreach (Unit att in attackers) {
			//Элемент случайности - увеличить или уменьшить силу атаки в этом раунде
			attackersPower += (int)(att.RangeAttack * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));
			attackersPower += (int)(att.MeleeAttack * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));;
		}
			
		foreach (Unit def in defenders) {
			defendersPower += (int)(def.RangeAttack * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));
			defendersPower += (int)(def.MeleeAttack * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));;
		}

		for (i = 0; i < defenders.Count - 1; i++)
			defendersDamageDistribution.Add(UnityEngine.Random.Range (0, attackersPower));
		defendersDamageDistribution.Add (0);
		defendersDamageDistribution.Add (attackersPower);
		defendersDamageDistribution.Sort ();

		for (i = 0; i < attackers.Count - 1; i++)
			attackersDamageDistribution.Add (UnityEngine.Random.Range (0, defendersPower));
		attackersDamageDistribution.Add (0);
		attackersDamageDistribution.Add (defendersPower);

		i = 0;
		foreach (Unit att in attackers) {
			att.SufferDamage((int)attackersDamageDistribution[i + 1] - (int)attackersDamageDistribution[i]);
			i++;
		}
		i = 0;
		foreach (Unit def in defenders) {
			def.SufferDamage((int)defendersDamageDistribution[i + 1] - (int)defendersDamageDistribution[i]);
			i++;
		}
	}

	private void checkRound(){
		roundChecker++;
		PlayRound ();
		attackers.RemoveAll (item => item.HP <= 0);
		defenders.RemoveAll (item => item.HP <= 0);
		if (attackers.Count > 0 && defenders.Count > 0)
			Invoke ("checkRound", 0.5f);
		else {
			if (defenders.Count <= 0) {
				endBattleEvent (1, attackerOwner, attackers);	
			} else {
				endBattleEvent (0, buildingOwner, defenders);
			}
			attackers.Clear ();
			defenders.Clear ();
		}
	}

	public void AddToBattlePlan(Unit unit){
		if (unit.Owner == buildingOwner)
			defenders.Add (unit);
		else
			attackers.Add (unit);
	}

	public Player BuildingOwner{
		get{return buildingOwner;}
		set{buildingOwner = value;}
	}

	public void StartBattle(){
		roundChecker = 0;
		checkRound ();
	}


}
