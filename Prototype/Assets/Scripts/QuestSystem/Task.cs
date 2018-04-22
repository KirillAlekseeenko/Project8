using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task{

	public int id;
	public string title;
	public string explanation;
	public AudioClip audioExplanation;
	public QuestProgress taskProgress;
}
