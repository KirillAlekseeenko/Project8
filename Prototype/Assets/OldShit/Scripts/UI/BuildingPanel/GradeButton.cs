using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GradeButton : MonoBehaviour, IPointerClickHandler {

	[SerializeField] private BuildingUpgradePanel upgradePanel;

	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Right) {
			upgradePanel.ShowBuildingLevelInfo (true, this.gameObject);
		}
	}
}
