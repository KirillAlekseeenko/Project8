using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBoxDrawComponent : MonoBehaviour {

	Vector3 clickPosition;
	Vector3 mousePosition;

	bool isDrawing = false;

	public void DrawRectangle(Vector3 clickPosition, Vector3 mousePosition)
	{
		this.clickPosition = clickPosition;
		this.mousePosition = mousePosition;
		isDrawing = true;
	}

	public void StopDrawing()
	{
		isDrawing = false;	
	}

	void OnGUI()
	{
		if (isDrawing) {
			var rect = UIUtils.GetScreenRect (clickPosition, mousePosition);
			UIUtils.DrawScreenRect (rect, new Color (0.8f, 0.8f, 0.95f, 0.25f));
			UIUtils.DrawScreenRectBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
		}
	}

}
