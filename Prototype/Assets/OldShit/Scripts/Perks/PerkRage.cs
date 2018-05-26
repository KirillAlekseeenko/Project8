using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkRage : Perk {

	[SerializeField] private float time;

	#region implemented abstract members of Perk

	protected override void initialize (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		if (performer == null)
			Debug.LogError ("missing parameter");
		return;
	}

	protected override void derivedPerform (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		Buff.AddBuff<RageBuff>(performer, time);
	}

	protected override bool isReadyToPerform (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		return true;
	}

	protected override void finish (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		return;
	}

	public override PerkType Type
    {
        get
        {
            return PerkType.Itself;
        }
    }

	#endregion


}
