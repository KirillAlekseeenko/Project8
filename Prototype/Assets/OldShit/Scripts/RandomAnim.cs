using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnim : StateMachineBehaviour {

	public int NumberOfRandomAnimations; 
	public string Parameter;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		animator.SetInteger (Parameter, Random.Range(1, NumberOfRandomAnimations + 1));
	}
}
