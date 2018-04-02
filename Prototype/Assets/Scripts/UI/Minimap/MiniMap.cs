

using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Transform target;
	public float zoom = 10;
	public GameObject mainCamera;
	public enum Mode {topLeft = 0, topRight = 1, bottomLeft = 2, bottomRight = 3};
	public Mode minimapMode = Mode.bottomLeft;

	private Vector2 XRot = Vector2.right;
	private Vector2 YRot = Vector2.up;
	private RectTransform myTransform;
	private bool isMouse;

	void Awake () 
	{
		myTransform = GetComponent<RectTransform>();
	}
	
	public Vector2 TransformPosition(Vector3 position)
	{
		Vector3 offset = position - target.position;
		Vector2 pos = offset.x * XRot;
		pos += offset.z * YRot;
		pos *= 4;
		return pos;
	}

	public Vector3 TransformRotation(Vector3 rotation)
	{
		return new Vector3(0, 0, target.eulerAngles.y - rotation.y);
	}

	public Vector2 MoveInside(Vector2 point)
	{
		Rect rect = GetComponent<RectTransform>().rect;
		point = Vector2.Max(point, rect.min);
		point = Vector2.Min(point, rect.max);
		return point;
	}

	void LateUpdate()
	{
		XRot = new Vector2(target.right.x, -target.right.z);
		YRot = new Vector2(-target.forward.x, target.forward.z);

		if(isMouse)
		{
			if(Input.GetMouseButtonDown(0))
			{
				SetWorldPosition(mainCamera);
			}	
		}
	}

	public void SetMouse(bool state)
	{
		isMouse = state;
	}

	void SetWorldPosition(GameObject obj)
	{
		Vector2 mouse = Input.mousePosition;
		float X = 0;
		float Y = 0;
		Vector3 curPos = Vector3.zero;

		X = myTransform.anchoredPosition.x - myTransform.sizeDelta.x / 2;
		Y = myTransform.anchoredPosition.y - myTransform.sizeDelta.y / 2;

		switch(minimapMode)
		{
		case Mode.topLeft:
			Y += Screen.height;
			break;
		case Mode.topRight:
			X += Screen.width;
			Y += Screen.height;
			break;
		case Mode.bottomRight:
			X += Screen.width;
			break;
		}

		curPos = new Vector3(mouse.x - X, mouse.y - Y, 0);

		X = myTransform.sizeDelta.x / 2;
		Y = myTransform.sizeDelta.y / 2;
		Vector3 pos = new Vector3((curPos.x - X) / zoom, 0, (curPos.y - Y) / zoom);
		pos = new Vector3(pos.x, mainCamera.transform.position.y, pos.z);
		obj.transform.position = pos;
	}
}
