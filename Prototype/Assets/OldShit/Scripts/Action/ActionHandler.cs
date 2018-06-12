using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionHandler : MonoBehaviour {

	[SerializeField] SelectionHandler selectionHandler;

	[SerializeField] KeyboardInput keyBoardInput;

    private const float FormationShift = 1.0f;

    public static void Attack(Unit unit, ICollection<WorldObject> unitGroup)
    {
        EngageUnits(unitGroup, worldObject => true, worldObject =>
        {
            worldObject.AssignAction(new AttackInteraction(worldObject as Unit, unit));
        });
    }

    public static void Enter(Building building, ICollection<WorldObject> unitGroup)
    {
        EngageUnits(unitGroup, worldObject => true, worldObject =>
        {
            worldObject.AssignAction(new EnterInteraction(worldObject as Unit, building));
        });
    }

    public static void Heal(Unit unit, ICollection<WorldObject> unitGroup)
    {
        EngageUnits(unitGroup, worldObject => worldObject.GetComponent<Scientist>() != null, worldObject =>
        {
            worldObject.AssignAction(new HealInteraction(worldObject as Unit, unit));
        });
    }

	public static void Crack(ControlPanel controlPanel, ICollection<WorldObject> unitGroup)
    {
        var firstHacker = unitGroup.FirstOrDefault(worldObject => worldObject.GetComponent<Hacker>() != null);
        if (firstHacker != null)
        {
			Debug.Log ("U crack you");
            firstHacker.AssignAction(new CrackInteraction(firstHacker as Unit, controlPanel));
        }
    }

    public static void MoveUnitsWithFormation(Vector3 position, ICollection<WorldObject> unitGroup)
	{
		Vector3 center = Vector3.zero;

        foreach (WorldObject worldObject in unitGroup) {
			center += worldObject.transform.position; 
		}

        center /= unitGroup.Count;
		float height = center.y;
		
        int squareSize = GetNextSquare(unitGroup.Count);

		Vector3 direction = position - center;
		direction = new Vector3 (direction.x, 0, direction.y);

		var rotation = Quaternion.FromToRotation (Vector3.right, direction.normalized);

		Vector3 rightForward;

		if (squareSize % 2 == 0) {
			rightForward = new Vector3 (FormationShift * (squareSize / 2 - 0.5f), height,
                                        FormationShift * (squareSize / 2 - 0.5f));
		} else {
			rightForward = new Vector3 (FormationShift * (squareSize / 2), height,
                                        FormationShift * (squareSize / 2));
		}

		int horizontal = 0;
		int vertical = 0;

        foreach (WorldObject worldObject in unitGroup) {
			if (worldObject is Unit && worldObject.Owner.IsHuman) {
				
				Vector3 unitPos = new Vector3 (rightForward.x - vertical * FormationShift,
                                               height, 
                                               rightForward.z - horizontal * FormationShift);

				unitPos += position;
				horizontal++;
				if (horizontal == squareSize) {
					horizontal = 0;
					vertical++;
				}
                
				MoveAction move = new MoveAction (worldObject as Unit, new Vector3(unitPos.x, height, unitPos.z));
				worldObject.AssignAction (move);
			}
		}
	}

	public static bool IsGroupIdle(ICollection<WorldObject> unitGroup)
	{
		return unitGroup.All(worldObject => worldObject.isIdle());
	}

    public void AssignAction(Vector3 clickPosition)
    {
        var ray = Camera.main.ScreenPointToRay(clickPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                MoveUnitsWithFormation(hit.point, selectionHandler.SelectedUnits);
            }
            else
            {
                var enemyUnit = hit.collider.gameObject.GetComponent<Unit>();
                var building = hit.collider.gameObject.GetComponent<Building>();
				var item = hit.collider.gameObject.GetComponent<ControlPanel>();
                if (enemyUnit != null && enemyUnit.IsVisibleInGame)
                {
                    if (Player.HumanPlayer.isEnemy(enemyUnit.Owner))
                    {
                        Attack(enemyUnit, selectionHandler.SelectedUnits);
                    }
                    else if (Player.HumanPlayer.isFriend(enemyUnit.Owner))
                    {
                        Heal(enemyUnit, selectionHandler.SelectedUnits);
                    }
                }
				if (item != null) {
					Crack (item, selectionHandler.SelectedUnits);
				}
                if (building != null)
                {
                    Enter(building, selectionHandler.SelectedUnits);
                }
            }

        }
    }

    private static void EngageUnits(ICollection<WorldObject> unitGroup,
                                    System.Func<WorldObject, bool> predicate,
                                    System.Action<WorldObject> action)
    {
        foreach(var worldObject in unitGroup)
        {
            if (worldObject is Unit && worldObject.Owner.IsHuman && predicate(worldObject))
                action(worldObject);
        }
    }

    private static int GetNextSquare(int x)
    {
        int result = 1;
        while (result * result < x)
            result++;
        return result;
    }
}
