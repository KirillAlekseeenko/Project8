using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkAction : Action {
	

	public delegate void PerformPerkDelegate(Unit performer, Vector3? place = null, Unit target = null);
	public delegate bool IsReadyToPerformDelegate(Unit performer, Vector3? place = null, Unit target = null);
	public delegate void FinishDelegate(Unit performer, Vector3? place = null, Unit target = null);
	public delegate void InitializeDelegate(Unit performer, Vector3? place = null, Unit target = null);

	private PerformPerkDelegate performPerk;
	private IsReadyToPerformDelegate isReadyToPerform;
	private FinishDelegate finish;
	private InitializeDelegate initialize;

	private Unit target;
	private Vector3? place;

	public PerkAction(PerformPerkDelegate performPerk, IsReadyToPerformDelegate isReadyToPerform, FinishDelegate finish, InitializeDelegate initialize, Unit performer, Vector3? place = null, Unit target = null)
	{
		this.performPerk = performPerk;
		this.isReadyToPerform = isReadyToPerform;
		this.finish = finish;
		this.initialize = initialize;
		this.actionOwner = performer;
		this.target = target;
		this.place = place;
	}

	#region implemented abstract members of Action

	public override void Perform ()
	{
		initialize (actionOwner as Unit, place, target);
	}

	public override void Finish ()
	{
		finish (actionOwner as Unit, place, target);
	}

	public override ActionState State {
		get {
			if (isReadyToPerform (actionOwner as Unit, place, target)) {
				performPerk (actionOwner as Unit, place, target);
				return new ActionState (true, -1);
			} else {
				return new ActionState (false, -1);
			}
		}
	}

	#endregion
}
