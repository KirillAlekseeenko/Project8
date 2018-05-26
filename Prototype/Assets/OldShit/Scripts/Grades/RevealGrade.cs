using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealGrade : Grade
{
    protected override void SubscribeToEvents()
    {
        
    }

    protected override void UnsubscribeFromEvents()
    {
        
    }

    protected override void UpdateViewController()
    {
        gradesViewController.SetRevealGrade(currentValue);
    }
}
