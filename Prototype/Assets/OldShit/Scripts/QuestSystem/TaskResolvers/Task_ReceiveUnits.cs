﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Task_ReceiveUnits : Task_ObjectsAndNumbers {

	SelectionHandler sHandler;

	void Awake(){
		sHandler = GameObject.FindObjectOfType<SelectionHandler>();
	}

	protected override int getNecessaryNumber (){
		int n = 0;
		foreach (Unit unit in sHandler.AllPlayerUnits) {
			if (unit.UnitClassID == checkedUnitID)
				n++;
		}
		return n;
	}

}
