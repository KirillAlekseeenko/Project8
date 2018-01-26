using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiter : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
			crowdZone.unitEntered ();
		}
	}
	void OnTriggerExit(Collider other)
	{
		var crowdZone = other.gameObject.GetComponent<CrowdZone> ();
		if (crowdZone != null) {
			crowdZone.unitLeft ();
		}
	}

}
