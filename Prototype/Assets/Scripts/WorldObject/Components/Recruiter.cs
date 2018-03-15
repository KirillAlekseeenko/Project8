using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiter : MonoBehaviour {

	[SerializeField] private int recruitPower;

	public int RecruitPower { get { return recruitPower; } }

	void OnTriggerEnter(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
			crowdZone.StartRecruiting (this);
		}
	}
	void OnTriggerExit(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
			crowdZone.StopRecruiting (this);
		}
	}
}
