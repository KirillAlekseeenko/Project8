using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class UpgradePanel : MonoBehaviour {

    public delegate void UpgradeEvents();
    public static event UpgradeEvents UpdateMaxCount;

    [SerializeField] private UpgradeIcon upgradeIcon;
    [SerializeField] private GameObject leftBracket;
    [SerializeField] private GameObject rightBracketPref;
    private GameObject rightBracket;
    public ICollection<Unit> CurrentUnitSet { get; private set; }

	private void OnEnable()
	{
        MouseInput.TurnOffUpgradeMode += HidePanel;
	}

	private void OnDisable()
	{
        MouseInput.TurnOffUpgradeMode -= HidePanel;
	}

	private void Start()
    {
        upgradeIcon.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
        leftBracket.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
        leftBracket.transform.position = new Vector2(leftBracket.transform.position.x, transform.position.y);
        rightBracketPref.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
        rightBracketPref.transform.position = transform.position;
        transform.position = leftBracket.transform.position + new Vector3(leftBracket.GetComponent<RectTransform>().rect.width+3,0,0);
    }
	
    public void ShowUpgradeIcons(Unit unit, ICollection<Unit> units)
    {
        CleanPanel();
        gameObject.SetActive(true);
        System.Func<UpgradeInfo, bool> filter = upgrade => unit.Owner.AvailableUpgrades.Contains(upgrade.Upgrade.UnitClassID);

        var filteredUpgrades = unit.PossibleUpgrades.Where(filter);
        var filteredRetrainings = unit.PossibleRetrainings.Where(filter);
        if(filteredUpgrades.Count()!=0)
            leftBracket.SetActive(true);
        foreach (var upgrade in filteredUpgrades.Concat(filteredRetrainings))
        {
            Destroy(rightBracket);
            var newUpgradeIcon = CreateUpgradeIcon(upgrade);
            rightBracket = Instantiate(rightBracketPref,gameObject.transform);
            rightBracket.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            rightBracket.GetComponent<RectTransform>().pivot = new Vector2(0, 0);   
        }
        CurrentUnitSet = units;
    }

    public void HidePanel()
    {
        leftBracket.SetActive(false);
        CleanPanel();
        gameObject.SetActive(false);
        CurrentUnitSet = null;
    }

    public void OnRemoveUnit()
    {
        if (!gameObject.activeSelf)
            return;
        if (UpdateMaxCount != null)
            UpdateMaxCount();
        if (CurrentUnitSet.Count == 0)
            HidePanel();
    }

    public void Upgrade(Unit prefab, int amount, int cost)
    {
        if (CurrentUnitSet == null)
            throw new UnityException("Trying to upgrade null unitSet");
        if(prefab == null)
            throw new UnityException("Trying to upgrade to null prefab");
        System.Action UpgradeAction = null;
        var count = amount;
        foreach(var unit in CurrentUnitSet)
        {
            count--;
            if (count < 0)
                break;

            UpgradeAction += () => UpgradeUnit(unit, prefab); // delayed call
        }

        if(UpgradeAction != null)
            UpgradeAction();

        Player.HumanPlayer.ResourcesManager.SpendMoney(amount * cost);

        HidePanel();
    }

    private void UpgradeUnit(Unit unit, Unit upgradePrefab)
    {
        upgradePrefab.Owner = unit.Owner;
        var pos = unit.transform.position;
        var rotation = unit.transform.rotation;
        var newUnit = Instantiate(upgradePrefab, pos, rotation);
        Manager.Instance.selectionHandler.SelectObject(newUnit);
        Destroy(unit.gameObject);
    }

    private void CleanPanel()
    {
        foreach (Transform icon in transform)
        {
            Destroy(icon.gameObject);
        }
    }

    private UpgradeIcon CreateUpgradeIcon(UpgradeInfo upgradeInfo)
    {
        var newIcon = Instantiate(upgradeIcon, transform);
        {
            newIcon.Unit = upgradeInfo.Upgrade;
            newIcon.name = upgradeInfo.Upgrade.name;
            newIcon.IconImage.sprite = upgradeInfo.Upgrade.Icon;
            newIcon.SetCost(upgradeInfo.Cost);
            newIcon.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            newIcon.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        }
        return newIcon;
    }
}
