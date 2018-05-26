using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStateMashineAnim : StateMachineBehaviour {

	public int NumberOfRandomAnimations; 
	public string Parameter;

	override public void OnStateMachineEnter(Animator animator, int stateMachinePathMesh){
		animator.SetInteger (Parameter, Random.Range(1, NumberOfRandomAnimations + 1));
	}
}
