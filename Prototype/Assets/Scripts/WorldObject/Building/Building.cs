using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IBuilding{
	bool isUnitWithinTheEntrance (Unit unit);
	bool AddUnit (Unit unit);
	Vector3 entrancePosition();
}

public class Building : WorldObject, IBuilding{

	[SerializeField]
	protected int Level;

	[Header("Units Inside")]

	[SerializeField]
	protected Vector3Int AmountOfHackersByLevel;
	[SerializeField]
	protected Vector3Int AmountOfScientistsByLevel;
	[SerializeField]
	protected Vector3Int AmountOfWarriorsByLevel;
	[SerializeField]
	protected List<GameObject> WarriorList;
	[SerializeField]
	protected List<GameObject> ScientistList;
	[SerializeField]
	protected List<GameObject> HackerList;

	protected VisualisationTools vTools;

	protected Color mainColor;

	protected bool buildingMenuOpened;

	protected delegate void BuildingHandler(GameObject building);
	protected event BuildingHandler ShowMenu;

	protected delegate void InformUIClearHouse();
	protected event InformUIClearHouse UIClearHouse;

	protected int money;
	protected int scientificResources;

	protected Battle battlePlan;
	protected bool battlePrepares;

	protected Entrance entrance;

	//Для случая вытаскивания юнитов из здания
	protected bool banishOneUnit;
	protected GameObject banishedOne;
	protected bool banishUnits;
	protected int unbanishedYet;

	protected void Awake(){

		base.Awake ();

		vTools = gameObject.GetComponent<VisualisationTools>();

		battlePlan = gameObject.AddComponent<Battle>();
		battlePlan.BuildingOwner = Owner;
	}

	protected void Start(){
		base.Start ();

		ShowMenu = new BuildingHandler(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().MenuOpen);
		UIClearHouse = new InformUIClearHouse(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().RemoveAll);

		entrance = gameObject.GetComponentInChildren<Entrance> ();

		RefreshUnitsInside ();

		addMoney ();
	}

	protected void Update(){
		base.Update ();
		IsVisible = true;
		unbanishedYet = 0;
		if (banishUnits) {
			foreach (GameObject unit in ScientistList) {
				if (Vector3.Distance (unit.transform.position, entrance.transform.position) > 1) {
					unit.gameObject.GetComponent<Rigidbody> ().MovePosition (entrance.transform.position);
					unbanishedYet++;
				} else {
					unit.SetActive (true);
				}
					
			}
			foreach (GameObject unit in HackerList) {
				if (Vector3.Distance (unit.transform.position, entrance.transform.position) > 1) {
					unit.GetComponent<Rigidbody> ().MovePosition (entrance.transform.position);
					unbanishedYet++;
				} else {
					unit.SetActive (true);
				}
			}
			foreach (GameObject unit in WarriorList) {
				if (Vector3.Distance (unit.transform.position, entrance.transform.position) > 1) {
					unit.GetComponent<Rigidbody> ().MovePosition (entrance.transform.position);
					unbanishedYet++;
				} else {
					unit.SetActive (true);
				}
			}
			if (unbanishedYet == 0) {
				banishUnits = false;
				HackerList.Clear ();
				ScientistList.Clear ();
				WarriorList.Clear ();
			}
		} else if (banishOneUnit) {
			if (Vector3.Distance (banishedOne.transform.position, entrance.transform.position) > 1) {
				banishedOne.GetComponent<Rigidbody> ().MovePosition (entrance.transform.position);
				unbanishedYet++;
			} else {
				banishedOne.SetActive (true);
				banishOneUnit = false;
				if (banishedOne.GetComponent<Scientist> () != null) {
					ScientistList.Remove (banishedOne);
				} else if (banishedOne.GetComponent<Hacker> () != null) {
					HackerList.Remove (banishedOne);
				} else {
					WarriorList.Remove (banishedOne);
				}
			}
		}
	}

	private void addMoney(){
		money += ScientistList.Count * 5;
		Invoke ("addMoney", 1f);
	}

	protected void startBattle(){
		battlePlan.StartBattle();
	}

	protected void RefreshUnitsInside(){
		foreach (GameObject obj in WarriorList) {
			obj.SetActive (false);
		}
		foreach (GameObject obj in ScientistList) {
			obj.SetActive (false);
		}
		foreach (GameObject obj in HackerList) {
			obj.SetActive (false);
		}
	}

	public override bool IsSelected {
		get {
			return base.IsSelected;
		}
		set {
			base.IsSelected = value;
			if (owner.IsHuman) {
				if (value) {
					ShowMenu (gameObject);
					vTools.SetBlinking (true);
				} else {
					vTools.SetBlinking (false);
					// hide menu
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
		//throw new System.NotImplementedException ();
	}

	public bool AddUnit(Unit unit){
		if (unit.Owner == gameObject.GetComponentInParent<Building> ().Owner) {

			if (unit.gameObject.GetComponent<Scientist> () != null) {
				if (ScientistList.Count < AmountOfScientistsByLevel [Level]) {
					ScientistList.Add (unit.gameObject);
					unit.gameObject.SetActive (false);
					return true;
				}
			} else if (unit.gameObject.GetComponent<Hacker> () != null) {
				if (HackerList.Count < AmountOfHackersByLevel [Level]) {
					HackerList.Add (unit.gameObject);
					unit.gameObject.SetActive (false);
					return true;
				}
			} else {
				if (WarriorList.Count < AmountOfWarriorsByLevel [Level]) {
					WarriorList.Add (unit.gameObject);
					unit.gameObject.SetActive (false);
					return true;
				}
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
			WarriorList.Clear ();
			foreach (Unit obj in units)
				WarriorList.Add (obj.gameObject);
		}
	}
		
	public bool InvadeUnit(Unit unit){
		if (!battlePrepares) {
			battlePrepares = true;
			//Add all defender troops 
			foreach (GameObject obj in WarriorList) {
				battlePlan.AddToBattlePlan (obj);
			}
			Invoke ("startBattle", 5f);
		}
		battlePlan.AddToBattlePlan (unit.gameObject);
		unit.gameObject.SetActive (false);

		return true;
	}

	public void RemoveUnit(int unitClass){
		banishOneUnit = true;
		if (unitClass == 0 && ScientistList.Count > 0)
			banishedOne = ScientistList [0];
		else if (unitClass == 1 && HackerList.Count > 0)
			banishedOne = HackerList [0];
		else if (unitClass == 2 && WarriorList.Count > 0)
			banishedOne = WarriorList [0];
	}

	public void RemoveAllUnits(){
		banishUnits = true;
	}

	public int CurrentLevel { 
		get { return Level; }
		set{
			Level = value;
			vTools.SetModel (Level);
		}}
	public int Money { get { return money; } }
	public int ScientificRes { get { return scientificResources; } }
	public int ScientistsInside { get { return ScientistList.Count; } }
	public int HackersInside { get { return HackerList.Count; } }
	public int WarriorsInside { get { return WarriorList.Count; } }
}