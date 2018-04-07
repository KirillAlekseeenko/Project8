using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitAnimations {

	public static void SetStartConditions(this Unit unit){
		if (unit.weapon == Unit.PeopleWeapon.NONE) {
			unit.animator.SetBool ("IsArmed", false);
		} else {
			unit.animator.SetBool ("IsArmed", true);
			switch (unit.weapon) {
			case Unit.PeopleWeapon.PISTOL:
				{
					unit.animator.SetBool ("HasPistol", true);	
					break;
				}
			case Unit.PeopleWeapon.FISTS:
				{
					unit.animator.SetBool ("HasFists", true);	
					break;
				}
			case Unit.PeopleWeapon.RIFLE:
				{
					unit.animator.SetBool ("HasRifle", true);	
					break;
				}
			}
		}

		if (unit.sex == Unit.Sex.MALE) {
			unit.animator.SetBool ("Sex", false);
		} else {
			unit.animator.SetBool ("Sex", true);
		}
	}

	public static void HearAgitator(this Unit unit){
		unit.animator.SetTrigger ("Interrupt");
	}

	public static void Idle(this Unit unit){
		unit.animator.SetInteger ("Speed", 0);
	}

	public static void SlowWalk(this Unit unit){
		unit.animator.SetTrigger ("Interrupt");
		unit.animator.SetInteger ("Speed", 1);
	}

	public static void Walk(this Unit unit){
		unit.animator.SetTrigger ("Interrupt");
		unit.animator.SetInteger ("Speed", 2);
	}

	public static void Run(this Unit unit, Vector3 velocity, float speed){
		if (unit.gameObject.GetComponent<Citizen> () != null) {
			SlowWalk (unit);
		} else {
			unit.animator.SetInteger ("Speed", 3);
			float velX = unit.transform.InverseTransformDirection (velocity).x / speed;
			float velZ = unit.transform.InverseTransformDirection (velocity).z / speed;
			unit.animator.SetFloat ("VelocityX", velX);
			unit.animator.SetFloat ("VelocityZ", velZ);
		}
	} 

	public static void ReceivedDamage(this Unit unit){
		unit.animator.SetTrigger ("Interrupt");
		unit.animator.SetTrigger ("Damage");
	}

	public static void Attack(this Unit unit){		
		unit.animator.SetTrigger ("Interrupt");
		unit.animator.SetTrigger ("Fight");
	} 

	public static void Dead(this Unit unit){
		unit.animator.SetTrigger ("Interrupt");
		unit.animator.SetBool ("Death", true);
	}

	public static void Talk(this Unit unit){
		unit.animator.SetTrigger ("Interrupt");
		unit.animator.SetInteger ("Speed", 0);
		unit.animator.SetTrigger ("Conversation");
	}

	public static void DistanceHack(this Unit unit){
		
	}


}