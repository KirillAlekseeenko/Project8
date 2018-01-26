using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Perk : MonoBehaviour {
	[SerializeField] private string perkName;

	public string Name {
		get {
			return perkName;
		}
	}

	public abstract PerkType Type { get; }
	public abstract bool isReadyToFire{ get; }
		
	public abstract void Run (Unit performer, Vector3? place = null, Unit target = null);

	protected abstract void initialize (Unit performer, Vector3? place = null, Unit target = null);
	protected abstract void perform (Unit performer, Vector3? place = null, Unit target = null);
	protected abstract bool isReadyToPerform (Unit performer, Vector3? place = null, Unit target = null);
	protected abstract void finish (Unit performer, Vector3? place = null, Unit target = null);

	public override bool Equals (object other)
	{
		if (other == null || !(other is Perk))
			return false;
		return (other as Perk).perkName == perkName;
	}
}

public enum PerkType{Itself, Ground, Target};
