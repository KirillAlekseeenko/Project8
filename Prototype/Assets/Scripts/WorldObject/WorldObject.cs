using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public abstract class WorldObject : MonoBehaviour {
	
	// mainFields
	[SerializeField]
	protected Player owner;

	[SerializeField]
	protected string currentActionType;

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


	public virtual bool IsSelected {
		get {
			return isSelected;
		}
		set {
			isSelected = value;
		}
	}

	public virtual bool IsVisible { get; protected set;}
		

	public GameObject Icon {
		get {
			return icon; 
		}
	}

	public virtual void Highlight()
	{
		halo.SetActive (true);
		// hp
	}
	public virtual void Dehighlight()
	{
		halo.SetActive (false);
		// hp
	}

	protected void Awake()
	{
		halo = Instantiate (haloPrefab, transform);
		halo.SetActive (false);
		isSelected = false;

		actionQueue = new Queue<Action> ();
	}

	protected void Start()
	{
		GetComponent<MeshRenderer> ().material.color = owner.Color;
	}

	protected void Update ()
	{
		try{
			currentActionType = actionQueue.Peek().GetType().ToString();
			if (actionQueue.Peek ().State.IsFinished) {
				actionQueue.Dequeue ();
				actionQueue.Peek ().Perform ();
			}
		}
		catch(InvalidOperationException) {
			
		}
	}

	public bool isIdle()
	{
		return actionQueue.Count == 0;
	}

	public void DoAction(Action action)
	{
		var copy = actionQueue.ToArray ();
		if (actionQueue.Count > 0) {
			var lastAction = actionQueue.Peek ();
			actionQueue.Clear ();
			lastAction.Finish ();
		}

		action.Perform ();
		actionQueue.Enqueue (action);
		foreach(var _action in copy)
			actionQueue.Enqueue(_action);
	}
	public void AssignActionShift(Action action)
	{
		actionQueue.Enqueue (action);
	}
	public void AssignAction(Action action)
	{
		if (actionQueue.Count > 0) {
			var lastAction = actionQueue.Peek ();
			actionQueue.Clear ();
			lastAction.Finish ();
		}

		action.Perform ();
		actionQueue.Enqueue (action);
	}

	public override bool Equals (object other)
	{
		if (other == null || !(other is WorldObject))
			return false;
		WorldObject worldObject = (WorldObject)other;
		return GetInstanceID () == worldObject.GetInstanceID ();
	}

	protected void OnDestroy()
	{
		
	}

}
