using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerBuilding : Building {

	[SerializeField]
	private float moneyUpdateTime;
	[SerializeField]
	private int oneHackerMoneyPerUpdate;

	private float efficiency;

	private void Start () {
		base.Start ();
		StartCoroutine (increaseMoney());
	}

	private IEnumerator increaseMoney(){
		while (true) {
			yield return new WaitForSeconds (moneyUpdateTime);
			if (techTree.FindTech (3).bought) {
				efficiency = 0.2f;
			}
			if (techTree.FindTech (4).bought) {
				efficiency = 0.4f;
			}
			Player.HumanPlayer.ResourcesManager.AddMoney ((int)((oneHackerMoneyPerUpdate * HackersInside.Count) + (oneHackerMoneyPerUpdate * HackersInside.Count) * efficiency));
		}
	}
}
