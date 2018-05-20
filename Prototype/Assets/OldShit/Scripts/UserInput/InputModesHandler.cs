using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModesHandler : MonoBehaviour {

    static InputModesHandler()
    {
        CurrentMode = InputMode.Default;
        UnitIcon.TurnOnUpgradeMode += () =>
        {
            if (CurrentMode == InputMode.Default)
                CurrentMode = InputMode.UpgradeMode;
        };
        PerkHandler.TurnOnPerkMode += () =>
        {
            if (CurrentMode == InputMode.Default)
                CurrentMode = InputMode.PerkMode;
        };
        PerkHandler.TurnOffPerkMode += TurnOffPerkMode;
        MouseInput.TurnOffPerkMode += TurnOffPerkMode;
        MouseInput.TurnOffUpgradeMode += TurnOffUpgradeMode;
        UpgradeIcon.TurnOffUpgradeMode += TurnOffUpgradeMode;
    }

    public static InputMode CurrentMode { get; private set; }

    private static void TurnOffPerkMode()
    {
        if (CurrentMode == InputMode.PerkMode)
            CurrentMode = InputMode.Default;
    }

    private static void TurnOffUpgradeMode()
    {
        if (CurrentMode == InputMode.UpgradeMode)
            CurrentMode = InputMode.Default;
    }
	
}

public enum InputMode { Default, PerkMode, UpgradeMode };
