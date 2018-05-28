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

	[SerializeField] private GameObject buildingLevelInfo;
	[SerializeField] private Text gradeName;
	[SerializeField] private Image gradeImage;
	[SerializeField] private Text capacityInfo;
	[SerializeField] private Text levelTechnologyName;
	[SerializeField] private Image levelTechnologyImage;
	[SerializeField] private GameObject connector;
	void Start(){
		connector.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
        //connector.transform.position = new Vector2(connector.transform.position.x, transform.position.y);  
	}

	void OnEnable(){
		connector.SetActive(true);
		level1Image.sprite = bpManager.currentBuilding.vTools.buildingLevels [0].image;
		level1Cost.text = bpManager.currentBuilding.vTools.buildingLevels [0].cost.ToString();

		level2Image.sprite = bpManager.currentBuilding.vTools.buildingLevels [1].image;
		level2Cost.text = bpManager.currentBuilding.vTools.buildingLevels [1].cost.ToString();

		level3Image.sprite = bpManager.currentBuilding.vTools.buildingLevels [2].image;
		level3Cost.text = bpManager.currentBuilding.vTools.buildingLevels [2].cost.ToString();
	}
	void OnDisable(){
		connector.SetActive(false);
	}

	public void buyUpgrade(int upgradeLevel){
		bpManager.UpgradeCurrentBuilding (upgradeLevel);
	}

	public void ShowBuildingLevelInfo(bool enable, GameObject gradeButton){
		buildingLevelInfo.SetActive (enable);
		if (enable) {
			int grade = 1;
			gradeImage.sprite = level1Image.sprite;
			if (gradeButton == level2Image.gameObject) {
				grade = 2;
				gradeImage.sprite = level2Image.sprite;
			} else if (gradeButton == level3Image.gameObject) {
				gradeImage.sprite = level3Image.sprite;
				grade = 3;
			}
			gradeName.text = bpManager.currentBuilding.GradeName(grade).ToString();
			capacityInfo.text = bpManager.currentBuilding.NonWarriorsAmountPerLevel(grade).ToString();
			if (bpManager.currentBuilding.LevelTechnology (grade) != null) {
				levelTechnologyName.text = bpManager.currentBuilding.LevelTechnology (grade).technologyName;
				levelTechnologyImage.sprite = bpManager.currentBuilding.LevelTechnology (grade).technologyImage;
				levelTechnologyImage.color = new Color (
					levelTechnologyImage.color.r,
					levelTechnologyImage.color.g,
					levelTechnologyImage.color.b,
					1
				);
			} else {
				levelTechnologyName.text = "";
				levelTechnologyImage.sprite = null;
				levelTechnologyImage.color = new Color (
					levelTechnologyImage.color.r,
					levelTechnologyImage.color.g,
					levelTechnologyImage.color.b,
					0
				);
			}
		}
	}
}
