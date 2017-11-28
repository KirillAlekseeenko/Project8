using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRecruitComponent : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "crowd") {
			other.gameObject.GetComponent<CrowdZoneComponent> ().unitEntered ();
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "crowd") {
			other.gameObject.GetComponent<CrowdZoneComponent> ().unitLeft ();
		}
	}

}
