using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] 
	private string infoText;
	[SerializeField]
	private BuildingPanelManager buildPanelManager;

	private Button button;

	private void Start()
	{
		button = GetComponent<Button>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!button.interactable)
			return;
		buildPanelManager.infoPanel.text = infoText;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buildPanelManager.infoPanel.text = " ";
		buildPanelManager.infoPanel.transform.parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 15f);
		if (!button.interactable)
			return;
	}
}
