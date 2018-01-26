using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IBuilding{

	bool isUnitWithinTheEntrance (Unit unit);
	bool AddUnit (Unit unit);
}

public class Building : WorldObject, IBuilding {

	private Renderer objRenderer;
	private Color mainColor;

	private HashSet<GameObject> scientistsInside;
	private HashSet<GameObject> hackersInside;
	private HashSet<GameObject> warriorsInside;
	private int buildingLevel;

	private bool buildingMenuOpened;

	private int resourcesCount;

	private delegate void BuildingHandler(GameObject building);
	private event BuildingHandler ShowMenu;

	private delegate void InformUIAddUnit(GameObject building, GameObject unit);
	private event InformUIAddUnit UIAddUnit;

	private delegate void InformUIClearHouse();
	private event InformUIClearHouse UIClearHouse;


	private IEnumerator blink;
	private IEnumerator counter;

	public int increaseResourcesByUnit;
	public int maxUnitsAllowed;

	private void Awake(){
		base.Awake ();

		objRenderer = GetComponent<Renderer> ();


		scientistsInside = new HashSet<GameObject> ();
		hackersInside = new HashSet<GameObject> ();
		warriorsInside = new HashSet<GameObject> ();

		resourcesCount = 0;

		//ShowMenu = new BuildingHandler(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().MenuOpen);
		//UIAddUnit = new InformUIAddUnit(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().AddUnit);
		//UIClearHouse = new InformUIClearHouse(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().RemoveAll);

		blink = Blink ();
		counter = IncreaseResources ();
	}

	protected void Start()
	{
		base.Start ();

		mainColor = objRenderer.material.color;

		ShowMenu = new BuildingHandler(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().MenuOpen);
		UIAddUnit = new InformUIAddUnit(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().AddUnit);
		UIClearHouse = new InformUIClearHouse(GameObject.Find("UIBuildingHandler").GetComponent<BuildingUI>().RemoveAll);
	}

	private void Update(){
		base.Update ();
	}

	/*
	private void OnMouseDown(){ // 
		isSelected = true;
		ShowMenu (gameObject);
	}*/

	public override bool IsVisible {
		get {
			return base.IsVisible || owner.IsHuman;
		}
		protected set {
			base.IsVisible = value;
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
				} else {
					// hide menu
				}
			}

		}
	}
		
	private IEnumerator Blink() {
		float maxAlpha = 1f;
		float minAlpha = 0.4f;
		float alphaDelta = 0.01f;
		while(true){
			if (objRenderer.material.color.a <= minAlpha || objRenderer.material.color.a >= maxAlpha)
				alphaDelta = -alphaDelta;
			objRenderer.material.SetColor("_Color", new Color(objRenderer.material.color.r,
				objRenderer.material.color.g,
				objRenderer.material.color.b,
				objRenderer.material.color.a + alphaDelta));
			yield return new WaitForSeconds(0.02f);
		}
	}

	private IEnumerator IncreaseResources() {
		while(true){
			resourcesCount += (scientistsInside.Count + hackersInside.Count) * increaseResourcesByUnit;
			yield return new WaitForSeconds(1);
		}
	}

	public void AddUnit(GameObject unit){
		if (unit.name.Contains ("Scientist")) {
			if (scientistsInside.Count < maxUnitsAllowed) {
				scientistsInside.Add (unit);
				UIAddUnit (gameObject, unit);
				unit.SetActive (false);
			}
		} 
		else if (unit.name.Contains ("Hacker")) {
			if (hackersInside.Count < maxUnitsAllowed) {
				hackersInside.Add (unit);
				UIAddUnit (gameObject, unit);
				unit.SetActive (false);
			}
		}
		else if (unit.name.Contains ("Warrior")) {
			if (warriorsInside.Count < maxUnitsAllowed) {
				warriorsInside.Add (unit);
				UIAddUnit (gameObject, unit);
				unit.SetActive (false);
			}
		}
		if (scientistsInside.Count + hackersInside.Count == 1)
			StartCoroutine (counter);
		
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
		foreach(GameObject unit in scientistsInside){
			unit.SetActive (true);
			unit.transform.position = new Vector3(unit.transform.position.x, 
												  unit.transform.position.y + 0.5f,
												  unit.transform.position.z);
		}
		scientistsInside.Clear ();

		foreach(GameObject unit in hackersInside){
			unit.SetActive (true);
			unit.transform.position = new Vector3(unit.transform.position.x, 
				unit.transform.position.y + 0.5f,
				unit.transform.position.z);
		}
		hackersInside.Clear ();

		foreach(GameObject unit in warriorsInside){
			unit.SetActive (true);
			unit.transform.position = new Vector3(unit.transform.position.x, 
				unit.transform.position.y + 0.5f,
				unit.transform.position.z);
		}
		warriorsInside.Clear ();

		StopCoroutine (counter);

		UIClearHouse ();
	}
		
	public void SetBlinking(bool blinking){
		if (blinking)
			StartCoroutine (blink);
		else {
			StopCoroutine (blink);
			objRenderer.material.color = mainColor;
			isSelected = false;
		}
	}

	#region IBuilding implementation

	public bool isUnitWithinTheEntrance (Unit unit)
	{
		throw new System.NotImplementedException ();
	}

	public bool AddUnit (Unit unit)
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	public int Recources { get { return resourcesCount; } }
	public int ScientistsInside { get { return scientistsInside.Count; } }
	public int HackersInside { get { return hackersInside.Count; } }
	public int WarriorsInside { get { return warriorsInside.Count; } }
}