using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeInfo {
    
    [SerializeField] private Unit upgrade;
    [SerializeField] private int cost;

    public Unit Upgrade { get { return upgrade; } }
    public int Cost { get { return cost; } }
}
