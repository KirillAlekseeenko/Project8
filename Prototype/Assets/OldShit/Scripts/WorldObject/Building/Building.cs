using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.InteropServices;

interface IBuilding{
	bool isUnitWithinTheEntrance (Unit unit);
	bool AddUnit (Unit unit);
	Vector3 entrancePosition();
}

public class Building : WorldObject, IBuilding{

	[Header("Units Inside")]
	[SerializeField]
	protected Vector3Int AmountOfHackersByLevel;
	[SerializeField]
	protected Vector3Int AmountOfScientistsByLevel;

	[SerializeField]
	protected List<Unit> unitsInside;

	[Header("Technologies")]
	[SerializeField] 
	protected TechTree techTree;
	[SerializeField]
	protected List<int> connectedTechnologiesIDs;
	[SerializeField]
	protected int Level;

	protected Battle battlePlan;
	protected bool battlePrepares;

	protected Entrance entrance;

	protected BuildingPanelManager uiHandler;

	//Для случая вытаскивания юнитов из здания
	protected bool banishOneUnit;
	protected Unit banishedOne;
	protected bool banishUnits;
	protected int unbanishedYet;

	protected int scientistID = 3;
	protected int hackerID = 4;

	[HideInInspector]
	public VisualisationTools vTools;

	protected void Awake(){

		base.Awake ();

		vTools = gameObject.GetComponent<VisualisationTools>();
		battlePlan = gameObject.AddComponent<Battle>();
		battlePlan.BuildingOwner = Owner;
	}

	protected void Start(){
		base.Start ();

		entrance = gameObject.GetComponentInChildren<Entrance> ();

		uiHandler = GameObject.FindObjectOfType<BuildingPanelManager> ();

		RefreshUnitsInside ();
	}

	protected void Update(){
		base.Update ();
		IsVisible = true;
		unbanishedYet = 0;
		//После того, как мы нажимаем кнопку "выгнать всех юнитов из здания"
		if (banishUnits) {
			foreach(Unit unit in unitsInside){
				if (Vector3.Distance (unit.gameObject.transform.position, entrance.transform.position) > 1) {
					unit.gameObject.GetComponent<Rigidbody> ().MovePosition (entrance.transform.position);
					unbanishedYet++;
				} else {
					unit.gameObject.SetActive (true);
				}
			}
			if (unbanishedYet == 0) {
				banishUnits = false;
				unitsInside.Clear ();
			}
		}
		//После того, как выгоняем только одного из юнитов
		else if (banishOneUnit) {
			if (Vector3.Distance (banishedOne.transform.position, entrance.transform.position) > 1) {
				banishedOne.GetComponent<Rigidbody> ().MovePosition (entrance.transform.position);
				unbanishedYet++;
			} else {
				banishedOne.gameObject.SetActive (true);
				banishOneUnit = false;
				unitsInside.Remove (banishedOne);
			}
		}
	}

	protected void startBattle(){
		battlePlan.StartBattle();
	}

	protected void RefreshUnitsInside(){
		foreach (Unit unit in unitsInside)
			unit.gameObject.SetActive (false);
	}

	public override bool IsSelected {
		get {
			return base.IsSelected;
		}
		set {
			base.IsSelected = value;
			if (owner.IsHuman) {
				if (value) {
					uiHandler.showPanel(true, this);
					vTools.SetBlinking (true);
				} else {
					vTools.SetBlinking (false);
				}
			}

		}
	}

	public override void Highlight(){
		//StartCoroutine (blink);
	}

	public override void Dehighlight(){
		//StopCoroutine (blink);
	}

	#region IBuilding implementation

	public bool isUnitWithinTheEntrance (Unit unit){
		if (entrance.UnitWithin (unit))
			return true;
		else
			return false;
	}

	public bool AddUnit(Unit unit){
		if (unit.Owner == gameObject.GetComponentInParent<Building> ().Owner) {
			if((unit.UnitClassID == scientistID
				&& unitsInside.FindAll(x => x.UnitClassID == scientistID).Count < AmountOfScientistsByLevel[Level - 1]) ||
				(unit.UnitClassID == hackerID
					&& unitsInside.FindAll(x => x.UnitClassID == hackerID).Count < AmountOfHackersByLevel[Level - 1]) ||
				(unit.UnitClassID != scientistID && unit.UnitClassID != hackerID)
			){
				unitsInside.Add (unit);
				unit.gameObject.SetActive (false);
				return true;
			}
		} else {
			InvadeUnit (unit);
		}
		return false;
	}
		
	public Vector3 entrancePosition(){
		return entrance.transform.position;
	}
	#endregion

	public void EndAutoBattle(int result, Player newOwner, List<Unit> units){
		battlePrepares = false;
		if (result == 1) {
			this.Owner = newOwner;
			GetComponent<MeshRenderer> ().material.color = owner.Color;

			unitsInside.Clear ();
			foreach(Unit unit in units){
				unitsInside.Add (unit);
			}
			if (this.Owner.IsHuman) {
				for (int i = 0; i < Level - 1; i++)
					techTree.UnblockTechnology (connectedTechnologiesIDs[i], true);
				//techTree.UnblockTechnology (connectedTechnologyID, true);
			} else {
				for (int i = 0; i < Level - 1; i++)
					techTree.UnblockTechnology (connectedTechnologiesIDs[i], false);
				//techTree.UnblockTechnology (connectedTechnologyID, false);
			}
		}
	}
		
	public bool InvadeUnit(Unit unit){
		if (!battlePrepares) {
			battlePrepares = true;
			//Add all defender troops 
			foreach(Unit u in unitsInside){
				battlePlan.AddToBattlePlan(u);
			}
			Invoke ("startBattle", 5f);
		}
		battlePlan.AddToBattlePlan(unit);
		unit.gameObject.SetActive (false);
		return true;
	}

	public void RemoveUnit(int unitClass){
		banishOneUnit = true;
		banishedOne = unitsInside.Find (x => x.UnitClassID == unitClass);
	}

	public void RemoveAllUnits(){
		banishUnits = true;
	}

	public int CurrentLevel { 
		get { return Level; }
		set{
			if (Player.HumanPlayer.ResourcesManager.IsEnoughMoney (vTools.buildingLevels [value - 1].cost)) {
				Player.HumanPlayer.ResourcesManager.SpendMoney (vTools.buildingLevels [value - 1].cost);
				Level = value;
				techTree.UnblockTechnology (connectedTechnologiesIDs[value - 2], true);
				vTools.SetModel (Level);
			}
		}
	}

	public string GradeName(int level){
		return vTools.buildingLevels [level - 1].name;
	}

	public Technology LevelTechnology(int level){
		if (level > 1)
			return techTree.FindTech (connectedTechnologiesIDs [level - 2]);
		else
			return null;
	}

	public int NonWarriorsAmountPerLevel(int level){
		if (AmountOfHackersByLevel [0] != 0)
			return AmountOfHackersByLevel [level - 1];
		else if (AmountOfScientistsByLevel [0] != 0)
			return AmountOfScientistsByLevel [level - 1];
		else
			return 0;
	}

	public string BuildingName{
		get{ return vTools.BuildingName;}
	}
		
	public List<Unit> UnitsInside { get{ return unitsInside;}}

	public List<Unit> ScientistsInside { get { return unitsInside.FindAll(x => x.UnitClassID == scientistID); } }
	public List<Unit> HackersInside { get { return unitsInside.FindAll (x => x.UnitClassID == hackerID);} }
	public List<Unit> WarriorsInside { get { return unitsInside.FindAll (x => x.UnitClassID != scientistID && x.UnitClassID != hackerID);} }
	public int NonWarriorsInside{get{ return unitsInside.FindAll (x => x.UnitClassID == scientistID || x.UnitClassID == hackerID).Count;}}
}