using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistBuilding : Building {

	[SerializeField]
	private float sciPointsUpdateTime;
	[SerializeField]
	private int oneScientistPointsPerUpdate;

	private float efficiency;

	private void Start () {
		base.Start ();
		StartCoroutine (increaseScientistRes());
	}

	private IEnumerator increaseScientistRes(){
		while (true) {
			yield return new WaitForSeconds (sciPointsUpdateTime);
			if (techTree.FindTech (1).bought) {
				efficiency = 0.2f;
			}
			if (techTree.FindTech (2).bought) {
				efficiency = 0.4f;
			}
			Player.HumanPlayer.ResourcesManager.AddSciencePoints ((int)((oneScientistPointsPerUpdate * ScientistsInside.Count) + (oneScientistPointsPerUpdate * ScientistsInside.Count) * efficiency));
		}
	}
}
