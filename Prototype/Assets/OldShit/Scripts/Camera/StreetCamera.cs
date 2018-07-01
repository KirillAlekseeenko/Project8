using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreetCamera : ControllableItem {

	public static event System.Action AddGradePenaltyEvent;
	public static event System.Action RemoveGradePenaltyEvent;

	private Transform grounds;

	private Timer checkSectorTimer = new Timer(1.0f);
	private bool playerUnitsUnderCamera = false;

	private new void Start()
	{
        base.Start();
		grounds = transform.Find("Grounds");
	}

	private new void Update()
	{
		if(!Captured)
		{
			checkSectorTimer.UpdateTimer(Time.deltaTime);
			if(checkSectorTimer.IsSet)
			{
				CheckSector();
				checkSectorTimer.Reset();
			}
		}
	}
    
    private void CheckSector()
	{
		var groundPosition = grounds.position;

        var playerUnits = Physics.OverlapSphere(groundPosition, LOS, LayerMask.GetMask("Unit"))
                                 .Select(collider => collider.GetComponent<Unit>())
                                 .Where(unit => unit != null)
                                 .Where(unit => unit.Owner.IsHuman);
		
		if(playerUnits.Count() > 0)
		{
			if(!playerUnitsUnderCamera)
			{
				playerUnitsUnderCamera = true;
				if (AddGradePenaltyEvent != null) AddGradePenaltyEvent();
			}
		}
		else
		{
			if(playerUnitsUnderCamera)
			{
				playerUnitsUnderCamera = false;
				if (RemoveGradePenaltyEvent != null) RemoveGradePenaltyEvent();
			}
		}
	}
}
