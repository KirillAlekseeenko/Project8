using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiter : MonoBehaviour {

	[SerializeField] private int recruitPower;
    private RevealGrade revGrade;
	private PopularityGrade popGrade;

	void Awake(){
		revGrade = GameObject.FindObjectOfType<RevealGrade> ();
		popGrade = GameObject.FindObjectOfType<PopularityGrade> ();
	}

    public bool IsRecruiting { get; set; }
    public int RecruitPower { get { return recruitPower; } }

	void OnTriggerEnter(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
			crowdZone.StartRecruiting (this);
            IsRecruiting = true;
			revGrade.HandleInstantEvent (5);
			popGrade.HandleInstantEvent (10);
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
