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
		if (Input.GetKeyDown (KeyCode.T)) {
			Talk (true);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			Speed (2f);
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			ReceivedDamage ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			Attack (true);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			Dead ();
		}
	}

	public void Speed(float speed){
		animator.SetFloat ("Speed", speed);
	}

	public void ReceivedDamage(){
		animator.SetTrigger ("Damage");
		if (weapon == PeopleWeapon.NONE) {
			animator.SetInteger ("UnarmedHit", Random.Range (1, 7));
		} else {
			animator.SetInteger ("ArmedHit", Random.Range (1, 7));
		}
	}

	public void Attack(bool fighting){
		animator.SetBool ("Fight", fighting);
		if (fighting) {
			
			if (weapon == PeopleWeapon.NONE) {
				animator.SetInteger ("HandAttack", Random.Range (1, 7));
			} else {
				animator.SetInteger ("PistolAttack", Random.Range (1, 4));
			}
		}
	}

	public void Dead(){
		animator.SetBool ("Death", true);
	}

	public void Talk(bool talking){
		animator.SetBool ("Conversation", talking);
		if(talking)
			animator.SetInteger ("ConversationVariant", Random.Range(1, 9));
	}

	//Placeholder functions for Animation events
	public void Hit(){}
	public void Shoot(){}
	public void FootR(){}
	public void FootL(){}
	public void Land(){}
	public void WeaponSwitch(){}

}
