using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaveAdvices : MonoBehaviour {

	[SerializeField] private List<string> advices;
	[SerializeField] private Unit mave;
	[SerializeField] private Text textPanel;

	public void PutAdvice(int adviceNum){
		if(!mave.isAttacking())
			textPanel.text = advices [adviceNum];
	}

	public void HideText(){
		textPanel.text = "";	
	}
}
