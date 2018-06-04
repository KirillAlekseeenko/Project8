using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyIcon : MonoBehaviour {

	[SerializeField] private Text className;
	[SerializeField] private Text techLevel1Price;
	[SerializeField] private Text techLevel2Price;

	[SerializeField] private Image techLevel1Image;
	[SerializeField] private Image techLevel2Image;


	private BuildingTechnology techPanel;
	private Technology techFirst;
	private Technology techSecond;

	void Awake(){
		techPanel = GameObject.FindObjectOfType<BuildingTechnology> ();
	}

	public void setTechnologies(Technology techFirstLevel, Technology techSecondLevel){
		techFirst = techFirstLevel;
		techSecond = techSecondLevel;
		className.text = techFirstLevel.connectedUnitClassName;

		techLevel1Image.sprite = techFirstLevel.technologyImage;
		techLevel1Image.gameObject.GetComponent<TechButton> ().technology = techFirstLevel;
		if (!techFirstLevel.bought)
			techLevel1Price.text = techFirstLevel.cost.ToString ();
		else
			techLevel1Price.text = "Открыто";
		
		techLevel2Image.sprite = techSecondLevel.technologyImage;
		techLevel2Image.gameObject.GetComponent<TechButton> ().technology = techSecondLevel;
		if (!techSecondLevel.bought)
			techLevel2Price.text = techSecondLevel.cost.ToString ();
		else
			techLevel2Price.text = "Открыто";
	}

	public void showTechnologyInfo(TechButton techButton){
		techPanel.showTechExplainPanel (true, techButton.technology);
	}

	public void buyTechnology(TechButton techButton){
		techPanel.buyTechnology (techButton.technology);
	}
}
