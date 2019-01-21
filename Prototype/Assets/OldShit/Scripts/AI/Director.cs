using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Director : MonoBehaviour {

    private const int GroupsCount = 10;  // move it to config or sth like this
    private const int InitialActiveGroupSize = 2; // it will depend on grades values in future updates
	private int activeGroupMultiplier = 0;

	private Player player;

	[SerializeField] private float alarmRadius;
    [SerializeField] List<Path> patrolPaths;
    [SerializeField] List<Transform> keyPoints;
    [SerializeField] List<Building> enemyBuildings;

    HashSet<Unit> units;
	HashSet<Unit> idleUnits;

    private Queue<IUnitGroup> pendingUnitGroups = new Queue<IUnitGroup>();
    private List<IUnitGroup> unitGroups = new List<IUnitGroup>();

    private Timer updateGroupTimer = new Timer(2.0f);

	public float SpawnCoefficient { get; private set; }
	private int GroupSize { get { return InitialActiveGroupSize * activeGroupMultiplier; }}

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
		player = GetComponent<Player>();
        unitGroups.AddRange(Enumerable.Range(0, GroupsCount).Select(i => new UnitGroup(player)));
    }

	void Update()
	{
        if (idleUnits.Count > 0)
        {
            engageUnits();
        }
        updateGroupTimer.UpdateTimer(Time.deltaTime);
        if(updateGroupTimer.IsSet)
        {
            UpdateGroups();
            updateGroupTimer.Reset();
        }
	}

	public void Alarm(Vector3 enemyPosition, Transform origin)
    {
        foreach (Unit unit in units)
        {
            if (Vector3.Distance(unit.transform.position, origin.transform.position) < alarmRadius)
            {
                if (!unit.isAttacking())
                {
                    unit.AssignAction(new MoveAction(unit, enemyPosition));
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

        if (pendingUnitGroups.Count == 0) return;

        var groupToSupplement = pendingUnitGroups.Peek();

        if (groupToSupplement.Count < GroupSize) groupToSupplement.AddUnit(unit, GroupSize);
        else pendingUnitGroups.Dequeue();
        units.Add(unit);
    }

    public void becameIdle(Unit unit)
    {
        if (unitGroups.Any(g => g.Contains(unit))) return;
        if (patrolPaths.Count == 0) return;
        int i = Random.Range(0, patrolPaths.Count);
        patrolPaths[i].AssignPath(unit, 20);
    }

    public void deadUnit(Unit unit)
    {
        units.Remove(unit);
        idleUnits.Remove(unit);
        unitGroups.ForEach(ug => ug.RemoveUnit(unit)); //TODO: optimize
    }

    public void OnBuildingCapture(Building building)
    {

    }

    public void OnBuildingReturn(Building building)
    {

    }

	private void engageUnits()
	{
		int averageCount = idleUnits.Count / patrolPaths.Count;
		int modulo = idleUnits.Count % patrolPaths.Count;

		int i = 0, j = 0;
		foreach (var unit in idleUnits) { 
			patrolPaths [i].AssignPath (unit, 20);
			j++;
			if (j == averageCount) {
				j = 0;
				i = (i == patrolPaths.Count) ? i : i + 1;
			}
		}

		idleUnits.Clear();
	}

    private void HandleSafeStage()
	{
		if (Player.HumanPlayer.isEnemy(player))
		{
			SpawnCoefficient = 0;
            activeGroupMultiplier = 0;
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

    private void UpdateGroups()
    {
        unitGroups.Where(ug => ug.Count == 0).ToList().ForEach(ug => pendingUnitGroups.Enqueue(ug));

        var activeGroups = unitGroups.Where(ug => ug.IsLocked).ToList();
        activeGroups.ForEach(ug => ug.Update());
        activeGroups.Where(ug => ug.IsIdle).ToList().ForEach(ug =>
        {
            var target = GetNextTarget();
            if (target != null) ug.Enter(target);
        });
    }

    private Building GetNextTarget()
    {
        return enemyBuildings.FirstOrDefault();
    }
}
