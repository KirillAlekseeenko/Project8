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

	[SerializeField] private ParticleSystem healEffect;

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

			
			// HealEffect
		}
	}
	private bool isReadyToHeal()
	{
		bool result = (reloadCounter >= healReloadTime);
		if (result)
			reloadCounter = 0;
		return result;
	}
}
