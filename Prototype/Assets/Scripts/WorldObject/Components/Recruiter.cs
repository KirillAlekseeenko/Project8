using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiter : MonoBehaviour {

	[SerializeField] private int recruitPower;
    private bool isRecruiting;

    public bool IsRecruiting
    {
        get { return isRecruiting; }
        set 
        {
            isRecruiting = value;
            // pass value to the animator
        }
    }
	public int RecruitPower { get { return recruitPower; } }

	void OnTriggerEnter(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
			crowdZone.StartRecruiting (this);
            IsRecruiting = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
            IsRecruiting = false;
			crowdZone.StopRecruiting (this);
		}
	}


	public override bool Equals(object other)
	{
        var recruiter = other as Recruiter;
        if (recruiter == null)
            return false;
        return recruiter.GetInstanceID() == this.GetInstanceID();
	}
}
