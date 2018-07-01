using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class WatchingCamera : ControlPanel {

	[SerializeField] private Renderer cameraBeam;

	public override void Activate()
	{
		if (!IsActivated)
		{
			activationTimer.Reset();
			IsActivated = true;
			cameraBeam.material.SetColor ("_TintColor", new Color(1f, 1f, 1f, 0f));
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
			if (activationTimer.CurrentProgress > 0.98f) {
				IsActivated = false;
				cameraBeam.material.SetColor ("_TintColor", new Color(1f, 1f, 0f, 0.1f));
			}
		}
	}
}*/
