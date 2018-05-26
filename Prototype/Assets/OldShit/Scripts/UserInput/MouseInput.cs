using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour {

    public delegate void MouseInputEvent();
    public static event MouseInputEvent TurnOffUpgradeMode;
    public static event MouseInputEvent TurnOffPerkMode;

	[SerializeField] private EventSystem eventSystem;
	[SerializeField] private SelectionHandler selectionHandler;
	[SerializeField] private SelectionBoxDrawComponent selectionBoxDrawer;
	[SerializeField] private ActionHandler actionHandler;

	[SerializeField] private float dragThreshold;

	private Vector3 clickPosition;
	private bool isLeftMouseButtonDown = false;
	private bool isDrag = false;

    // Update is called once per frame
    void Update()
    {
        selectionHandler.OnMouseHover(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            switch (InputModesHandler.CurrentMode)
            {
                case InputMode.Default:
                    {
                        if (!eventSystem.IsPointerOverGameObject())
                        {
                            isDrag = false;

                            clickPosition = Input.mousePosition;
                            isLeftMouseButtonDown = true;
                            selectionHandler.OnLeftButtonDown(clickPosition);
                        }
                        break;
                    }
                case InputMode.PerkMode:
                    {
                        selectionHandler.Perks.OnLeftButtonDown(Input.mousePosition);
                        break;
                    }
                case InputMode.UpgradeMode:
                    {
                        if (TurnOffUpgradeMode != null && !eventSystem.IsPointerOverGameObject())
                            TurnOffUpgradeMode();
                        break;
                    }
            }
        }

        if (Input.GetMouseButtonUp(0) && InputModesHandler.CurrentMode == InputMode.Default)
        {
            isLeftMouseButtonDown = false;
            isDrag = false;

            selectionHandler.OnLeftButtonUp(Input.mousePosition);
            selectionBoxDrawer.StopDrawing();
        }

        if (isLeftMouseButtonDown
            && Vector3.Distance(Input.mousePosition, clickPosition) > dragThreshold
            && InputModesHandler.CurrentMode == InputMode.Default)
        {
            isDrag = true;

            selectionHandler.OnDrag(clickPosition, Input.mousePosition);
            selectionBoxDrawer.DrawRectangle(clickPosition, Input.mousePosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            switch (InputModesHandler.CurrentMode)
            {
                case InputMode.Default:
                    {
                        actionHandler.AssignAction(Input.mousePosition);
                        break;
                    }
                case InputMode.PerkMode:
                    {
                        selectionHandler.Perks.OnRightButtonDown(Input.mousePosition);
                        break;
                    }
            }
        }
    }

}
