using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IBuilding{
	bool isUnitWithinTheEntrance (Unit unit);
	bool AddUnit (Unit unit);
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
	[SerializeField]
	protected HashSet<GameObject> scientistsInside;
	[SerializeField]
	protected HashSet<GameObject> hackersInside;
	[SerializeField]
	protected HashSet<GameObject> warriorsInside;

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
		if (unit.gameObject.GetComponent<Scientist>() != null) {
			if (ScientistList.Count < AmountOfScientistsByLevel[Level]) {
				ScientistList.Add (unit.gameObject);
				unit.gameObject.SetActive (false);
				return true;
			}
		} 
		else if (unit.gameObject.GetComponent<Hacker>() != null) {
			if (HackerList.Count < AmountOfHackersByLevel[Level]) {
				HackerList.Add (unit.gameObject);
				unit.gameObject.SetActive (false);
				return true;
			}
		}
		else{
			if (WarriorList.Count < AmountOfWarriorsByLevel[Level]) {
				WarriorList.Add (unit.gameObject);
				unit.gameObject.SetActive (false);
				return true;
			}
		}
		return false;
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

	public void RemoveUnit(GameObject unit){
		if (ScientistList.Contains (unit))
			ScientistList.Remove (unit);
		else if (HackerList.Contains (unit))
			HackerList.Remove (unit);
		else if (WarriorList.Contains (unit))
			WarriorList.Remove (unit);
	}

	public void RemoveAllUnits(){
		Vector3 coords = new Vector3(
			gameObject.GetComponentInChildren<Entrance> ().Coordinates.x - 5,
			gameObject.GetComponentInChildren<Entrance> ().Coordinates.y,
			gameObject.GetComponentInChildren<Entrance> ().Coordinates.z
		);
		foreach(GameObject unit in ScientistList){
			unit.transform.position = coords;
			unit.SetActive (true);
		}
		ScientistList.Clear ();

		foreach(GameObject unit in HackerList){
			unit.transform.position = coords;
			unit.SetActive (true);
		}
		HackerList.Clear ();

		foreach(GameObject unit in WarriorList){
			unit.transform.position = coords;
			unit.SetActive (true);
		}
		WarriorList.Clear ();
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