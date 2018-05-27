using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Director : MonoBehaviour {

    private const int InitialActiveGroupSize = 5; // it will depend on grades values in future updates
	private int activeGroupMultiplier = 1;

	private Player player;

	[SerializeField] private float alarmRadius;
    [SerializeField] List<Path> patrolPaths;
    [SerializeField] List<Transform> serializedSafePoints;

	HashSet<Unit> units;
	HashSet<Unit> idleUnits;

	HashSet<Building> capturedBuildings;

    ICollection<WorldObject> activeUnitGroup; // group logic will be incapsulated in a class
	bool groupIsActive = false;

    ICollection<Vector3> safePoints;

	public float SpawnCoefficient { get; private set; }
	private int ActiveGroupSize { get { return InitialActiveGroupSize * activeGroupMultiplier; }}

	private void OnEnable()
	{
		RevealGrade.SafeStage += HandleSafeStage;
		RevealGrade.FirstStage += HandleFirstStage;
		RevealGrade.SecondStage += HandleSecondStage;
		RevealGrade.ThirdStage += HandleThirdStage;
	}

	private void OnDisable()
	{
		RevealGrade.SafeStage -= HandleSafeStage;
        RevealGrade.FirstStage -= HandleFirstStage;
        RevealGrade.SecondStage -= HandleSecondStage;
        RevealGrade.ThirdStage -= HandleThirdStage;
	}

	void Awake()
	{
		idleUnits = new HashSet<Unit> ();
        capturedBuildings = new HashSet<Building>();
        activeUnitGroup = new HashSet<WorldObject>();
		player = GetComponent<Player>();
	}

	void Start()
	{
        safePoints = serializedSafePoints.Select(transform => transform.position).ToList();
	}

	void Update()
	{
		if (idleUnits.Count > 0) {
            if(capturedBuildings.Count > 0)
            {
				MoveGroupToTheBuilding(capturedBuildings.First());
            }
            else
            {
                engageUnits();
            }
		}
	}

	public void Alarm(Unit enemy, Transform origin)
    {
        foreach (Unit unit in units)
        {
            if (Vector3.Distance(unit.transform.position, origin.transform.position) < alarmRadius)
            {
                if (!unit.isAttacking())
                {
                    unit.AssignAction(new AttackInteraction(unit, enemy));
                }
            }
        }
    }

    public void spawnedUnit(Unit unit)
    {
        if (units == null)
        {
            units = new HashSet<Unit>();
        }
        else
        {
            units.Add(unit);
        }
    }

    public void becameIdle(Unit unit)
    {
		if(activeUnitGroup.Contains(unit) && groupIsActive)
		{
			return; // don't interrupt active group while they're moving to the building
		}
        if (patrolPaths.Count == 0)
            return;
        int i = Random.Range(0, patrolPaths.Count);
        patrolPaths[i].AssignPath(unit, 20);
    }

    public void deadUnit(Unit unit)
    {
        units.Remove(unit);
        idleUnits.Remove(unit);
        activeUnitGroup.Remove(unit);
    }

	private void engageUnits()
	{
		int averageCount = idleUnits.Count / patrolPaths.Count;
		int modulo = idleUnits.Count % patrolPaths.Count;

		int i = 0, j = 0;
		foreach (var unit in idleUnits) {
            if (activeUnitGroup.Count < ActiveGroupSize)
            {
                activeUnitGroup.Add(unit);
            }   
			patrolPaths [i].AssignPath (unit, 20);
			j++;
			if (j == averageCount) {
				j = 0;
				i = (i == patrolPaths.Count) ? i : i + 1;
			}
		}

		idleUnits.Clear ();
	}

    private void MoveGroupToTheBuilding(Building building)
    {
        var buildingPos = building.transform.position;
		var closestSafePoint = safePoints.Min(vector3 => Vector3.Distance(buildingPos, vector3));
		StartCoroutine(GroupMovement(closestSafePoint, building));
    }

	private IEnumerator GroupMovement(Vector3 safePoint, Building building)
	{
		ActionHandler.MoveUnitsWithFormation(safePoint, activeUnitGroup); // gather all units
		while(ActionHandler.IsGroupIdle(activeUnitGroup))
		{
			yield return new WaitForSeconds(1f);
		}
		ActionHandler.Enter(building, activeUnitGroup); // move them
	}

    private void HandleSafeStage()
	{
		if (Player.HumanPlayer.isEnemy(player))
		{
			SpawnCoefficient = 0;
		}
	}

    private void HandleFirstStage()
	{
		if (Player.HumanPlayer.isEnemy(player))
        {
			SpawnCoefficient = 2;
			activeGroupMultiplier = 2;
        }
	}

    private void HandleSecondStage()
	{
		if (Player.HumanPlayer.isEnemy(player))
        {
			SpawnCoefficient = 3;
			activeGroupMultiplier = 3;
        }
	}

    private void HandleThirdStage()
	{
		if (Player.HumanPlayer.isEnemy(player))
        {
			SpawnCoefficient = 8;
			activeGroupMultiplier = 4;
        }
	}
}
