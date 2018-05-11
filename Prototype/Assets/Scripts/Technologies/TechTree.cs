using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTree : MonoBehaviour {

	[SerializeField]
	private List<Technology> technologies;

	public void UnblockTechnology(int techID, bool unblocked){
		technologies.Find (x => x.technologyID == techID).unblocked = unblocked;
	}
}

[System.Serializable]
public class Technology{
	//Technology parameters
	public int cost;
	public int technologyID;
	public int ancestorTechnologyID;
	public string technologyName;
	public string description;
	public bool unblocked;

	//Connected unit parameters
	public int connectedUnitClassID;
	public int eficiencyPercentage;
	public int newClassID;
	public int unblockedPerks;
}