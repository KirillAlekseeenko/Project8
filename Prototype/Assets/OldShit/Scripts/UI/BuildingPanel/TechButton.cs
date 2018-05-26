using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechButton : MonoBehaviour, IPointerClickHandler {

	[SerializeField] private TechnologyIcon techIcon;

	public Technology technology;
	public Color color;

	void Start(){
		color = GetComponent<Image> ().color;
		if (!technology.unblocked) {
			gameObject.GetComponent<Image> ().color = new Color (0f, 0f, 0f, 0.5f);
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Right) {
			techIcon.showTechnologyInfo (this);
		} else if (eventData.button == PointerEventData.InputButton.Left) {
			techIcon.buyTechnology (this);
		}
	}

}
