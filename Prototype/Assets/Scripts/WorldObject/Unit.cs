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
	private float reloadCounter;

	// visibility
	[SerializeField]
	private bool _isVisible = false;
	protected MeshRenderer meshRenderer;
	private float visibilityCounter;
	private float visibilityUpdateTime = 0.5f;

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

	public override bool IsVisible {
		get {
			return _isVisible || owner.IsHuman;
		}
		protected set {
			if (value != _isVisible) {
				if (value) {
					BecomeVisible ();
				} else {
					BecomeInvisible ();
				}
			}
			_isVisible = value;
		}
	}	


		

	protected void Awake()
	{
		base.Awake ();
		if (!owner.IsHuman) {
			//gameObject.AddComponent<VisionArcComponent> ();
			gameObject.AddComponent<LocalAI> ();
		}
		else
			gameObject.AddComponent<PlayerLocalAI> ();
		hp = baseHP;
		healthBarRectTransform = healthBar.GetComponent<RectTransform> ();
		meshRenderer = GetComponent<MeshRenderer> ();

		visibilityCounter = 0;
	}

	protected void Start()
	{
		base.Start ();

		if (!owner.IsHuman)
			GetComponent<MeshRenderer> ().enabled = false;

		var navMesh = GetComponent<NavMeshAgent> ().speed = this.speed;
		reloadTime = 1 / longRangeAttackPerSecond;
	}

	protected void Update()
	{
		base.Update ();

		//reload
		if (reloadCounter < reloadTime) {
			reloadCounter += Time.deltaTime;
		}

		if (visibilityCounter < visibilityUpdateTime) {
			visibilityCounter += Time.deltaTime;
		} else {
			visibilityCounter = 0;
			IsVisible = false;
		}

		if (halo.activeInHierarchy) {
			var x = canvas.transform.rotation.eulerAngles.x;
			canvas.transform.rotation = Quaternion.Euler(x, 0, 0);
		}
	}

	public bool Fire()
	{
		bool result = (reloadCounter >= reloadTime);
		if (result) {
			reloadCounter = 0;
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

	public void SetVisible()
	{
		IsVisible = true;
		visibilityCounter = 0;
	}

	private void BecomeVisible()
	{
		Manager.Instance.fieldOfViewHandler.Add (this);
		meshRenderer.enabled = true;

	}
	private void BecomeInvisible()
	{
		Manager.Instance.fieldOfViewHandler.Remove (this);
		meshRenderer.enabled = false;
		Dehighlight ();
	}

	private void die()
	{
		if (owner.IsHuman) {
			Manager.Instance.selectionHandler.ObjectsInsideFrustum.Remove (GetComponent<WorldObject> ());
		} else {
			Manager.Instance.fieldOfViewHandler.Remove (GetComponent<Unit> ());
		}

		Manager.Instance.selectionHandler.SelectedUnits.Remove (GetComponent<WorldObject> ());
		Manager.Instance.fogOfWarHanlder.UpdateFogQuery ();

		Destroy (gameObject);
	}



	private void updateUI()
	{
		healthBarRectTransform.localScale = new Vector3 ((float)hp / (float)baseHP, 1, 1);
	}




}
