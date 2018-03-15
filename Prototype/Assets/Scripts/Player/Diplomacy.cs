using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diplomacy : MonoBehaviour {

	[SerializeField] private DiplomacyData diplomacyData;

	public Relation getRelation(Team team1, Team team2)
	{
		if (team1 < team2)
			return getRelation (team2, team1);
		if (team1 == team2)
			return Relation.Friend;

		return diplomacyData [(int)team1] [(int)team2];
	}

}

public enum Relation {Neutral, Enemy, Friend}

public enum Team {One = 0, Two = 1, Three = 2, Four = 3, Five = 4, Six = 5, Seven = 6, Eight = 7};
