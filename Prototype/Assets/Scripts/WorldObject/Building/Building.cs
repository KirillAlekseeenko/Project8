using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IBuilding{

	bool isUnitWithinTheEntrance (Unit unit);
	bool AddUnit (Unit unit);
}

public class Building : WorldObject, IBuilding {

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

	protected HashSet<GameObject> scientistsInside;
	protected HashSet<GameObject> hackersInside;
	protected HashSet<GameObject> warriorsInside;

	protected bool buildingMenuOpened;

	protected delegate void BuildingHandler(GameObject building);
	protected event BuildingHandler ShowMenu;

	protected delegate void InformUIAddUnit(GameObject building, GameObject unit);
	protected event InformUIAddUnit UIAddUnit;

	protected delegate void InformUIClearHouse();
	protected event InformUIClearHouse UIClearHouse;

	protected int money;
	protected int scientificResources;

	protected Battle battlePlan;
	protected bool battlePrepares;

	protected void Awake(){

		base.Awake ();

		vTools = gameObject.AddComponent<VisualisationTools>();

		battlePlan = gameObject.AddComponent<Battle>();
		battlePlan.BuildingOwner = Owner;
	}

	protected void Start(){
		base.Start ();

		ShowMenu = new BuildingHandler(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().MenuOpen);
		UIAddUnit = new InformUIAddUnit(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().AddUnit);
		UIClearHouse = new InformUIClearHouse(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().RemoveAll);

		scientistsInside = new HashSet<GameObject> ();
		hackersInside = new HashSet<GameObject> ();
		warriorsInside = new HashSet<GameObject> ();

		Invoke ("AddUnitsWhenStarts", 0.1f);
	}

	protected void AddUnitsWhenStarts(){
		foreach (GameObject unit in WarriorList) {
			AddUnit (unit);
		}
		foreach (GameObject unit in ScientistList) {
			AddUnit (unit);
		}
		foreach (GameObject unit in HackerList) {
			AddUnit (unit);
		}
	}

	protected void Update(){
		base.Update ();
		IsVisible = true;
	}

	protected void startBattle(){
		battlePlan.StartBattle();
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
		throw new System.NotImplementedException ();
	}

	public bool AddUnit (Unit unit){
		throw new System.NotImplementedException ();
	}

	#endregion

	public void EndAutoBattle(int result, Player newOwner, List<Unit> units){
		if (result == 1) {
			this.Owner = newOwner;
			GetComponent<MeshRenderer> ().material.color = owner.Color;
		}
	}

	public void AddUnit(GameObject unit){
		if (unit.gameObject.GetComponent<Scientist>() != null) {
			if (scientistsInside.Count < AmountOfScientistsByLevel[Level]) {
				scientistsInside.Add (unit);
				UIAddUnit (gameObject, unit);
				unit.SetActive (false);
			}
		} 
		else if (unit.gameObject.GetComponent<Scientist>() != null) {
			if (hackersInside.Count < AmountOfHackersByLevel[Level]) {
				hackersInside.Add (unit);
				UIAddUnit (gameObject, unit);
				unit.SetActive (false);
			}
		}
		else{
			if (warriorsInside.Count < AmountOfWarriorsByLevel[Level]) {
				warriorsInside.Add (unit);
				UIAddUnit (gameObject, unit);
				unit.SetActive (false);
			}
		}
	}

	public void InvadeUnit(GameObject unit){
		if (!battlePrepares) {
			battlePrepares = true;
			//Add all defender troops 
			foreach (GameObject obj in warriorsInside)
				battlePlan.AddToBattlePlan (obj);
			Invoke ("startBattle", 5f);
		}
		battlePlan.AddToBattlePlan (unit);
		unit.SetActive (false);
	}

	public void RemoveUnit(GameObject unit){
		if (scientistsInside.Contains (unit)) {
			scientistsInside.Remove (unit);
		}
		else if (hackersInside.Contains (unit)) {
			hackersInside.Remove (unit);
		}
		else if (warriorsInside.Contains (unit)) {
			warriorsInside.Remove (unit);
		}
	}

	public void RemoveAllUnits(){
		Vector3 coords = new Vector3(
			gameObject.GetComponentInChildren<Entrance> ().Coordinates.x - 5,
			gameObject.GetComponentInChildren<Entrance> ().Coordinates.y,
			gameObject.GetComponentInChildren<Entrance> ().Coordinates.z
		);
		foreach(GameObject unit in scientistsInside){
			unit.transform.position = coords;
			unit.SetActive (true);
		}
		scientistsInside.Clear ();

		foreach(GameObject unit in hackersInside){
			unit.transform.position = coords;
			unit.SetActive (true);
		}
		hackersInside.Clear ();

		foreach(GameObject unit in warriorsInside){
			unit.transform.position = coords;
			unit.SetActive (true);
		}
		warriorsInside.Clear ();
	}

	public int Money { get { return money; } }
	public int ScientificRes { get { return scientificResources; } }
	public int ScientistsInside { get { return scientistsInside.Count; } }
	public int HackersInside { get { return hackersInside.Count; } }
	public int WarriorsInside { get { return warriorsInside.Count; } }
}