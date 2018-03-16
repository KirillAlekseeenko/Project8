using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CrowdZone : MonoBehaviour {

	const float recruitingInterval = 0.5f;

	Dictionary<Player, RecruitmentData> players;

	HashSet<Unit> unitsInside;

	WaitForSeconds waitRecruitingInterval = new WaitForSeconds(recruitingInterval);

	Coroutine recruitmentCoroutine;

	[SerializeField] private Spawn attachedSpawn;

	[SerializeField] private Unit baseUnit;

	void Awake()
	{
		players = new Dictionary<Player, RecruitmentData> ();
		unitsInside = new HashSet<Unit> ();
	}

	void OnEnable()
	{
		Unit.Dead += onUnitDissapear;
		Unit.EnteredBuilding += onUnitDissapear;
	}

	void OnDisable()
	{
		Unit.Dead -= onUnitDissapear;
		Unit.EnteredBuilding -= onUnitDissapear;
	}

	void OnTriggerEnter(Collider collider)
	{
		var unit = collider.GetComponent<Unit> ();
		if (unit != null)
			unitsInside.Add (unit);
	}

	void OnTriggerExit(Collider collider)
	{
		var unit = collider.GetComponent<Unit> ();
		if (unit != null)
			unitsInside.Remove (unit);
	}

	private void onUnitDissapear(Unit unit)
	{
		var recruiter = unit.GetComponent<Recruiter> ();
		if (unitsInside.Contains (unit)) {
			unitsInside.Remove (unit);
			if (recruiter != null) {
				StopRecruiting (recruiter);
			}
		}
	}

	public void StartRecruiting(Recruiter recruiter)
	{
		var owner = recruiter.GetComponent<Unit> ().Owner;
		if (players.Count == 0) {
			recruitmentCoroutine = StartCoroutine (recruiting ());
		}
		if (!players.ContainsKey (owner)) {
			players.Add (owner, new RecruitmentData (owner));
		}
		players [owner].AddRecruiter (recruiter);
	}

	public void StopRecruiting(Recruiter recruiter)
	{
		var owner = recruiter.GetComponent<Unit> ().Owner;
		players [owner].RemoveRecruiter (recruiter); 
		if (players [owner].RecruiterCount == 0) {
			players.Remove (owner);
			if (players.Count == 0) {
				StopCoroutine (recruitmentCoroutine);
			}
		}
	}

	private IEnumerator recruiting()
	{
		while (true) {
			yield return waitRecruitingInterval;
			foreach (var recrData in players.Values) {
				if (recrData.ReadyToSpawn (enemiesInside (recrData.Player))) {
					var citizen = findCitizen ();
					if (citizen != null) {
						citizen.changeOwner (recrData.Player);
					} else {
						baseUnit.Owner = recrData.Player;
						attachedSpawn.SpawnUnit (baseUnit);
					}
				}
			}
		}
	}
		
	private bool enemiesInside(Player player)
	{
		foreach (var unit in unitsInside) {
			if (unit.Owner.isEnemy (player))
				return true;
		}
		return false;
	}

	private Unit findCitizen()
	{
		foreach (var unit in unitsInside) {
			if (unit.Owner.Citizen)
				return unit;
		}
		return null;
	}

	private class RecruitmentData
	{
		Player player;
		int recruitmentPower;

		int currentProgress;

		public RecruitmentData (Player player)
		{
			this.player = player;
			this.recruitmentPower = 0;
			RecruiterCount = 0;
		}

		public Player Player { get { return player; } }
		public int RecruiterCount { get; set; }
		
		public bool ReadyToSpawn (bool enemiesInside)
		{
			if (enemiesInside) {
				currentProgress = 0;
				return false;
			}

			currentProgress += recruitmentPower;
			if (currentProgress > 100) {  
				currentProgress = 0;
				return true;
			}
			return false;
		}

		public void AddRecruiter(Recruiter recruiter)
		{
			recruitmentPower += recruiter.RecruitPower;
			RecruiterCount++;
		}

		public void RemoveRecruiter(Recruiter recruiter)
		{
			recruitmentPower -= recruiter.RecruitPower;
			RecruiterCount--;
		}
	}
}
