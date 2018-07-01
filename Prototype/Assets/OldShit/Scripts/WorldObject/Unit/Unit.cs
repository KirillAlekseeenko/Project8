using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : WorldObject {

	public static event System.Action AddGradePenaltyEvent_Fighting;
	public static event System.Action RemoveGradePenaltyEvent_Fighting;

	//////////////////////////////////////////////////////////////
	public enum PeopleWeapon{
		NONE,
		PISTOL,
		FISTS,
		RIFLE,
		SNIPER_RIFLE,
		TWO_PISTOLS
	};

	public enum Sex{
		MALE,
		FEMALE
	};
		
	public AudioClip StepSound;
	public AudioClip DeathSound;
	public AudioClip ShootSound;
	public AudioClip AbilitySound;

	public AudioSource audioSource;

	protected bool stopped;
	protected Unit currentEnemyUnit;
		
	//Placeholder functions for Animation events
	public void Hit(){
	}
	public void Shoot(){
		audioSource.PlayOneShot (ShootSound);
	}
	public void FootR(){
		audioSource.PlayOneShot (StepSound);	
	}
	public void FootL(){
		audioSource.PlayOneShot (StepSound);
	}
	public void Land(){}
	public void WeaponSwitch(){}
	///////////////////////////////////////////////////////////////
	
	public delegate void AddMinimapMark(GameObject unit);
	public static event AddMinimapMark OnStart;
	public delegate void UnitEvent(Unit unit);

	public static event UnitEvent Disable;
	public static event UnitEvent EnteredBuilding;
	public static event UnitEvent LeftBuilding;

    [SerializeField] private int unitClassID;

	[Header("Stats:")]

	/////////////////////////////////////////////////////////////////
	public Sex sex;
	public PeopleWeapon weapon;
	public Animator animator;
	/////////////////////////////////////////////////////////////////

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



	// buffs or debuffs
	public float SufferDamageMultiplier { get; set; }
	public float MakeDamageMultiplier { get; set; }
	public float LifeSteal { get; set; }
	public float AttackSpeedModifier{ get; set; }

    [Header("Upgrade")]
    [SerializeField] private List<UpgradeInfo> possibleUpgrades;
    [SerializeField] private List<UpgradeInfo> possibleRetrainning;

	[Header("UI")]
	[SerializeField] protected Canvas canvas;
	[SerializeField] protected Image healthBar;
	protected RectTransform healthBarRectTransform;

	[Header("Perks")]
	[SerializeField] protected List<Perk> perkList;
	[SerializeField] protected ParticleSystem traceParticle;
	/////////////////////////////////////////////////////////////
	[SerializeField] protected GameObject Weapon;


	// reloading
	private float rangeReloadTime;
	private float meleeReloadTime;
	private float reloadCounter;

	[SerializeField] private bool _isVisible = false;
	protected MeshRenderer meshRenderer;
	private float visibilityCounter;
	private float visibilityUpdateTime = 1.0f;

    private bool firstEnableCalled = false;
	public float Speed{ get { return speed; } set { speed = value; gameObject.GetComponent<NavMeshAgent> ().speed = value;}}
    public int UnitClassID {get { return unitClassID; } }
	public float Sneak { get { return sneak; } }
	public bool HalfVisible { get { return halfVisible; } }
	public bool IsRange { get { return isRange; } }
	public int MeleeAttack { get { return meleeAttack; } }
	public int RangeAttack { get { return rangeAttack; } }
	public float RangeAttackPerSecond { get { return rangeAttackPerSecond; } }
	public float RangeAttackRadius { get { return rangeAttackRadius; } }
	public float MeleeAttackRadius { get { return meleeAttackRadius; } }
	public float BaseHP { get{ return baseHP; } }
	public float HP { get{ return hp; } }
	public float AssaultSkill { get{ return assaultSkill; } }

	public bool IsVisibleByEnemy { get; private set; }

	public override bool IsVisibleInGame {
		get {
			return _isVisible || Player.HumanPlayer.isFriend(owner);
		}
		protected set {
			if (value != _isVisible) {
				if (Player.HumanPlayer.isFriend(owner))
					return;
				if (value) {
					BecomeVisible ();
				} else {
					BecomeInvisible ();
				}
			}
			_isVisible = value;
		}
	}	

	public List<Perk> PerkList { get { return perkList; } }
    public List<UpgradeInfo> PossibleUpgrades { get { return possibleUpgrades; } }
    public List<UpgradeInfo> PossibleRetrainings { get { return possibleRetrainning; } }

	protected void OnDisable()
	{
		if (owner.IsHuman) {
			Manager.Instance.selectionHandler.ObjectsInsideFrustum.Remove (this);
            Manager.Instance.selectionHandler.AllPlayerUnits.Remove(this);
		} else {
			Manager.Instance.fieldOfViewHandler.Remove (this);
		}

		Manager.Instance.selectionHandler.UnselectObject (this);
		Manager.Instance.fogOfWarHanlder.UpdateFogQuery ();

		if (Disable != null)
			Disable (this);
	}

	protected void OnEnable()
	{
        if (firstEnableCalled && Owner.IsHuman)
            Manager.Instance.selectionHandler.AllPlayerUnits.Add(this);
        
        firstEnableCalled = true;
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

		//////////////////////////////////From Nikita animations
		if(animator != null)
			this.SetStartConditions();
		audioSource = gameObject.GetComponent<AudioSource> ();
			///////////////////////////////////////////////////
	}

	protected void Start()
	{
		base.Start ();
		if(OnStart!=null)
			OnStart(gameObject);
		if (!Player.HumanPlayer.isFriend(owner)) {
			GetComponent<MeshRenderer> ().enabled = false;
		}

        if (Owner.IsHuman)
            Manager.Instance.selectionHandler.AllPlayerUnits.Add(this);


		var navMesh = GetComponent<NavMeshAgent> ().speed = this.speed;

		rangeReloadTime = 1f / rangeAttackPerSecond;
		meleeReloadTime = 1f / meleeAttackPerSecond;

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
			IsVisibleByEnemy = false;
			IsVisibleInGame = false;
		}

        updateCanvasPosition();

        ////////////////////////////////////////////////////////////////////////////////
		if (!isIdle ()) {
			if (animator != null) {
				this.Run (GetComponent<NavMeshAgent> ().velocity, GetComponent<NavMeshAgent> ().speed);
			}
		} else {
			if (animator != null) {
				if (GetComponent<Recruiter> () != null && GetComponent<Recruiter> ().IsRecruiting) {
					this.Talk ();
				} else {
					this.Idle ();
				}
			}
		}
		//////////////////////////////////////////////////////////////////////////////////
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

    private void updateCanvasPosition()
    {
        var cameraLook = -Camera.main.transform.forward;
        canvas.transform.rotation = Quaternion.LookRotation(-cameraLook);
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
			currentEnemyUnit = enemyUnit;
			//enemyUnit.SufferDamage ((int)(rangeAttack * MakeDamageMultiplier));
			//spawnParticleEffect (enemyUnit);
			//////////From Nikita animations
			if (animator != null) {
					this.Attack ();
			}
			currentEnemyUnit.SufferDamage ((int)(rangeAttack * MakeDamageMultiplier));
			spawnParticleEffect (currentEnemyUnit);
			// sound
		}
	}

	private void spawnParticleEffect(Unit enemyUnit)
	{
		Quaternion rotation = Quaternion.LookRotation (enemyUnit.transform.position);// particle's rotation
		/////////////////////////////////////////////////////////////////
		//Поменял transform.position на координаты оружия
		ParticleSystem trace;
		if (Weapon != null) {
			trace = Instantiate (traceParticle.gameObject, Weapon.transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem> ();
		} else {
			trace = Instantiate (traceParticle.gameObject, transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem> ();
		}
		float length = (enemyUnit.transform.position - transform.position).magnitude; // length of the particle
		trace.startLifetime = length / trace.main.startSpeed.constant;
		trace.transform.localRotation = Quaternion.identity;

	}
	public void PerformMeleeAttack(Unit enemyUnit)
	{
		if (isReadyToBeat ()) {
			//////////From Nikita animations
			if (animator != null) {
				this.Attack ();
			}
			enemyUnit.SufferDamage ((int)(meleeAttack * MakeDamageMultiplier));
			Heal ((int)Mathf.Floor (LifeSteal * meleeAttack));
		}
	}

	public void SufferDamage(int damage)
	{
		hp -= (int)(damage * SufferDamageMultiplier);
		//////////From Nikita animations
		if (animator != null) {
			this.ReceivedDamage ();
			stopped = true;
		}
		updateUI ();
		if (hp <= 0) {
			if (gameObject.GetComponent<Mave> () == null) {
				Invoke ("die", 0.5f);
				audioSource.PlayOneShot (DeathSound);
				//die ();
				//////////From Nikita animations
				if (animator != null)
					this.Dead ();
			} else {
				hp = baseHP;
				gameObject.GetComponent<Mave> ().MoveToStart ();
			}
		}
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
		IsVisibleInGame = true;
		IsVisibleByEnemy = true;
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
		if (!Owner.IsHuman)
			GameObject.FindObjectOfType<RevealGrade> ().HandleInstantEvent (10);
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

	public override void ChangeOwner(Player newOwner)
	{
        base.ChangeOwner(newOwner);
		GetComponent<MeshRenderer> ().material.color = owner.Color; // что-то придумать по цвету

		Stop ();

		if (newOwner.IsHuman) {                                   // осторожно, говнокод
			Destroy (GetComponent<LocalAI> ());
			gameObject.AddComponent<PlayerLocalAI> ();
			Manager.Instance.selectionHandler.ObjectsInsideFrustum.Add (this);
            Manager.Instance.selectionHandler.AllPlayerUnits.Add(this);
            Manager.Instance.fieldOfViewHandler.Remove(this);
        } else {
			Destroy (GetComponent<PlayerLocalAI> ());
			gameObject.AddComponent<LocalAI> ();
			Manager.Instance.selectionHandler.ObjectsInsideFrustum.Remove (this);
            Manager.Instance.selectionHandler.AllPlayerUnits.Remove(this);
            Manager.Instance.fieldOfViewHandler.Add(this);
        }

        IsVisibleByEnemy = false;
	}

}
