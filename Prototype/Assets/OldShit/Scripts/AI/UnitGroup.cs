using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * All group logic will be here
 * 
 */

public interface IUnitGroup
{
    int Count { get; }
    Player Owner { get; }
    bool IsIdle { get; }
    bool IsLocked { get; }
    void AddUnit(Unit unit, int unitsCap);
    void RemoveUnit(Unit unit);
    bool Contains(Unit unit);
    void DismissGroup();
    void Move(Vector3 pos);
    void Attack(Unit enemy);
    void Enter(Building building);
    void Update();
}

public class UnitGroup : IUnitGroup
{
    private const float ScatterDegree = 2.0f;

    private readonly ICollection<Unit> units;
    private readonly Player owner;

    private bool isLocked;

    public UnitGroup(Player player)
    {
        units = new HashSet<Unit>();
        owner = player;
    }

    int IUnitGroup.Count => units.Count;
    Player IUnitGroup.Owner => owner;
    bool IUnitGroup.IsIdle => units.All(unit => unit.isIdle());
    bool IUnitGroup.IsLocked => isLocked;

    void IUnitGroup.AddUnit(Unit unit, int unitsCap)
    {
        units.Add(unit);
        if (units.Count >= unitsCap) isLocked = true;
    }

    void IUnitGroup.RemoveUnit(Unit unit)
    {
        units.Remove(unit);
        if (units.Count == 0) isLocked = false;
    }

    bool IUnitGroup.Contains(Unit unit)
    {
        return units.Contains(unit);
    }

    void IUnitGroup.Attack(Unit enemy)
    {
        foreach(var unit in units)
        {
            unit.AssignAction(new AttackInteraction(unit, enemy));
        }
    }

    void IUnitGroup.DismissGroup()
    {
        foreach(var unit in units)
        {
            unit.Stop();
        }
        units.Clear();
    }

    void IUnitGroup.Enter(Building building)
    {
        foreach (var unit in units)
        {
            unit.AssignAction(new EnterInteraction(unit, building));
        }
    }

    void IUnitGroup.Move(Vector3 pos)
    {
        foreach (var unit in units)
        {
            unit.AssignAction(new MoveAction(unit, pos));
        }
    }
    //TODO optimize
    void IUnitGroup.Update() // every 2 sec 
    {
        if(units.All(unit => !unit.isAttacking()))
        {
            if(AreUnitsScattered(ScatterDegree) && !units.Any(unit => unit.isIdle()))
            {
                ActionHandler.MoveUnitsWithFormation(units.First().transform.position, units.Select(unit => (WorldObject)unit).ToList());
            }
        }
        else
        {
            var unitNeedSupport = units.First(unit => unit.isAttacking());
            ActionHandler.MoveUnitsWithFormation(
                unitNeedSupport.transform.position, 
                units.Where(unit => !unit.Equals(unitNeedSupport) && !unit.isAttacking()).Select(unit => (WorldObject)unit).ToList());
        }
    }

    private bool AreUnitsScattered(float scatterDegree)
    {
        var middlePoint = units.Aggregate(Vector3.zero, (current, next) => current + next.transform.position) / units.Count;
        return units.Any(unit => Vector3.Distance(middlePoint, unit.transform.position) > scatterDegree);
    }
}

