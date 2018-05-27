using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableBuilding : MonoBehaviour, IControllableItem {

	bool captured;

	public bool Captured { get{ return captured;} set{ captured = value;} }

	public void ResetOwner()
	{
	}

	public void SetOwner(Player player)
	{
	}
}
