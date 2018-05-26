using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : WorldObject {

	[SerializeField] private float activationCooldown;
	[SerializeField] private IControllableItem controllableItem;

	private Timer activationTimer;

	public bool IsActivated { get; private set; }
    
	public void Activate()
    {
		if (!IsActivated)
		{
			activationTimer.Reset();
			IsActivated = true;
		}
    }

	private new void Start()
	{
		base.Start();
		activationTimer = new Timer(activationCooldown);
	}

	private new void Update()
	{
		base.Update();
		if (IsActivated)
		{
			activationTimer.UpdateTimer(Time.deltaTime);
			if (activationTimer.IsSet)
				IsActivated = false;
		}
	}
}
