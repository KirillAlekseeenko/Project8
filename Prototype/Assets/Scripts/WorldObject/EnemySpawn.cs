using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	const float spawnInterval = 10.0f;

	[SerializeField] private Player owner; // set in the editor

	[SerializeField] private List<Unit> unitsToSpawn;

	private Spawn attachedSpawn;

	public Player Owner { get { return owner; } }

	void Awake()
	{
		attachedSpawn = GetComponent<Spawn> ();
	}

	void Start()
	{
		StartCoroutine (spawning ());
	}

	private IEnumerator spawning()
	{
		while (true) {
			yield return new WaitForSeconds (spawnInterval);
			spawn ();
		}
	}

	private void spawn()
	{
		var unit = unitsToSpawn [Random.Range (0, unitsToSpawn.Count)];
		unit.Owner = owner;
		attachedSpawn.SpawnUnit (unit);
	}
}
