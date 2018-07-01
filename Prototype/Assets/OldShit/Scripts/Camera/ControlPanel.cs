using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : WorldObject {

	[SerializeField] protected float activationCooldown;
	[SerializeField] protected ControllableItem controllableItem;

	protected Timer activationTimer;

	public virtual bool IsActivated { get; protected set; }
    
	public virtual void Activate(Player player)
    {
		if (!IsActivated)
		{
            controllableItem.SetOwner(player);
			activationTimer.Reset();
			IsActivated = true;
		}
    }

	protected new void Start()
	{
		activationTimer = new Timer(activationCooldown);
	}

	protected new void Update()
	{
		if (IsActivated)
		{
			activationTimer.UpdateTimer(Time.deltaTime);
            if (activationTimer.IsSet)
                Deactivate();
		}
	}

    protected void Deactivate()
    {
        IsActivated = false;
        controllableItem.ResetOwner();
    }
}
