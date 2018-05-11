using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradePanel : MonoBehaviour {

	[SerializeField]
	private BuildingPanelManager bpManager;

	[SerializeField] private Image level1Image;
	[SerializeField] private Text level1Cost;

	[SerializeField] private Image level2Image;
	[SerializeField] private Text level2Cost;

	[SerializeField] private Image level3Image;
	[SerializeField] private Text level3Cost;

	void OnEnable(){
		level1Image.sprite = bpManager.currentBuilding.vTools.buildingLevels [0].image;
		level1Cost.text = bpManager.currentBuilding.vTools.buildingLevels [0].cost.ToString();

		level2Image.sprite = bpManager.currentBuilding.vTools.buildingLevels [1].image;
		level2Cost.text = bpManager.currentBuilding.vTools.buildingLevels [1].cost.ToString();

		level3Image.sprite = bpManager.currentBuilding.vTools.buildingLevels [2].image;
		level3Cost.text = bpManager.currentBuilding.vTools.buildingLevels [2].cost.ToString();
	}

	public void buyUpgrade(int upgradeLevel){
		bpManager.UpgradeCurrentBuilding (upgradeLevel);
	} 
}
