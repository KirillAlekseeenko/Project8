using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerkGrenade : Perk {

	[SerializeField] private float throwRange;
	[SerializeField] [Range(0, 90.0f)] private float throwAngle;

	[SerializeField] private GameObject grenadePrefab;

	/*private void throwGrenade(Unit performer, Vector3 targetPos)
	{
		var grenadeRigidbody = Instantiate (grenadePrefab, performer.transform.position, Quaternion.identity).GetComponent<Rigidbody> ();
		grenadeRigidbody.gameObject.GetComponent<Grenade> ().Owner = performer.Owner;
		var vectorToTarget = targetPos - performer.transform.position;
		var distance = vectorToTarget.magnitude;
		var g = Physics.gravity.magnitude;
		var angleInRadians = throwAngle * Mathf.Deg2Rad;
		var velocityMagnitute = Mathf.Sqrt (g * distance / (2 * Mathf.Sin (angleInRadians) * Mathf.Cos (angleInRadians)));

		vectorToTarget = new Vector3(targetPos.x, Mathf.Tan(angleInRadians) * distance, targetPos.z) - performer.transform.position;
		grenadeRigidbody.velocity = vectorToTarget.normalized * velocityMagnitute;
	}*/
	//нужно было запустить через какое-то время
	private IEnumerator throwGrenade(Unit performer, Vector3 targetPos)
	{
		yield return new WaitForSeconds (1.5f);
		var grenadeRigidbody = Instantiate (grenadePrefab, performer.transform.position, Quaternion.identity).GetComponent<Rigidbody> ();
		grenadeRigidbody.gameObject.GetComponent<Grenade> ().Owner = performer.Owner;
		var vectorToTarget = targetPos - performer.transform.position;
		var distance = vectorToTarget.magnitude;
		var g = Physics.gravity.magnitude;
		var angleInRadians = throwAngle * Mathf.Deg2Rad;
		var velocityMagnitute = Mathf.Sqrt (g * distance / (2 * Mathf.Sin (angleInRadians) * Mathf.Cos (angleInRadians)));

		vectorToTarget = new Vector3(targetPos.x, Mathf.Tan(angleInRadians) * distance, targetPos.z) - performer.transform.position;
		grenadeRigidbody.velocity = vectorToTarget.normalized * velocityMagnitute;
	}

	#region implemented abstract members of Perk

	protected override void initialize (Unit performer, Vector3? place = null, Unit target = null)
	{
		if (!place.HasValue)
			Debug.LogError ("missing parameter");
		performer.GetComponent<NavMeshAgent> ().SetDestination (place.Value);
	}

	protected override void derivedPerform (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		performer.GetComponent<NavMeshAgent> ().ResetPath (); // stop
		performer.transform.LookAt(new Vector3(place.Value.x, performer.transform.position.y, place.Value.z));

		//////////////////////////////////Animation
		performer.UsePerk();
		StartCoroutine (throwGrenade (performer, new Vector3(place.Value.x, performer.transform.position.y, place.Value.z)));
		//throwGrenade(performer, new Vector3(place.Value.x, performer.transform.position.y, place.Value.z)); // для простоты одинаковая высота
	}

	protected override bool isReadyToPerform (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		var vectorToTarget = place.Value - performer.transform.position;
		var ray = new Ray (performer.transform.position, vectorToTarget);
		var rayLength = vectorToTarget.magnitude;
		RaycastHit hit;

		return (!Physics.Raycast (ray, out hit, rayLength, ~LayerMask.GetMask ("Unit", "Ground")) && rayLength < throwRange);
	}

	protected override void finish (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		performer.GetComponent<NavMeshAgent> ().ResetPath ();
	}

	#endregion

	#region implemented abstract members of Perk
	public override PerkType Type {
		get {
			return PerkType.Ground;
		}
	}
	#endregion

}
