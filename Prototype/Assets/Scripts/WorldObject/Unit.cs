using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : WorldObject {

	[Header("Stats:")]

	[SerializeField] protected int baseHP;
	protected int hp;

	[SerializeField] protected bool isRange;
	[SerializeField] protected int meleeAttack;
	[SerializeField] protected float meleeAttackPerSecond;
	[SerializeField] protected float meleeAttackRadius;
	[SerializeField] protected int rangeAttack;
	[SerializeField] protected float rangeAttackPerSecond;
	[SerializeField] protected float rangeAttackRadius;

	[SerializeField] protected float assaultSkill; // for buildings

	[SerializeField] protected float speed;

	[SerializeField] protected float sneak; // 0 to 1
	[SerializeField] protected bool halfVisible;

	[SerializeField] protected float LOS; // line of sight

	// buffs or debuffs
	public float SufferDamageMultiplier { get; set; }
	public float MakeDamageMultiplier { get; set; }
	public float LifeSteal { get; set; }
	public float AttackSpeedModifier{ get; set; }

	[Header("UI")]
	[SerializeField] protected Canvas canvas;
	[SerializeField] protected Image healthBar;
	protected RectTransform healthBarRectTransform;

	[Header("Perks")]
	[SerializeField] protected List<Perk> perkList;
	[SerializeField] protected ParticleSystem traceParticle;


	// reloading
	private float rangeReloadTime;
	private float meleeReloadTime;
	private float reloadCounter;

	[SerializeField] private bool _isVisible = false;
	protected MeshRenderer meshRenderer;
	private float visibilityCounter;
	private float visibilityUpdateTime = 0.5f;

	public float Sneak {
		get {
			return sneak;
		}
	}

	public bool HalfVisible {
		get {
			return halfVisible;
		}
	}

	public bool IsRange {
		get {
			return isRange;
		}
	}

	public int MeleeAttack {
		get {
			return meleeAttack;
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

	public float RangeAttackPerSecond {
		get {
			return rangeAttackPerSecond;
		}
	}

	public float RangeAttackRadius {
		get {
			return rangeAttackRadius;
		}
	}

	public float MeleeAttackRadius {
		get {
			return meleeAttackRadius;
		}
	}

	public float BaseHP
	{
		get{
			return baseHP;
		}
	}

	public float HP
	{
		get{
			return hp;
		}
	}

	public float AssaultSkill
	{
		get{
			return assaultSkill;
		}
	}

	public override bool IsVisible {
		get {
			return _isVisible || Player.HumanPlayer.isFriend(owner);
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

	public List<Perk> PerkList {
		get {
			return perkList;
		}
	}
		
	protected void Awake()
	{
		base.Awake ();
		if (!owner.IsHuman) {
			gameObject.AddComponent<LocalAI> ();
		}
		else
			gameObject.AddComponent<PlayerLocalAI> ();
		hp = baseHP;
		healthBarRectTransform = healthBar.GetComponent<RectTransform> ();
		meshRenderer = GetComponent<MeshRenderer> ();

		SufferDamageMultiplier = 1.0f;
		MakeDamageMultiplier = 1.0f;
		AttackSpeedModifier = 1.0f;
		LifeSteal = 0;

		visibilityCounter = 0;
	}

	protected void Start()
	{
		base.Start ();

		if (!Player.HumanPlayer.isFriend(owner)) {
			GetComponent<MeshRenderer> ().enabled = false;
		}

		var navMesh = GetComponent<NavMeshAgent> ().speed = this.speed;

		rangeReloadTime = 1 / rangeAttackPerSecond;
		meleeReloadTime = 1 / meleeAttackPerSecond;

		initializePerks ();
	}

	protected void Update()
	{
		base.Update ();

		//reload
		if (reloadCounter < rangeReloadTime) {
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

	private void initializePerks()
	{
		var perksParent = new GameObject();
		perksParent.transform.parent = gameObject.transform;
		perksParent.name = "Perks";
		List<Perk> activeList = new List<Perk> ();
		foreach (var perk in perkList) {
			var newPerk = Instantiate (perk.gameObject, perksParent.transform).GetComponent<Perk>();
			activeList.Add (newPerk);
		}
		perkList = activeList;
	}

	private bool isReadyToFire()
	{
		bool result = (reloadCounter >= rangeReloadTime / AttackSpeedModifier);
		if (result)
			reloadCounter = 0;
		return result;
	}
	private bool isReadyToBeat()
	{
		bool result = (reloadCounter >= meleeReloadTime / AttackSpeedModifier);
		if (result)
			reloadCounter = 0;
		return result;
	}

	public void PerformRangeAttack(Unit enemyUnit)
	{
		if (isReadyToFire ()) {
			enemyUnit.SufferDamage ((int)(rangeAttack * MakeDamageMultiplier));
			spawnParticleEffect (enemyUnit);
			// sound
		}
	}

	private void spawnParticleEffect(Unit enemyUnit)
	{
		Quaternion rotation = Quaternion.LookRotation (enemyUnit.transform.position);// particle's rotation
		ParticleSystem trace = Instantiate (traceParticle.gameObject, transform.position, Quaternion.identity, transform ).GetComponent<ParticleSystem>();


		float length = (enemyUnit.transform.position - transform.position).magnitude; // length of the particle
		trace.startLifetime = length / trace.main.startSpeed.constant;
		trace.transform.localRotation = Quaternion.identity;

	}
	public void PerformMeleeAttack(Unit enemyUnit)
	{
		if (isReadyToBeat ()) {
			enemyUnit.SufferDamage ((int)(meleeAttack * MakeDamageMultiplier));
			Heal ((int)Mathf.Floor (LifeSteal * meleeAttack));
		}
	}

	public void SufferDamage(int damage)
	{
		hp -= (int)(damage * SufferDamageMultiplier);
		updateUI ();
		if (hp <= 0)
			die ();
	}

	public void Heal(int healAmount)
	{
		hp += healAmount;
		hp = Mathf.Clamp (hp, 0, baseHP);
		updateUI ();
	}
	public bool IsHealthy()
	{
		if (hp > baseHP)
			Debug.LogError ("hp more than base hp");
		return hp == baseHP;
	}

	public bool isAttacking()
	{
		return actionQueue.Count > 0 && actionQueue.Peek () is AttackInteraction;
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
			Manager.Instance.selectionHandler.ObjectsInsideFrustum.Remove (this);
		} else {
			Manager.Instance.fieldOfViewHandler.Remove (this);
		}

		Manager.Instance.selectionHandler.UnselectObject (this);
		Manager.Instance.fogOfWarHanlder.UpdateFogQuery ();

		Destroy (gameObject);
	}
		
	private void updateUI()
	{
		healthBarRectTransform.localScale = new Vector3 ((float)hp / (float)baseHP, 1, 1);
	}

	public bool isEnemy(Unit otherUnit)
	{
		return owner.isEnemy (otherUnit.owner);
	}

	public bool isFriend(Unit otherUnit)
	{
		return owner.isFriend (otherUnit.owner);
	}

	public void changeOwner(Player newOwner)
	{
		owner = newOwner;
		GetComponent<MeshRenderer> ().material.color = owner.Color;
	}

}
