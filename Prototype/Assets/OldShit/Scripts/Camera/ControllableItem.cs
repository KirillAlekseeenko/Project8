using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllableItem
{
	void SetOwner(Player player);
	void ResetOwner();
	bool Captured { get; }
}

public abstract class ControllableItem : WorldObject, IControllableItem {
    
	protected Player permanentOwner;

	public bool Captured { get; private set; }

    protected new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
       // base.Start();    // не работает цвет
        permanentOwner = Owner;
    }
	
	public virtual void ResetOwner()
	{
        ChangeOwner(permanentOwner);
		Captured = false;
	}

	public virtual void SetOwner(Player player)
	{
        ChangeOwner(player);
		Captured = true;
	}
}
