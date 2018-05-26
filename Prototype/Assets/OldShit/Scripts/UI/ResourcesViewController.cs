using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesViewController : MonoBehaviour {

    [SerializeField] private Text moneyText;
    [SerializeField] private Text sciencePointsText;

    public void SetMoney(int value)
    {
        moneyText.text = string.Format("{0} $", value);
    }

    public void SetSciencePoints(int value)
    {
        sciencePointsText.text = string.Format("{0} SP", value);
    }
}
