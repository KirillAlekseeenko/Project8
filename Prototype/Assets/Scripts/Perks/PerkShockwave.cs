using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerkShockwave : Perk {

	[SerializeField] private float range;
	[SerializeField] private float waveRange;
	[SerializeField] private float damage;
	[SerializeField] private float initialTime;
	[SerializeField] private ParticleSystem lightningPrefab;

	private IEnumerator waveSpreadCoroutine(Unit performer, Unit target)
	{
		var isHuman = performer.Owner.IsHuman;
		var currentUnit = target;
		Unit preUnit = performer;
		var time = initialTime;
		while (time >= 1.0f) {
			if (preUnit == null || currentUnit == null)
				break;
			//spawnLightning (preUnit.transform.position, currentUnit.transform.position); пока не нужно, т.к. нет нормального партикла))
			Buff.AddBuff<ShockedDebuff> (currentUnit, time);
			time -= 1.0f;
			var colliders = Physics.OverlapSphere (currentUnit.transform.position, waveRange, LayerMask.GetMask ("Unit"));
			Unit closestUnit = null;
			foreach (var collider in colliders) {
				var unit = collider.gameObject.GetComponent<Unit> ();
				if (unit != null && unit.Owner.IsHuman != isHuman && unit.GetComponent<ShockedDebuff>() == null) {
					if (closestUnit == null || Utils.Distance (closestUnit, currentUnit) > Utils.Distance (unit, currentUnit)) { // здесь мб еще добавить учет препятствий
						closestUnit = unit;
					}
				}
			}
			if (closestUnit == null)
				break;
			else {
				preUnit = currentUnit;
				currentUnit = closestUnit;
				yield return new WaitForSeconds (0.1f);
			}
		}
	}

	private void spawnLightning(Vector3 begin, Vector3 end)
	{
		Quaternion rotation = Quaternion.LookRotation (end - begin);
		var lightning = Instantiate (lightningPrefab.gameObject, begin, Quaternion.identity).GetComponent<ParticleSystem> ();

		float length = (end - begin).magnitude; // length of the particle
		lightning.startLifetime = length / lightning.main.startSpeed.constant;
		lightning.transform.localRotation = rotation;
		// setting up parameters
	}

	#region implemented abstract members of Perk

	protected override void initialize (Unit performer, Vector3? place = null, Unit target = null)
	{
		performer.GetComponent<NavMeshAgent> ().SetDestination (target.transform.position);
	}

	protected override void perform (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		StartCoroutine (waveSpreadCoroutine (performer, target));
	}

	protected override bool isReadyToPerform (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		var vectorToTarget = target.transform.position - performer.transform.position;
		var ray = new Ray (performer.transform.position, vectorToTarget);
		var rayLength = vectorToTarget.magnitude;
		RaycastHit hit;

		return (!Physics.Raycast (ray, out hit, rayLength, ~LayerMask.GetMask ("Unit")) && rayLength < range);
	}

	protected override void finish (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		performer.GetComponent<NavMeshAgent> ().ResetPath ();
	}

	#endregion

	#region implemented abstract members of Perk
	public override void Run (Unit performer, Vector3? place = default(Vector3?), Unit target = null)
	{
		if (target == null)
			Debug.LogError ("missing parameter");
		var action = new PerkAction (perform, isReadyToPerform, finish, initialize, performer, place, target);
		performer.AssignAction (action);
	}
	public override PerkType Type {
		get {
			return PerkType.Target;
		}
	}
	public override bool isReadyToFire {
		get {
			return true;
		}
	}
	#endregion
}
