using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType{ Militia, Scientist, Hacker }

public class CitizenUpgradeHandler {

    private delegate void DelayedUpgrade();

    private DelayedUpgrade delayedUpgrade;

    private readonly SelectionHandler selectionHandler;

    public CitizenUpgradeHandler(SelectionHandler selectionHandler)
    {
        this.selectionHandler = selectionHandler;
    }

    public void UpdateUnits(UpgradeType upgradeType)
    {
        delayedUpgrade = null;
        
        foreach(var unit in selectionHandler.SelectedUnits)
        {
            var citizen = unit.GetComponent<CitizenUpgrade>();
            if(citizen != null)
            {
                delayedUpgrade += () => citizen.Upgrade(upgradeType);
            }
        }

        if (delayedUpgrade != null)
            delayedUpgrade();
    }
}
