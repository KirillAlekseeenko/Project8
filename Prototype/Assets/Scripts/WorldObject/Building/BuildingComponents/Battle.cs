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
	private Player attackerowner;

	private bool startBattle;

	private int attackersRangePower;
	private int defendersRangePower;

	private int attackersMeleePower;
	private int defendersMeleePower;

	//Сколько range damage получает каждый атакующий за этот раунд
	private ArrayList attackersRangeDamadeDistribution;
	//Сколько range damage получает каждый защитник за этот раунд
	private ArrayList defendersRangeDamadeDistribution;

	//The same with Melee attack
	private ArrayList attackersMeleeDamadeDistribution;

	private ArrayList defendersMeleeDamadeDistribution;


	private void Awake(){
		Building thisBuilding = gameObject.GetComponent <Building>();
		endBattleEvent = new EndBattle(thisBuilding.EndAutoBattle);

		attackers = new List<Unit> ();
		defenders = new List<Unit> ();

		attackersRangeDamadeDistribution = new ArrayList ();
		defendersRangeDamadeDistribution = new ArrayList ();

		attackersMeleeDamadeDistribution = new ArrayList ();
		defendersMeleeDamadeDistribution = new ArrayList ();
	}

	private void PlayRound(){
		int i;

		attackersRangePower = 0;
		defendersRangePower = 0;
		attackersMeleePower = 0;
		defendersMeleePower = 0;
		attackersRangeDamadeDistribution.Clear ();
		defendersRangeDamadeDistribution.Clear ();
		attackersMeleeDamadeDistribution.Clear ();
		defendersMeleeDamadeDistribution.Clear ();

		foreach (Unit att in attackers) {
			attackersRangePower += att.RangeAttack;
			attackersMeleePower += att.MeleeAttack;
		}
		//Элемент случайности - увеличить или уменьшить силу атаки в этом раунде
		attackersRangePower = (int)(attackersRangePower * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));
		attackersMeleePower = (int)(attackersMeleePower * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));

		foreach (Unit def in defenders) {
			defendersRangePower += def.RangeAttack;
			defendersMeleePower += def.MeleeAttack;
		}
		defendersRangePower = (int)(defendersRangePower * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));
		defendersMeleePower = (int)(defendersMeleePower * (1 - UnityEngine.Random.Range (-0.3f, 0.3f)));

		for (i = 0; i < defenders.Count - 1; i++) {
			defendersRangeDamadeDistribution.Add (UnityEngine.Random.Range (0, attackersRangePower));
			defendersMeleeDamadeDistribution.Add (UnityEngine.Random.Range (0, attackersMeleePower));
		}
		defendersMeleeDamadeDistribution.Add (0);
		defendersMeleeDamadeDistribution.Add (attackersMeleePower);
		defendersMeleeDamadeDistribution.Sort ();
		defendersRangeDamadeDistribution.Add (0);
		defendersRangeDamadeDistribution.Add (attackersRangePower);
		defendersRangeDamadeDistribution.Sort ();


		for (i = 0; i < attackers.Count - 1; i++) {
			attackersRangeDamadeDistribution.Add (UnityEngine.Random.Range (0, defendersRangePower));
			attackersMeleeDamadeDistribution.Add (UnityEngine.Random.Range (0, defendersMeleePower));
		}
		attackersMeleeDamadeDistribution.Add (0);
		attackersMeleeDamadeDistribution.Add (defendersMeleePower);
		attackersMeleeDamadeDistribution.Sort ();
		attackersRangeDamadeDistribution.Add (0);
		attackersRangeDamadeDistribution.Add (defendersRangePower);
		attackersRangeDamadeDistribution.Sort ();

		i = 0;
		//Debug.Log (attackersDamadeDistribution[0]);
		foreach (Unit att in attackers) {
			att.SufferDamage ((int)attackersRangeDamadeDistribution[i + 1] - (int)attackersRangeDamadeDistribution[i]);
			att.SufferDamage ((int)attackersMeleeDamadeDistribution[i + 1] - (int)attackersMeleeDamadeDistribution[i]);
		}
		foreach (Unit def in defenders) {
			def.SufferDamage ((int)defendersRangeDamadeDistribution[i + 1] - (int)defendersRangeDamadeDistribution[i]);
			def.SufferDamage ((int)defendersMeleeDamadeDistribution[i + 1] - (int)defendersMeleeDamadeDistribution[i]);
		}
	}

	private void checkRound(){
		roundChecker++;
		PlayRound ();
		attackers.RemoveAll (item => item.HP < 0);
		defenders.RemoveAll (item => item.HP < 0);
		Debug.Log (attackers.Count);
		Debug.Log (defenders.Count);
		if (attackers.Count > 0 && defenders.Count > 0)
			Invoke ("checkRound", 0.5f);
		else {
			if (defenders.Count <= 0) {
				Debug.Log ("All defenders died");
				endBattleEvent (1, attackers[0].Owner, attackers);	
			}
		}
	}

	public void AddToBattlePlan(GameObject unit){
		if (unit.GetComponent<Unit>().Owner == buildingOwner)
			defenders.Add (unit.GetComponent<Unit>());
		else
			attackers.Add (unit.GetComponent<Unit>());
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
