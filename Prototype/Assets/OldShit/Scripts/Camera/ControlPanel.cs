using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : WorldObject {

	[SerializeField] protected float activationCooldown;
	[SerializeField] protected IControllableItem controllableItem;

	protected Timer activationTimer;

	public virtual bool IsActivated { get; protected set; }
    
	public virtual void Activate()
    {
		if (!IsActivated)
		{
			activationTimer.Reset();
			IsActivated = true;
		}
    }

	protected new void Start()
	{
		base.Start();
		activationTimer = new Timer(activationCooldown);
	}

	protected new void Update()
	{
		base.Update();
		if (activationTimer.CurrentProgress > 0.98f)
		{
			activationTimer.UpdateTimer(Time.deltaTime);
			if (activationTimer.IsSet)
				IsActivated = false;
		}
	}
}
