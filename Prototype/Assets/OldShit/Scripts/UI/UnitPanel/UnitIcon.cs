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
    //private GameObject unitPanel;
    [SerializeField] private UPManager unitPanel;
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
        unitPanel = GetComponentInParent<UPManager>();
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
        if(eventData.button == PointerEventData.InputButton.Right){
            unitPanel.unitInfoPanel.SetActive(true);
            unitPanel.unitInfoImage.sprite = currentUnitType.Icon;
            unitPanel.HPOut.text = currentUnitType.HP.ToString();
            unitPanel.MAOut.text = currentUnitType.MeleeAttack.ToString();
            unitPanel.RAOut.text = currentUnitType.RangeAttack.ToString();
            unitPanel.RADOunt.text = currentUnitType.RangeAttackRadius.ToString();
            unitPanel.SPOut.text = currentUnitType.Speed.ToString();
        }
		if(eventData.button == PointerEventData.InputButton.Left && currentUnitType.Owner.IsHuman){
            upgradePanel.ShowUpgradeIcons(currentUnitType, unitSet);
            if (TurnOnUpgradeMode != null)
                TurnOnUpgradeMode();
        }
    }

    private void UpdateUnitsCountText()
    {
        unitsCount.text = Count.ToString();
    }
}
