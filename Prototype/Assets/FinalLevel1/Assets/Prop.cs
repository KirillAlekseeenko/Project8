using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : ControllableItem {

	public void ResetOwner()
	{
		currentOwner = owner;
	}

	public void SetOwner(Player player)
	{
		currentOwner = player;
	}
}
