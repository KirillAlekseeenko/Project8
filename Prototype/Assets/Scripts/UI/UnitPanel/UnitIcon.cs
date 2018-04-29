using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour, IPointerClickHandler
{
    public delegate void UpgradeMode();
    public static event UpgradeMode TurnOnUpgradeMode;

    [SerializeField] private Image iconImage;
    [SerializeField] private Text unitsCount;

    Unit currentUnitType;
    HashSet<Unit> unitSet;
    UpgradePanel upgradePanel;

    public Image IconImage { get { return iconImage; } }
    public int ClassID { get { return currentUnitType.UnitClassID; } }

	private void Awake()
	{
        unitSet = new HashSet<Unit>();
	}

	private void Start()
	{
        upgradePanel = GetComponentInParent<UPManager>().UpgradePanel;
	}

	public int Count { get { return unitSet.Count; } }

    public void AddUnit(Unit unit)
    {
        unitSet.Add(unit);
        if (currentUnitType == null)
            currentUnitType = unit;
        UpdateUnitsCountText();
    }

    public void RemoveUnit(Unit unit)
    {
        unitSet.Remove(unit);
        UpdateUnitsCountText();
        upgradePanel.OnRemoveUnit();
    }

	public void OnPointerClick(PointerEventData eventData)
    {
        upgradePanel.ShowUpgradeIcons(currentUnitType, unitSet);
        if (TurnOnUpgradeMode != null)
            TurnOnUpgradeMode();
    }

    private void UpdateUnitsCountText()
    {
        unitsCount.text = Count.ToString();
    }
}
