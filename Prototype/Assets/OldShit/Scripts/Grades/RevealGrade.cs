using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealGrade : Grade
{
    protected override void SubscribeToEvents()
    {
		StreetCamera.AddGradePenaltyEvent += () => AddOngoingProcess(OngoingProcessType.UnderCamera);
		StreetCamera.RemoveGradePenaltyEvent += () => RemoveOngoingProcess(OngoingProcessType.UnderCamera);
    }

    protected override void UnsubscribeFromEvents()
    {
        
    }

    protected override void UpdateViewController()
    {
        gradesViewController.SetRevealGrade(currentValue);
    }
}