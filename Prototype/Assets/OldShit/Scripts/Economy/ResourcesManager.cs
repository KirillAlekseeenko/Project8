using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourcesManager
{
    private readonly ResourcesViewController resourcesViewController;

    public int Money { get; private set; }
    public int SciencePoints { get; private set; }

    public ResourcesManager(int money, int sciencePoints, ResourcesViewController resourcesViewController)
    {
        this.Money = money;
        this.SciencePoints = sciencePoints;
        this.resourcesViewController = resourcesViewController;
        UpdateMoneyUI();
        UpdateSciencePointsUI();
    }

    public bool IsEnoughMoney(int cost)
    {
        return Money > cost;
    }

    public bool IsEnoughSciencePoints(int cost)
    {
        return SciencePoints > cost;
    }

    public void SpendMoney(int cost)
    {
        if (!IsEnoughMoney(cost))
            throw new UnityException("not enough money");
        Money -= cost;
        UpdateMoneyUI();
    }

    public void SpendSciencePoints(int cost)
    {
        if (!IsEnoughMoney(cost))
            throw new UnityException("not enough sciencePoints");
        SciencePoints -= cost;
        UpdateSciencePointsUI();
    }

    public void AddMoney(int value)
    {
        Money += value;
        UpdateMoneyUI();
    }

    public void AddSciencePoints(int value)
    {
        SciencePoints += value;
        UpdateSciencePointsUI();
    }

    private void UpdateMoneyUI()
    {
        if (resourcesViewController != null)
            resourcesViewController.SetMoney(Money);
    }

    private void UpdateSciencePointsUI()
    {
        if (resourcesViewController != null)
            resourcesViewController.SetSciencePoints(SciencePoints);
    }

}
