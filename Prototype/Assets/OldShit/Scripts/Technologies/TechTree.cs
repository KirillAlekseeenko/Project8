using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTree : MonoBehaviour {

	[SerializeField]
	private List<Technology> technologies;

	public void UnblockTechnology(int techID, bool unblocked){
		technologies.Find (x => x.technologyID == techID).unblocked = unblocked;
	}

	public Technology FindTech(int techID){
		return technologies.Find (x => x.technologyID == techID);
	}

	public List<Technology> Technologies{
		get{ return technologies;}
	}

	public void PayForTech(int techID){
		FindTech (techID).bought = true;	
		if (techID == 5)
			Player.HumanPlayer.AvailableUpgrades.Add (9);
		if(techID == 7)
			Player.HumanPlayer.AvailableUpgrades.Add (11);
	}
}

[System.Serializable]
public class Technology{
	//Technology parameters
	public bool bought;
	public int cost;
	public int technologyID;
	public int nextTechnologyID;
	public string technologyName;
	public string description;
	public bool unblocked;
	public Sprite technologyImage;
	public Sprite technologyUnitImage;

	//Connected unit parameters
	public int connectedUnitClassID;
	public string connectedUnitClassName;
	public int eficiencyPercentage;
	public int newClassID;
	public int unblockedPerks;
}