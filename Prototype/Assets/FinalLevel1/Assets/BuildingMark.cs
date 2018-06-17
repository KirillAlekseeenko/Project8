using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMark : CanvasMark {

	[SerializeField] private Sprite pointerMark;
	[SerializeField] private Sprite fightMark;

	[SerializeField] private Image markImage;

	void Start(){
		base.Start ();
		Building.AddGradePenaltyEvent_FightInside += () => ChangeMark(fightMark);
		Building.RemoveGradePenaltyEvent_FightInside += () => ChangeMark(pointerMark);
	}

	private void ChangeMark(Sprite mark){
		markImage.sprite = mark;
	}
}
