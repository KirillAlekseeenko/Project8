using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLocalAI : MonoBehaviour {

	private Unit unitComponent;

	private HashSet<Unit> visibleObjects;

	public HashSet<Unit> VisibleObjects {
		get {
			return visibleObjects;
		}
	}

	void Awake()
	{
		visibleObjects = new HashSet<Unit> ();
	}

	void Start()
	{
		unitComponent = GetComponent<Unit> ();
		StartCoroutine (updateCoroutine());
	}


	private IEnumerator updateCoroutine()
	{
		while (true) {
			checkForEnemies ();
			yield return new WaitForSeconds (0.2f);
		}
	}


	void Update()
	{
		//if (unitComponent.isIdle ()) {
			//checkForEnemies ();
		//}
	}

	// debug

	[SerializeField]
	private int visibleObjectCount;

	private void checkForEnemies()
	{
		var colliders = Physics.OverlapSphere (transform.position, unitComponent.pLOS, LayerMask.GetMask ("Unit"));

		var visible = 0;

		foreach (var collider in colliders) {
			var unit = collider.gameObject.GetComponent<Unit> ();
			if (unit == null)
				continue;
			if (!unit.Owner.IsHuman) {
				var vectorToEnemy = unit.transform.position - unitComponent.transform.position;
				RaycastHit hit;
				if (!Physics.Raycast (new Ray (transform.position, vectorToEnemy),out hit, vectorToEnemy.magnitude,LayerMask.GetMask ("Building"))) {

					if (unitComponent.isIdle ()) {
						unitComponent.AssignAction (new AttackInteraction (unitComponent, unit));
					}
					unit.SetVisible ();
					visible++;

				} 
			}
		}

		visibleObjectCount = visible;
	}
		
		
}
