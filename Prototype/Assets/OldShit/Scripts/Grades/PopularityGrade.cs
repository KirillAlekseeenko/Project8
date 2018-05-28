using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopularityGrade : Grade
{
    protected override void SubscribeToEvents()
    {
        
    }

    protected override void UnsubscribeFromEvents()
    {
        
    }

    protected override void UpdateViewController()
    {
        gradesViewController.SetPopularityGrade(currentValue / 100);
    }

	protected override void HandleValue()
	{
		// pass
	}

	public void HandleInstantEvent(float value)
	{
		currentValue += value;
		currentValue = Mathf.Clamp(currentValue, 0, maxValue);
	}
}
