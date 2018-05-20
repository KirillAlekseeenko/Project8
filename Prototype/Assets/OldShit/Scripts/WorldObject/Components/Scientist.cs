using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour {

	// mining

	[SerializeField] private int income;

	//healing

	[SerializeField] private int healAmount;
	[SerializeField] private float healPerSecond;
	[SerializeField] private float healRadius;

	[SerializeField] private ParticleSystem healTrace;

	private float healReloadTime;
	private float reloadCounter;

	public float HealRadius {
		get {
			return healRadius;
		}
	}

	void Start()
	{
		healReloadTime = 1 / healPerSecond;
	}

	void Update()
	{
		reloadCounter += Time.deltaTime;
	}

	public void PerformHeal(Unit friendlyUnit)
	{
		if (isReadyToHeal ()) {
			friendlyUnit.Heal (healAmount);
			spawnTrace (friendlyUnit);
		}
	}
	private void spawnTrace(Unit friendlyUnit)
	{
		Quaternion rotation = Quaternion.LookRotation (Utils.Direction(GetComponent<Unit>(), friendlyUnit));// particle's rotation
		ParticleSystem trace = Instantiate (healTrace.gameObject, transform.position, Quaternion.identity, transform ).GetComponent<ParticleSystem>();

		float length = Utils.Distance (GetComponent<Unit> (), friendlyUnit); // length of the particle
		trace.startLifetime = length / trace.main.startSpeed.constant;
		trace.transform.rotation = rotation;
	}
	private bool isReadyToHeal()
	{
		bool result = (reloadCounter >= healReloadTime);
		if (result)
			reloadCounter = 0;
		return result;
	}
}
