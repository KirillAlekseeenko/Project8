using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * All group logic will be here
 * 
 */

public class UnitGroup
{
	private readonly ICollection<Unit> unitGroup;

	public UnitGroup()
	{
		unitGroup = new HashSet<Unit>();
	}
	public UnitGroup(ICollection<Unit> unitGroup)
	{
		this.unitGroup = unitGroup;
	}

	public ICollection<Unit> Group { get { return unitGroup; } }

    public bool IsIdle()
	{
		return unitGroup.All(unit => unit.isIdle());
	}

    public void MoveGroupToThePosition(Vector3 position)
	{
		
	}

}

