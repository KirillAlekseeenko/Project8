using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	// Use this for initialization

	[SerializeField] private Transform movePoint;
    [SerializeField] private float radius;

	//private Unit movedUnit;
	private bool moving;

	public void SpawnUnit(Unit unit)
	{
        var randomShift = (Vector3.forward * Random.Range(-1, 1) + Vector3.right * Random.Range(-1, 1)) * radius;
        Instantiate(unit.gameObject, transform.position + randomShift, Quaternion.identity);
		//moving = true;
        /*var citizen = movedUnit.GetComponent<Citizen>();
        if (citizen != null)
            citizen.StopClapping();*/
	}

	/*void Update(){
		if (moving) {
			if (Vector3.Distance (movedUnit.transform.position, movePoint.position) > 0.01) {
				movedUnit.GetComponent<Rigidbody> ().MovePosition (movePoint.position);
				moving = false;
			}
		}
	}*/
}
