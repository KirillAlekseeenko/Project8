using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour {

    protected BuffParameters parameters;
	protected float time;
	protected Unit unit;
	protected ParticleSystem buffParticle;

	public static Buff AddBuff<T> (Unit unit, float time, BuffParameters parameters = null) where T : Buff
	{
		var buff = unit.gameObject.AddComponent<T> ();
		buff.time = time;
		buff.unit = unit;
		buff.buffParticle = null;
        buff.parameters = parameters ?? new BuffParameters();
		return buff;
	}

	protected abstract void addEffect ();
	protected abstract void removeEffect ();
    protected abstract void init();

	void Start()
	{
        init();
		StartCoroutine (lifeCycle());
	}

	private IEnumerator lifeCycle()
	{
		addEffect ();
		yield return new WaitForSeconds (time);
		removeEffect ();
		Destroy (this, time);
	}

}
