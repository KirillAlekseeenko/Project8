using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo : MonoBehaviour {

	private static BuffInfo instance;

	public static BuffInfo Instance {
		get {
			return instance;
		}
	}

	void Awake()
	{
		if (instance == null) { 
			instance = this;
		} else if(instance == this){
			Destroy(gameObject);
		}
	}

	public ParticleSystem ShockedParticle;
	public ParticleSystem RagedParticle;
}
