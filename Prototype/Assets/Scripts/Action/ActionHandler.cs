using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour {

	[SerializeField]
	SelectionHandler selectionHandler;

	[SerializeField]
	KeyboardInput keyBoardInput;

	private float formationShift = 1.0f;

	public void AssignAction( Vector3 clickPosition )
	{
		var ray = Camera.main.ScreenPointToRay (clickPosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
				moveObjets (hit.point);
			} else {
				var enemyUnit = hit.collider.gameObject.GetComponent<Unit> ();
				if (enemyUnit != null && !enemyUnit.Owner.IsHuman) {
					attack (enemyUnit);
				}
			}

		}
	}

	private void moveObjets(Vector3 position)
	{
		
		Vector3 center = Vector3.zero;

		foreach (WorldObject worldObject in selectionHandler.SelectedUnits) {
			center += worldObject.transform.position; 
		}

		center /= selectionHandler.SelectedUnits.Count;


		float height = center.y;
		
		int squareSize = getNextSquare(selectionHandler.SelectedUnits.Count);

		Vector3 direction = position - center;
		direction = new Vector3 (direction.x, 0, direction.y);

		var rotation = Quaternion.FromToRotation (Vector3.right, direction.normalized);

		Vector3 rightForward;

		if (squareSize % 2 == 0) {
			rightForward = new Vector3 (formationShift * (squareSize / 2 - 0.5f), height, formationShift * (squareSize / 2 - 0.5f));
		} else {
			rightForward = new Vector3 (formationShift * (squareSize / 2), height, formationShift * (squareSize / 2));
		}


		//Debug.Log (rightForward);
		//Debug.Log (rotation.eulerAngles);
		//Debug.DrawLine (center, position, Color.green);
		//Debug.DrawLine (position, position + rightForward, Color.green);
		//Debug.DrawLine (position, position + rotation * rightForward, Color.red);


		int horizontal = 0;
		int vertical = 0;

		foreach (WorldObject worldObject in selectionHandler.SelectedUnits) {
			if (worldObject is Unit && worldObject.Owner.IsHuman) {
				
				Vector3 unitPos = new Vector3 (rightForward.x - vertical * formationShift, height, rightForward.z - horizontal * formationShift);
				//unitPos = rotation * unitPos;
				unitPos += position;
				horizontal++;
				if (horizontal == squareSize) {
					horizontal = 0;
					vertical++;
				}


				MoveAction move = new MoveAction (worldObject as Unit, unitPos );
				worldObject.AssignAction (move);
			}
		}
	}

	private void attack(Unit unit)
	{
		foreach (WorldObject worldObject in selectionHandler.SelectedUnits) {
			if (worldObject is Unit && worldObject.Owner.IsHuman) {
				AttackInteraction action = new AttackInteraction (worldObject as Unit, unit);
				worldObject.AssignAction (action);
			}
		}
	}

	private int getNextSquare(int x)
	{
		int result = 1;
		while (result * result < x)
			result++;
		return result;
	}

}
