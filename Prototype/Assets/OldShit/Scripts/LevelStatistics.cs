using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatistics : MonoBehaviour {

	public static event System.Action AddGradePenaltyEvent_Fighting;
	public static event System.Action RemoveGradePenaltyEvent_Fighting;

	public static LevelStatistics instance;

	private List<int> deadPlayerUnitsIds = new List<int>();
	private List<int> deadEnemyUnitsIds = new List<int>();

	private SelectionHandler selectionHandler;

	private RevealGrade revealGrade;

	public List<int> DeadPlayerInitsIDs { get { return deadPlayerUnitsIds; } } 
    public List<int> DeadEnemyInitsID { get { return deadEnemyUnitsIds; } }
	public bool Fighting { get; private set; }
    public bool Visibility { get; private set; }

	public void AddToDeadList(Unit unit)
	{
        if (unit.Owner.IsHuman)
            deadPlayerUnitsIds.Add (unit.UnitClassID);
        else
            deadEnemyUnitsIds.Add (unit.UnitClassID);
    }

    public void LevelCompleteEvent(bool success)
	{
        //Show level complete panel
    }

	void Awake()
	{
		if (instance == null)
			instance = this;
		if (instance != this)
			Destroy (gameObject);

		selectionHandler = FindObjectOfType<SelectionHandler>();
		revealGrade = FindObjectOfType<RevealGrade>();
	}

	void Start()
	{
		StartCoroutine (СheckFighting ());
	}

	private IEnumerator СheckFighting()
	{
		while (true) 
		{
			var currentVisibility = false;
			var currentFighting = false;

			foreach (Unit unit in selectionHandler.AllPlayerUnits)
			{
				if (unit.IsVisibleByEnemy && !currentVisibility)
					currentVisibility = true;
				if(unit.isAttacking())
				{
					currentFighting = true;
					currentVisibility = true;
					break;
				}
			}

			if(!Visibility && currentVisibility)
			{
				revealGrade.HandleInstantEvent(10);  //Вас заметили
			}

			if(Fighting != currentFighting)
			{
				if(currentFighting)
				{
					if (AddGradePenaltyEvent_Fighting != null) AddGradePenaltyEvent_Fighting();
				}
				else
				{
					if (RemoveGradePenaltyEvent_Fighting != null) RemoveGradePenaltyEvent_Fighting();
				}
			}

			Visibility = currentVisibility;
			Fighting = currentFighting;

			yield return new WaitForSeconds (1);
		}
	}

    
}
