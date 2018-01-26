using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrowdZone : MonoBehaviour {

	int unitsInside = 0;

	[SerializeField] private float recruitmentTime;

	private bool isRecruiting = false;

	[SerializeField] private Spawn attachedSpawn;

	[SerializeField] private Unit baseUnit;

	private float time = 0;


	public void unitEntered()
	{
		unitsInside++;
		if (isRecruiting == false)
			isRecruiting = true;
	}
	public void unitLeft()
	{
		unitsInside--;
		if (unitsInside == 0) {
			isRecruiting = false;
			time = 0;
		}
	}

	void Update()
	{
		if (isRecruiting) {
			time += Time.deltaTime;
			var actualRecruitmentTime = (unitsInside == 0) ? Single.PositiveInfinity : recruitmentTime / unitsInside;
			if (time >= actualRecruitmentTime) {
				attachedSpawn.spawnUnit (baseUnit);
				time = 0;
			}
		}
	}

}
