using UnityEngine;
using System.Collections;

public class Citizen : MonoBehaviour
{
	[SerializeField] private Material convertedManMaterial;
	[SerializeField] private Material convertedWomanMaterial;

    public bool IsFree
    {
        get
        {
            if (GetComponent<Unit>().ActionQueue.Count > 0)
            {
                return !(GetComponent<Unit>().ActionQueue.Peek() is RecruiteeInteraction);
            }
            else
                return true;
        }
    }
    
    public Player RercruiterOwner
    {
        get
        {
            if (GetComponent<Unit>().ActionQueue.Count > 0)
            {
                var currentAction = GetComponent<Unit>().ActionQueue.Peek();
                if (currentAction is RecruiteeInteraction)
                    return (currentAction as RecruiteeInteraction).RecruiterOwner;
            }
            return null;
        }
    }

	public void StartClapping()
    {
		GetComponent<Unit> ().HearAgitator ();
    }

    public void StopClapping()
    {
		Unit unit = GetComponent<Unit> ();
		unit.Interrupt();
		unit.Idle();
		unit.animator.SetInteger ("HearAgitVariants", 0);
		if (unit.Owner.IsHuman) {
			if (unit.sex == Unit.Sex.FEMALE) {
				GetComponentInChildren<SkinnedMeshRenderer> ().material = convertedWomanMaterial;
			} else {
				GetComponentInChildren<SkinnedMeshRenderer> ().material = convertedManMaterial;
			}
		}
	}
}
