using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : WorldObject {

	[Header("Stats:")]

	[SerializeField]
	protected int baseHP;
	protected int hp;

	[SerializeField]
	protected int meleeAttack;
	[SerializeField]
	protected float meleeAttackPerSecond;
	[SerializeField]
	protected int rangeAttack;
	[SerializeField]
	protected float longRangeAttackPerSecond;
	[SerializeField]
	protected float assaultSkill; // for buildings

	[SerializeField]
	protected float speed;

	[SerializeField]
	protected float meleeAttackRadius;
	[SerializeField]
	protected float longRangeAttackRadius;

	[SerializeField]
	protected float sneak; // 0 to 1



	[SerializeField]
	protected float LOS; // line of sight

	[Header("UI")]
	[SerializeField]
	protected Canvas canvas;

	[SerializeField]
	protected Image healthBar;

	protected RectTransform healthBarRectTransform;


	// reloading
	private float reloadTime;
	private float time;

	public float Sneak {
		get {
			return sneak;
		}
	}

	public int RangeAttack {
		get {
			return rangeAttack;
		}
	}

	public float pLOS {
		get {
			return LOS;
		}
	}

	public float LongRangeAttackPerSecond {
		get {
			return longRangeAttackPerSecond;
		}
	}

	public float LongRangeAttackRadius {
		get {
			return longRangeAttackRadius;
		}
	}
		

	protected void Awake()
	{
		base.Awake ();
		if (!owner.IsHuman) {
			gameObject.AddComponent<VisionArcComponent> ();
			gameObject.AddComponent<LocalAI> ();
		}
		else
			gameObject.AddComponent<PlayerLocalAI> ();
		hp = baseHP;
		healthBarRectTransform = healthBar.GetComponent<RectTransform> ();
	}

	protected void Start()
	{
		base.Start ();

		var navMesh = GetComponent<NavMeshAgent> ().speed = this.speed;
		reloadTime = 1 / longRangeAttackPerSecond;
	}

	protected void Update()
	{
		base.Update ();

		//reload
		if (time < reloadTime) {
			time += Time.deltaTime;
		}

		if (halo.activeInHierarchy) {
			var x = canvas.transform.rotation.eulerAngles.x;
			canvas.transform.rotation = Quaternion.Euler(x, 0, 0);
		}
	}

	public bool Fire()
	{
		bool result = (time >= reloadTime);
		if (result) {
			time = 0;
			return true;
		} else
			return false;
	}

	public override void Highlight ()
	{
		base.Highlight ();
		canvas.gameObject.SetActive (true);
	}
	public override void Dehighlight ()
	{
		base.Dehighlight ();
		canvas.gameObject.SetActive (false);
	}

	public void SufferDamage(int damage)
	{
		hp -= damage;
		updateUI ();
		if (hp <= 0)
			die ();

	}

	public bool isAttacking()
	{
		return actionQueue.Count > 0 && actionQueue.Peek () is AttackInteraction;
	}

	private void die()
	{
		if (owner.IsHuman) {
			Manager.Instance.selectionHandler.ObjectsInsideFrustum.Remove (GetComponent<WorldObject> ());
		} else {
			Manager.Instance.fieldOfViewHandler.Remove (GetComponent<Unit> ());
		}

		Manager.Instance.selectionHandler.SelectedUnits.Remove (GetComponent<WorldObject> ());

		Destroy (gameObject);
	}

	private void updateUI()
	{
		healthBarRectTransform.localScale = new Vector3 ((float)hp / (float)baseHP, 1, 1);
	}




}
