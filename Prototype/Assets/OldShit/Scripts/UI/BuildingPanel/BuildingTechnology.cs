﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTechnology : MonoBehaviour {

	[SerializeField] private TechTree technologies;
	[SerializeField] private GameObject techIcon;

	[SerializeField] private GameObject techExplainPanel;
	[SerializeField] private Text currentTechName;
	[SerializeField] private Image currentTech;
	[SerializeField] private Image unitImage;
	[SerializeField] private GameObject connector;
	private List<GameObject> activeTechnologies;
	private BuildingPanelManager buildPanel;

	void Awake(){
		activeTechnologies = new List<GameObject> ();
		buildPanel = GameObject.FindObjectOfType<BuildingPanelManager> ();
		techIcon.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
		connector.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
	}

	private void clearTechList(){
		for (int i = 0; i < activeTechnologies.Count; i++) {
			buildPanel.clickedPanels.Remove (activeTechnologies[i]);
			Destroy (activeTechnologies [i]);
		}
		activeTechnologies.Clear ();
		connector.SetActive(false);
	}

	public void PanelOpened(bool enable){
		if (enable && activeTechnologies.Count == 0) {
			connector.SetActive(true);
			//Все технологи первого уровня развития
			foreach (Technology tech in technologies.Technologies.FindAll(x => x.nextTechnologyID != 0)) {
				var icon = Instantiate (techIcon, gameObject.transform);
				icon.SetActive (true);
				icon.GetComponent<TechnologyIcon> ().setTechnologies (tech, technologies.FindTech (tech.nextTechnologyID));
				activeTechnologies.Add (icon);
				buildPanel.clickedPanels.Add (icon);
			}
		} else {
			clearTechList ();
		}
	}

	public void showTechExplainPanel(bool enable, Technology tech){
		techExplainPanel.SetActive (enable);
		if (enable) {
			currentTechName.text = tech.technologyName;
			currentTech.sprite = tech.technologyImage;
			unitImage.sprite = tech.technologyUnitImage;
		}
	}

	public void buyTechnology(Technology tech){
		if (tech.unblocked && Player.HumanPlayer.ResourcesManager.IsEnoughSciencePoints (tech.cost)) {
			Player.HumanPlayer.ResourcesManager.SpendSciencePoints (tech.cost);
			technologies.PayForTech (tech.technologyID);
			clearTechList ();
			PanelOpened (true);
		}
		
	}
}
