using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;

public class ManAnimations : MonoBehaviour {

	public enum PeopleWeapon{
		NONE,
		PISTOL
	};

	public enum Sex{
		MALE,
		FEMALE
	};

	[SerializeField]
	private Sex sex;
	[SerializeField]
	private PeopleWeapon weapon;
	public Animator animator;

	private bool canAction;

	private void Awake(){
		if (weapon == PeopleWeapon.NONE) {
			animator.SetBool ("IsArmed", false);
		} else {
			animator.SetBool ("IsArmed", true);
			if (weapon == PeopleWeapon.PISTOL)
				animator.SetBool ("HasPistol", true);
		}

		if (sex == Sex.MALE) {
			animator.SetBool ("Sex", false);
		} else {
			animator.SetBool ("Sex", true);
		}
	}

	private void Update(){
		if (Input.GetKeyDown (KeyCode.Y)) {
			Talk (true);
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			Idle ();
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			SlowWalk ();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			Walk ();
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			Run ();
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			ReceivedDamage ();
		}

		if (Input.GetKeyDown (KeyCode.O)) {
			Attack (true);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			Dead ();
		}
	
		if (!canAction) {
			animator.SetBool ("Animate", false);
		}
	}

	private void startRandCoroutine(string animatorParam, int numsCount, float time){
		StopAllCoroutines ();
		StartCoroutine (resetRandomNum(animatorParam, numsCount, time));
	}

	private IEnumerator resetRandomNum(string animatorParam, int numsCount, float time){
		while (true) {
			animator.SetInteger (animatorParam, Random.Range (1, numsCount));
			yield return new WaitForSeconds (time);
		}
	}

	public void Idle(){
		Debug.Log ("hesoyam");
		animator.SetTrigger ("Interrupt");
		animator.SetInteger ("Speed", 0);
	}

	public void SlowWalk(){
		animator.SetTrigger ("Interrupt");
		animator.SetInteger ("Speed", 1);
	}

	public void Walk(){
		animator.SetTrigger ("Interrupt");
		animator.SetInteger ("Speed", 2);
	}

	public void Run(){
		animator.SetTrigger ("Interrupt");
		animator.SetInteger ("Speed", 3);
	} 

	public void ReceivedDamage(){
		animator.SetTrigger ("Interrupt");
		animator.SetTrigger ("Damage");
		if (weapon == PeopleWeapon.NONE) {
			startRandCoroutine ("UnarmedHit", 6, 0.4f);
		} else {
			startRandCoroutine ("ArmedHit", 6, 0.4f);
		}
	}

	public void Attack(bool fighting){
		animator.SetTrigger ("Interrupt");
		animator.SetInteger ("Speed", 0);
		animator.SetTrigger ("Fight");
		if (fighting){
			switch (weapon) {
				case PeopleWeapon.NONE:{
					startRandCoroutine ("HandAttack", 7, 0.4f);	
					break;
				}
				case PeopleWeapon.PISTOL:{
					startRandCoroutine ("PistolAttack", 4, 0.4f);	
					break;
				}
			}
		}
	}

	public void Dead(){
		animator.SetTrigger ("Interrupt");
		animator.SetBool ("Death", true);
		canAction = false;
	}

	public void Talk(bool talking){
		animator.SetTrigger ("Interrupt");
		animator.SetBool ("Conversation", talking);
		if(talking)
			startRandCoroutine ("ConversationVariant", 9, 0.4f);
	}

	//Placeholder functions for Animation events
	public void Hit(){}
	public void Shoot(){}
	public void FootR(){}
	public void FootL(){}
	public void Land(){}
	public void WeaponSwitch(){}

}
