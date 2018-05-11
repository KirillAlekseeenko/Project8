using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path {

	[SerializeField] List<Transform> pathNodes;
	[SerializeField] private bool isEnclosed;

	public void AssignPath(Unit unit, int repeatCount)
	{
        for (int step = 0; step < repeatCount; step++)
        {
            for (int i = 0; i < pathNodes.Count; i++)
            {
                unit.AssignActionShift(new MoveAction(unit, pathNodes[i].position));
            }

            if (isEnclosed)
            {
                for (int i = 0; i < pathNodes.Count; i++)
                {
                    unit.AssignActionShift(new MoveAction(unit, pathNodes[i].position));
                }
            }
            else
            {
                for (int i = pathNodes.Count; i >= 0; i--)
                {
                    unit.AssignActionShift(new MoveAction(unit, pathNodes[i].position));
                }
            }
        }
	}
}
