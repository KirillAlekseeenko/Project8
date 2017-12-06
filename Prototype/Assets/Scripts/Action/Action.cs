using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action {

	protected WorldObject actionOwner;

	public abstract ActionState Finished { get; }
	public abstract void Perform ();
	public abstract void Finish ();

}

public struct ActionState
{
	bool isFinished;
	float percentageDone;  // 0.0 to 1.0   // нужно для действий вроде исследования технологии, чтобы показать игроку progressBar

	public ActionState(bool isFinished, float percentageDone)
	{
		this.isFinished = isFinished;
		this.percentageDone = percentageDone;
	}

	public bool IsFinished {
		get {
			return isFinished;
		}
	}

	public float PercentageDone {
		get {
			return percentageDone;
		}
	}
}

