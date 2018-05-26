using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PerkInfo
{
    [SerializeField] int perkCount = 1;
    [SerializeField] string name;
    [SerializeField] PerkType type;
    [SerializeField] Sprite image;

    public PerkInfo(string name, Perk perk)
    {
        this.name = name;
        this.image = perk.Image;
        this.type = perk.Type;
        this.Description = perk.Description;
    }

    public string Description { get; private set; }
    public string Name { get { return name; } }
    public PerkType Type { get { return type; } }
    public int PerkCount { get { return perkCount; } set { perkCount = value; } }
    public Sprite Image { get { return image; } }
}
