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

	public bool Captured { get; private set; }
	
	public virtual void ResetOwner()
	{
		currentOwner = owner;
		Captured = false;
	}

	public virtual void SetOwner(Player player)
	{
		currentOwner = player;
		Captured = true;
	}
}
