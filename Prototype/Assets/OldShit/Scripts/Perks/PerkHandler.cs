using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerkHandler
{
    public delegate void PerkEvents();
    public static event PerkEvents TurnOnPerkMode;
    public static event PerkEvents TurnOffPerkMode;

    private SelectionHandler selectionHandler;
    private SkillsPanelManager skillsPanelManager;

    [SerializeField] private List<PerkInfo> unitPerks;
    [SerializeField] private List<Unit> activatedUnits;
    private PerkInfo currentPerk;

    public PerkInfo CurrentPerk
    {
        get
        {
            return currentPerk;
        }
    }

    public PerkHandler(SelectionHandler selectionHandler, SkillsPanelManager skillsPanelManager)
    {
        unitPerks = new List<PerkInfo>();
        activatedUnits = new List<Unit>();
        this.selectionHandler = selectionHandler;
        this.skillsPanelManager = skillsPanelManager;
    }

    public void AddPerks(Unit unit)
    {
        foreach (var perk in unit.PerkList)
        {
            var perkInfo = unitPerks.Find(x => x.Name.Equals(perk.Name));
            if (perkInfo != null)
            {
                perkInfo.PerkCount++;
            }
            else
            {
                unitPerks.Add(new PerkInfo(perk.Name, perk));
                skillsPanelManager.AddPerk(new PerkInfo(perk.Name, perk));
            }
        }
    }

    public void RemovePerks(Unit unit)
    {
        foreach (var perk in unit.PerkList)
        {
            var perkInfo = unitPerks.Find(x => x.Name.Equals(perk.Name));
            if (perkInfo != null)
            {
                perkInfo.PerkCount--;
                if (perkInfo.PerkCount <= 0)
                {
                    int index = unitPerks.IndexOf(perkInfo);
                    unitPerks.Remove(perkInfo);
                    skillsPanelManager.RemovePerk(perkInfo, index);
                }
            }
        }
        if (activatedUnits.Contains(unit))
        {
            activatedUnits.Remove(unit);
            if (activatedUnits.Count == 0)
                deactivate();
        }
    }

    public string GetPerkDescription(int index)
    {
        if (index >= unitPerks.Count)
        {
            throw new UnityException("Perk index is out of range");
        }
        return unitPerks[index].Description;
    }

    public void ActivatePerk(int index)
    {
        if (index >= unitPerks.Count)
        {
            Debug.Log(unitPerks);
            return;
        }
        deactivate();
        PerkInfo perk = unitPerks[index];
        currentPerk = perk;
        foreach (var worldObject in selectionHandler.SelectedUnits)
        {
            if (worldObject is Unit)
            {
                var unit = worldObject as Unit;
                var perkToActivate = unit.PerkList.Find(x => x.Name.Equals(perk.Name));
                if (perkToActivate != null)
                {
                    if (perk.Type == PerkType.Itself)
                    {
                        perkToActivate.Run(unit);
                    }
                    else
                    {
                        activatedUnits.Add(unit);
                    }
                }
            }
        }

        if (!(perk.Type == PerkType.Itself))
        {
            if (TurnOnPerkMode != null)
                TurnOnPerkMode();
        }
    }

    private void performPerk(Vector3? place = null, Unit target = null)
    {
        if (activatedUnits.Count > 0)
        {
            int index = 0;
            while (index < activatedUnits.Count && !activatedUnits[index].PerkList.Find(x => x.Name.Equals(currentPerk.Name)).IsReadyToFire)
            { // first reloaded
                index++;
            }
            if (index == activatedUnits.Count)
            {
                deactivate();
                return;
            }
            var unit = activatedUnits[index];
            var perkToActivate = unit.PerkList.Find(x => x.Name.Equals(currentPerk.Name));
            if (perkToActivate != null)
            {
                if (target == null && currentPerk.Type == PerkType.Target)
                    return;
                if (currentPerk.Type == PerkType.Ground)
                {
                    perkToActivate.Run(unit, place: place);
                }
                else if (currentPerk.Type == PerkType.Target)
                {
                    perkToActivate.Run(unit, target: target);
                }
            }
        }
        deactivate();
    }

    public void OnLeftButtonDown(Vector3 mousePosition)
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var unit = hit.collider.gameObject.GetComponent<Unit>();
            if (unit != null && unit.IsVisible && Player.HumanPlayer.isEnemy(unit.Owner))
            {
                performPerk(unit.transform.position, unit);
            }
            else
            {
                performPerk(hit.point);
            }
        }
    }

    public void OnRightButtonDown(Vector3 mousePosition)
    {
        deactivate();
    }

    private void deactivate()
    {
        activatedUnits.Clear();
        currentPerk = null;
        if (TurnOffPerkMode != null)
            TurnOffPerkMode();
    }
}
