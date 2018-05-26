using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiplomacyData {
	[SerializeField] private Row[] rows;

	public Row this[int index]
	{
		get { return rows [index]; }
	}
}

[System.Serializable]
public class Row
{
	[SerializeField] private Relation[] rowData;

	public Relation this[int index]
	{
		get { return rowData [index]; }
		set { rowData [index] = value; }
	}
}

