using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllableItem
{
	void SetOwner(Player player);
	void ResetOwner();
	bool Captured { get; }
}

public abstract class ControllableItem : MonoBehaviour, IControllableItem {

	protected Player owner;        // const
	protected Player currentOwner;

	public bool Captured { get; protected set; }
	
	public virtual void ResetOwner()
	{
		currentOwner = owner;
	}

	public virtual void SetOwner(Player player)
	{
		currentOwner = player;
	}
}
