using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diplomacy : MonoBehaviour {

	private Relation[][] relations;

	void Awake()
	{
		relations = new Relation[6][];
		for (int i = 0; i < 6; i++) {
			relations [i] = new Relation[6 - i];
		}

		relations [(int)Team.One] [(int)Team.Two] = Relation.Enemy;
		relations [(int)Team.One] [(int)Team.Three] = Relation.Neutral;
		relations [(int)Team.One] [(int)Team.Four] = Relation.Enemy;

		relations [(int)Team.Two] [(int)Team.Three] = Relation.Neutral;
		relations [(int)Team.Two] [(int)Team.Four] = Relation.Enemy;

		relations [(int)Team.Three] [(int)Team.Four] = Relation.Neutral;
	}

	public Relation getRelation(Team team1, Team team2)
	{
		if (team1 > team2)
			return getRelation (team2, team1);
		if (team1 == team2)
			return Relation.Friend;

		return relations [(int)team1] [(int)team2];
	}

}

public enum Relation {Enemy, Friend, Neutral}

public enum Team {One = 0, Two = 1, Three = 2, Four = 3, Five = 4, Six = 5};
