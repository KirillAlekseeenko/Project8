using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	[SerializeField] private float timeToExplosion;
	[SerializeField] private float explodeRadius;
	[SerializeField] private int damage;
	[SerializeField] private ParticleSystem explosionPrefab;

	public Player Owner { get; set; }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			StartCoroutine (explosionCoroutine ());
		}
	}

	private IEnumerator explosionCoroutine()
	{
		yield return new WaitForSeconds (timeToExplosion);
		explode ();
	}
	private void explode()
	{
		makeDamage ();
		spawnParticle ();
		Destroy (gameObject);
	}
	private void spawnParticle()
	{
		var explosion = Instantiate (explosionPrefab.gameObject, transform.position, Quaternion.identity);
		explosion.transform.localScale = Vector3.one * explodeRadius;
	}
	private void makeDamage()
	{
		var colliders = Physics.OverlapSphere (transform.position, explodeRadius, LayerMask.GetMask("Unit"));

		foreach (var collider in colliders) {
			var unit = collider.gameObject.GetComponent<Unit> ();
			if (unit != null) {
				if (Owner.isEnemy(unit.Owner)) {
					unit.SufferDamage (damage);
				}
			}
		}
	}

}
