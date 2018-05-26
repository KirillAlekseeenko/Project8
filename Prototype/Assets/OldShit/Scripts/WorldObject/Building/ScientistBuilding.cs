using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistBuilding : Building {

	[SerializeField]
	private float sciPointsUpdateTime;
	[SerializeField]
	private int sciPointsPerUpdate;

	// Use this for initialization
	private void Start () {
		base.Start ();
		StartCoroutine (increaseScientistRes());
	}

	private IEnumerator increaseScientistRes(){
		while (true) {
			yield return new WaitForSeconds (sciPointsUpdateTime);
			Player.HumanPlayer.ResourcesManager.AddSciencePoints (sciPointsPerUpdate);
		}
	}
}
