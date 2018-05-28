using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealGrade : Grade
{
	public static event System.Action SafeStage;
	public static event System.Action FirstStage;
	public static event System.Action SecondStage;
	public static event System.Action ThirdStage;

    protected override void SubscribeToEvents()
    {
		StreetCamera.AddGradePenaltyEvent += () => AddOngoingProcess(OngoingProcessType.UnderCamera);
		Building.AddGradePenaltyEvent_WithoutHacking += () => AddOngoingProcess (OngoingProcessType.BuildingCapture);
    }

    protected override void UnsubscribeFromEvents()
    {      
		StreetCamera.RemoveGradePenaltyEvent += () => RemoveOngoingProcess(OngoingProcessType.UnderCamera);
		Building.RemoveGradePenaltyEvent_WithoutHacking += () => RemoveOngoingProcess(OngoingProcessType.BuildingCapture);
    }

    protected override void UpdateViewController()
    {
        gradesViewController.SetRevealGrade(currentValue);
    }

	protected override void HandleValue()
	{
		var percentage = currentValue / maxValue * 100;
		if(percentage > 60)
		{
			if (FirstStage != null) FirstStage();
		}
		else if(percentage > 80)
		{
			if (SecondStage != null) SecondStage();
		}
		else if(percentage > 90)
		{
			if (ThirdStage != null) ThirdStage();
		}
		else
		{
			if (SafeStage != null) SafeStage();
		}
	}
}