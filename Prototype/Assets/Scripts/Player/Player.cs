using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

	Diplomacy diplomacy;

	[SerializeField] private string username;  // задел под мультиплеер ))0)
	[SerializeField] private bool isHuman;
	[SerializeField] private bool citizen;
	[SerializeField] private Team team;
	[SerializeField] private Color color;

	private static Player humanPlayer;

	public static Player HumanPlayer { get { return humanPlayer; } }

	void Awake()
	{
		diplomacy = transform.parent.GetComponent<Diplomacy> ();
		if (isHuman)
			humanPlayer = this;
	}

	public bool IsHuman { get { return isHuman; } }
	public bool Citizen { get { return citizen; } }
	public Color Color { get { return color; } }

	public bool isEnemy(Player player)
	{
		return diplomacy.getRelation (team, player.team) == Relation.Enemy;
	}

	public bool isFriend(Player player)
	{
		return diplomacy.getRelation (team, player.team) == Relation.Friend;
	}
}

