﻿using System.Collections;
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
		Building.AddGradePenaltyEvent_FightInside += () => AddOngoingProcess(OngoingProcessType.BuildingRetreive);
		LevelStatistics.AddGradePenaltyEvent_Fighting += () => AddOngoingProcess(OngoingProcessType.Battle);
    }

    protected override void UnsubscribeFromEvents()
    {      
		StreetCamera.RemoveGradePenaltyEvent += () => RemoveOngoingProcess(OngoingProcessType.UnderCamera);
		Building.RemoveGradePenaltyEvent_WithoutHacking += () => RemoveOngoingProcess(OngoingProcessType.BuildingCapture);
		Building.RemoveGradePenaltyEvent_FightInside += () => RemoveOngoingProcess(OngoingProcessType.BuildingRetreive);
		LevelStatistics.RemoveGradePenaltyEvent_Fighting += () => RemoveOngoingProcess(OngoingProcessType.Battle);
   }

    protected override void UpdateViewController()
	{	
        gradesViewController.SetRevealGrade(currentValue / 100);
    }

	protected override void HandleValue()
	{
		var percentage = currentValue / maxValue * 100;
		if(percentage < 60)
		{
            SafeStage?.Invoke();
        }
		else if(percentage < 80)
		{
            FirstStage?.Invoke();
        }
		else if(percentage < 90)
		{
            SecondStage?.Invoke();
        }
		else
		{
            ThirdStage?.Invoke();
        }
	}

	public void HandleInstantEvent(float value)
	{
		currentValue += value;
		currentValue = Mathf.Clamp(currentValue, 0, maxValue);
	}

	public float CurrentValue{
		get{return currentValue;}
	}

}