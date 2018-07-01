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

	[SerializeField] protected GameObject haloPrefab;
	[SerializeField] protected Sprite icon;

    [SerializeField] protected float lOS; // line of sight

    public Player Owner { get { return owner; } set { owner = value; } }
    public Queue<Action> ActionQueue { get { return actionQueue; } }

    public virtual bool IsSelected { get { return isSelected; } set { isSelected = value; } }
	public virtual bool IsVisibleInGame { get; protected set;}
    public Sprite Icon { get { return icon; } }
    public float LOS { get { return lOS; } }

	public virtual void Highlight()
	{
		halo.SetActive (true);
	}
	public virtual void Dehighlight()
	{
		halo.SetActive (false);
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

	public void Stop()
	{
		if (actionQueue.Count == 0)
			return;
		var lastAction = actionQueue.Peek ();
		if (lastAction != null) {
			lastAction.Finish ();
			actionQueue.Clear ();
		}
	}

	public override bool Equals (object other)
	{
		if (other == null || !(other is WorldObject))
			return false;
		WorldObject worldObject = (WorldObject)other;
		return GetInstanceID () == worldObject.GetInstanceID ();
	}

    public virtual void ChangeOwner(Player newOwner)
    {
        owner = newOwner;
        var visionArc = GetComponent<VisionArcComponent>();
        if(visionArc != null)
        {
            Destroy(GetComponent<VisionArcComponent>());
            gameObject.AddComponent<VisionArcComponent>();
        }
    }
}
