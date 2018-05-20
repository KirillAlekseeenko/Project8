using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBuff : Buff {

	private float multiplier = 2.0f;
	private float lifeStealModifier = 0.2f;

	#region implemented abstract members of Buff

	// need some aura

	protected override void addEffect ()
	{
		buffParticle = Instantiate (BuffInfo.Instance.RagedParticle, gameObject.transform).GetComponent<ParticleSystem>();
		unit.SufferDamageMultiplier *= multiplier;
		unit.AttackSpeedModifier *= multiplier;
		unit.LifeSteal = lifeStealModifier;
	}

	protected override void removeEffect ()
	{
		unit.SufferDamageMultiplier /= multiplier;
		unit.AttackSpeedModifier /= multiplier;
		unit.LifeSteal = 0;
		if(buffParticle != null)
			Destroy (buffParticle.gameObject);
	}

	#endregion
}
