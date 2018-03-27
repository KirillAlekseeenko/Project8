using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerkInfo
{
    [SerializeField] int perkCount = 1;
    [SerializeField] string name;
    [SerializeField] PerkType type;

    public PerkInfo(string name, Perk perk)
    {
        this.name = name;
        type = perk.Type;
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public PerkType Type
    {
        get
        {
            return type;
        }
    }

    public int PerkCount
    {
        get
        {
            return perkCount;
        }
        set
        {
            perkCount = value;
        }
    }
}
