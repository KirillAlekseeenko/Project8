<<<<<<< HEAD
﻿using System.Collections;
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
=======
﻿using System.Collections;
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
>>>>>>> c60b1ff91aeac50b3e217313038ef44d0d866be6
