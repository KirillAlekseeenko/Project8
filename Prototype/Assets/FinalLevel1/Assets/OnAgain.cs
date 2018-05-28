using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAgain : MonoBehaviour {

	[SerializeField] private GameObject unitPanel;

	void Update(){
		if (!unitPanel.activeInHierarchy)
			unitPanel.SetActive (true);
	}
}
