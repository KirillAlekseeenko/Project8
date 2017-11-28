using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	[SerializeField]
	protected GameObject haloPrefab;    

	protected GameObject halo;

	[SerializeField]
	protected GameObject iconPrefab;   

	[SerializeField]
	protected List<GameObject> updateList;

	[SerializeField]
	protected GameObject downgradePrefab; 

	[SerializeField]
	protected int price; 

	protected GameObject icon;  

	protected bool isSelected = false; 

	[SerializeField]
	protected bool isCapacious;

	protected List<MovingUnit> unitsOnBoard;

	public bool IsSelected {
		get {
			return isSelected;
		}
		set {
			isSelected = value;
		}
	}

	public bool IsCapacious {
		get {
			return isCapacious;
		}
	}

	public List<MovingUnit> UnitsOnBoard {
		get {
			return unitsOnBoard;
		}
	}

	public GameObject IconPrefab { 
		get {
			return iconPrefab;
		}
	}

	public GameObject Icon { 
		get {
			return icon;
		}
		set {
			icon = value;
		}
	}

	public List<GameObject> UpdateList { 
		get {
			return updateList;
		}
	}

	public GameObject DowngradePrefab { 
		get {
			return downgradePrefab;
		}
	}

	public int Price { 
		get {
			return price;
		}
	}


	protected virtual void Awake()
	{
		halo = Instantiate (haloPrefab, gameObject.transform);
		halo.SetActive (false);
	}


	public void showHalo() 
	{
		halo.SetActive (true);
	}
	public void hideHalo() 
	{
		halo.SetActive (false);
	}

	public void getOnBoard(MovingUnit unit)
	{
		unitsOnBoard.Add (unit);
	}
	public void takeFromBoard(MovingUnit unit)
	{
		unitsOnBoard.Remove (unit);
	}

}
