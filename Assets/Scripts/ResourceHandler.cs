using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHandler : MonoBehaviour {

	private static ResourceHandler resourceHandler = null;

	[SerializeField]
	private int initialGold;

	[SerializeField]
	private Text goldWidget;

	private int gold;

	public static ResourceHandler GetResourceHandler {
		get {
			return resourceHandler;
		}
	}

	public int InitialGold {
		get {
			return initialGold;
		}
	}

	public int Gold {
		get {
			return gold;
		}
	}

	void Awake()
	{
		if (resourceHandler == null)
			resourceHandler = this;
		else if (resourceHandler != this)
			Destroy (this);

	}

	// Use this for initialization
	void Start () {

		gold = initialGold;
		updateGoldWidget ();
		
	}



	public bool isAffordable(int price)
	{
		return price <= gold;
	}
	
	public void addGold(int number)
	{
		gold += number;
		updateGoldWidget ();
	}
	public void takeGold(int number)
	{
		gold -= number;
		if (gold < 0)
			gold = 0;
		updateGoldWidget ();
	}
	private void updateGoldWidget()
	{
		goldWidget.text = "Gold: " + gold.ToString ();
	}
}
