using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Player : MonoBehaviour {

	Diplomacy diplomacy;

	[SerializeField] private string username;  // задел под мультиплеер ))0)
	[SerializeField] private bool isHuman;
	[SerializeField] private bool citizen;
	[SerializeField] private Team team;
	[SerializeField] private Color color;

    [Header("Resources")]
    [SerializeField] private int initialMoney;
    [SerializeField] private int initialSciencePoints;
    [SerializeField] private ResourcesViewController resourcesViewController;

    private ResourcesManager resourcesManager;

    [SerializeField] private List<Unit> upgradesFromStart;

    private ICollection<int> availableUpgrades;

	private static Player humanPlayer;

	public static Player HumanPlayer { get { return humanPlayer; } }

	private void Awake()
	{
        availableUpgrades = new HashSet<int>(upgradesFromStart.Where(unit => unit != null).Select(unit => unit.UnitClassID));
		diplomacy = transform.parent.GetComponent<Diplomacy> ();
        if (isHuman)
        {
            if (humanPlayer != null)
                throw new UnityException("There are two human players");
            humanPlayer = this;
        }
	}

	private void Start()
	{
        resourcesManager = new ResourcesManager(initialMoney, initialSciencePoints, resourcesViewController);
	}

	public bool IsHuman { get { return isHuman; } }
	public bool Citizen { get { return citizen; } }
	public Color Color { get { return color; } }
    public ResourcesManager ResourcesManager { get { return resourcesManager; } }
    public ICollection<int> AvailableUpgrades { get { return availableUpgrades; } }


	public bool isEnemy(Player player)
	{
		return diplomacy.getRelation (team, player.team) == Relation.Enemy;
	}

	public bool isFriend(Player player)
	{
		return diplomacy.getRelation (team, player.team) == Relation.Friend;
	}

}

