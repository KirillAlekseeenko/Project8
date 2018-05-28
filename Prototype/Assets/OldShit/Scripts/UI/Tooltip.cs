using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] 
	private string infoText;
	[SerializeField]
	private BuildingPanelManager buildPanelManager;

	void Awake(){
		if (buildPanelManager == null)
			buildPanelManager = GameObject.FindObjectOfType<BuildingPanelManager> ().GetComponent<BuildingPanelManager>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buildPanelManager.infoPanel.text = infoText;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buildPanelManager.infoPanel.text = " ";
		buildPanelManager.infoPanel.transform.parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 15f);
	}
}
