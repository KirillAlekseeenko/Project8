using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechButton : MonoBehaviour, IPointerClickHandler {

	[SerializeField] private TechnologyIcon techIcon;

	[SerializeField] private AudioClip rightClick;
	[SerializeField] private AudioClip leftClick;

	public Technology technology;
	public Color color;

	void Start(){
		color = GetComponent<Image> ().color;
		if (!technology.unblocked) {
			gameObject.GetComponent<Image> ().color = new Color (0.31f, 0.31f, 0.31f, 0.7f);
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Right) {
			techIcon.showTechnologyInfo (this);
			SoundMain.instance.Play (rightClick);
		} else if (eventData.button == PointerEventData.InputButton.Left) {
			techIcon.buyTechnology (this);
			SoundMain.instance.Play (leftClick);
		}
	}

}
