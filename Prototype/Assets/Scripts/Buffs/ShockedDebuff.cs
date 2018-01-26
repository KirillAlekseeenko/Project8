using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockedDebuff : Buff {

	private float multiplier = 1.5f;
	private int damage = 1;

	#region implemented abstract members of Buff

	protected override void addEffect ()
	{
		unit.SufferDamageMultiplier *= multiplier;
		StartCoroutine (damageCoroutine ());
	}

	protected override void removeEffect ()
	{
		StopCoroutine (damageCoroutine ());
		unit.SufferDamageMultiplier /= multiplier;
	}

	#endregion

	private IEnumerator damageCoroutine()
	{
		while (true) {
			unit.SufferDamage (damage);
			yield return new WaitForSeconds (1.0f);
		}
	}


}
