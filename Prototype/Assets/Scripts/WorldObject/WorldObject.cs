using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class WorldObject : MonoBehaviour {
	
	// mainFields

	protected Player owner;
	protected Queue<Action> actionQueue;
	protected Action currentAction;

	//common fields

	protected bool isSelected;
	protected GameObject halo;
	protected GameObject icon;

	[SerializeField]
	protected GameObject haloPrefab;
	[SerializeField]
	protected GameObject iconPrefab;

	// hp, armor etc

	protected RectTransform healthBarRectTransform;

	public Player Owner {
		get {
			return owner;
		}
	}

	public Queue<Action> ActionQueue {
		get {
			return actionQueue; 
		}
	}


	public bool IsSelected {
		get {
			return isSelected;
		}
	}

	public GameObject Halo {
		get {
			return halo; // setActive(true) when selected
		}
	}

	public GameObject Icon {
		get {
			return icon; 
		}
	}

	protected void Awake()
	{
		
	}

	protected void Start()
	{
		
	}

	protected void Update ()
	{
		
	}

}
