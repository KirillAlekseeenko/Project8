using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

	[SerializeField]
	private string username;  // задел под мультиплеер ))0)
	[SerializeField]
	private bool isHuman; 
	[SerializeField]
	private Team team; // мб в будущем будут союзники
	[SerializeField]
	private Color color;

	public bool IsHuman {
		get {
			return isHuman;
		}
	}

	public Color Color {
		get {
			return color;
		}
	}
}

public enum Team{One, Two, Three, Four};
