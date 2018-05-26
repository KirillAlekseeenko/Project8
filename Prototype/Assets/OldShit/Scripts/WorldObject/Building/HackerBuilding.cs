using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerBuilding : Building {

	[SerializeField]
	private float moneyUpdateTime;
	[SerializeField]
	private int moneyPerUpdate;

	private void Start () {
		base.Start ();
		StartCoroutine (increaseMoney());
	}

	private IEnumerator increaseMoney(){
		while (true) {
			yield return new WaitForSeconds (moneyUpdateTime);
			Player.HumanPlayer.ResourcesManager.AddSciencePoints (moneyPerUpdate);
		}
	}
}
