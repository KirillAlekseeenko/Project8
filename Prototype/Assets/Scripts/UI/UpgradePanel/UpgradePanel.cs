using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradePanel : MonoBehaviour {

    public delegate void UpgradeEvents();
    public static event UpgradeEvents UpdateMaxCount;

    [SerializeField] private UpgradeIcon upgradeIcon;

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
    }
	
    public void ShowUpgradeIcons(Unit unit, ICollection<Unit> units)
    {
        CleanPanel();
        gameObject.SetActive(true);
        foreach(var upgrade in unit.PossibleUpgrades)
        {
            var newUpgradeIcon = CreateUpgradeIcon(upgrade);
        }
        CurrentUnitSet = units;
    }

    public void HidePanel()
    {
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

    public void Upgrade(int count)
    {
        if (CurrentUnitSet == null)
            throw new UnityException("Trying to upgrade null unitSet");
        foreach(var unit in CurrentUnitSet)
        {
            count--;
            if (count <= 0)
                break;
            Debug.Log("Upgrade " + unit.name);
        }

        HidePanel();
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
        newIcon.name = upgradeInfo.Upgrade.name;
        newIcon.IconImage.sprite = upgradeInfo.Upgrade.Icon;
        newIcon.SetCost(upgradeInfo.Cost);
        newIcon.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        newIcon.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        return newIcon;
    }
}
