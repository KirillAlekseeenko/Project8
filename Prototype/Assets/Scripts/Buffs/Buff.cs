using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour {

	protected float time;
	protected Unit unit;

	public static Buff AddBuff<T> (Unit unit, float time) where T : Buff
	{
		var buff = unit.gameObject.AddComponent<T> ();
		buff.time = time;
		buff.unit = unit;
		return buff;
	}

	protected abstract void addEffect ();
	protected abstract void removeEffect ();

	void Start()
	{
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
