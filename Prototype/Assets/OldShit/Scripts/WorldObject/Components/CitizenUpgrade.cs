using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenUpgrade : MonoBehaviour {

    [SerializeField] private Unit militiaPrefab;
    [SerializeField] private Unit scientistPrefab;
    [SerializeField] private Unit hackerPrefab;

    public void Upgrade(UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            case UpgradeType.Scientist:
                {
                    replaceUnit(scientistPrefab);
                    break;
                }
            case UpgradeType.Hacker:
                {
                    replaceUnit(hackerPrefab);
                    break;
                }
            case UpgradeType.Militia:
                {
                    replaceUnit(militiaPrefab);
                    break;
                }
        }
    }

    private void replaceUnit(Unit newUnit)
    {
        newUnit.Owner = GetComponent<Unit>().Owner;
        var upgraded = Instantiate(newUnit, transform.position, transform.rotation);
        Manager.Instance.selectionHandler.SelectObject(upgraded);
        Destroy(gameObject);
    }
	
}
