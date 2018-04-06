using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationStart : StateMachineBehaviour {

	private bool randUpdated;

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		if (!randUpdated) {
			randUpdated = true;
			animator.SetFloat ("RandomAnimationStart", Random.Range (0f, 0.3f));
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		randUpdated = false;
	}
}
