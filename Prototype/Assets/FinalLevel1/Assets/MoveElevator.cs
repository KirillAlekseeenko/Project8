using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevator : MonoBehaviour {

	[SerializeField]
	private bool playing;
	[SerializeField]
	private Vector3 speed;
	[SerializeField]
	private Vector3 finishCoords;
	[SerializeField]
	private int pause;
	[SerializeField]
	private bool randomPauseOnPlay = false;
	[SerializeField]
	private bool isLooped;

	private Vector3 startCoords;
	private Vector3 temp;
	private float timer;

	private Vector3 startCoordsSave;
	private Vector3 finishCoordsSave;
	private Vector3 speedSave;

	void Start () {
		startCoords = new Vector3(transform.position.z, transform.position.y, transform.position.z);
		startCoordsSave = startCoords;
		finishCoordsSave = finishCoords;
		speedSave = speed;
		temp = new Vector3(0, 0, 0);
		timer = pause;

		if (randomPauseOnPlay)
			timer = Random.Range(1f, pause);
		else
			timer = 0;
	}

	void FixedUpdate () {
		if (playing) {
			if (isLooped) {
				if ((finishCoords.x - transform.position.x) * speed.x <= 0
					&& (finishCoords.y - transform.position.y) * speed.y <= 0
					&& (finishCoords.z - transform.position.z) * speed.z <= 0) {

					if (timer > 0) {
						timer -= Time.smoothDeltaTime;
					} else {
						temp = finishCoords;
						finishCoords = startCoords;
						startCoords = temp;
						speed = -speed;
						timer = pause;
					}
				} else {
					if (timer > 0) {
						timer -= Time.smoothDeltaTime;
					} else {
						gameObject.transform.position = new Vector3 (
							gameObject.transform.position.x + speed.x,
							gameObject.transform.position.y + speed.y,
							gameObject.transform.position.z + speed.z);
					}
				}
			} else {
				if ((finishCoords.x - transform.position.x) * speed.x >= 0
					&& (finishCoords.y - transform.position.y) * speed.y >= 0
					&& (finishCoords.z - transform.position.z) * speed.z >= 0) {

					gameObject.transform.position = new Vector3 (
						gameObject.transform.position.x + speed.x,
						gameObject.transform.position.y + speed.y,
						gameObject.transform.position.z + speed.z);
				} else {
					playing = false;
					temp = finishCoords;
					finishCoords = startCoords;
					startCoords = temp;
					speed = -speed;
				}
			}
		}
	}

	public bool Play{
		get { return playing; }
		set{playing = value;}
	}

	public Vector3 FinalCoordinates{
		get { return finishCoords; }
		set{finishCoords = value;}
	}

	public void Reset(){
		startCoords = startCoordsSave;
		finishCoords = finishCoordsSave;
		speed = speedSave;
		gameObject.transform.position = new Vector3 (
			startCoords.x,
			startCoords.y,
			startCoords.z);
		timer = pause;
	}

	public Vector2 StartCoordinates { get { return startCoordsSave; } }
}
