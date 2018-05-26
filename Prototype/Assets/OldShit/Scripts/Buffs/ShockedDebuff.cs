using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockedDebuff : Buff {

	private float multiplier = 1.5f;
	private int damage = 1;

	private Coroutine damageCoroutine;

	#region implemented abstract members of Buff

	protected override void addEffect ()
	{
		buffParticle = Instantiate (BuffInfo.Instance.ShockedParticle, gameObject.transform).GetComponent<ParticleSystem>();
		unit.SufferDamageMultiplier *= multiplier;
		damageCoroutine = StartCoroutine (sufferDamage ());
	}

	protected override void removeEffect ()
	{
		StopCoroutine (damageCoroutine);
		unit.SufferDamageMultiplier /= multiplier;
		if(buffParticle != null)
			Destroy (buffParticle.gameObject);
	}

	#endregion

	private IEnumerator sufferDamage()
	{
		while (true) {
			unit.SufferDamage (damage);
			yield return new WaitForSeconds (1.0f);
		}
	}


}
