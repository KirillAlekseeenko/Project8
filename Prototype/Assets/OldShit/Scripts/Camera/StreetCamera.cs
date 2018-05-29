using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreetCamera : ControllableItem {

	public static event System.Action AddGradePenaltyEvent;
	public static event System.Action RemoveGradePenaltyEvent;

	[SerializeField] private float viewRadius;
	[SerializeField] [Range(0, 180)] private float angle;

	private Transform cameraBeam;

	[SerializeField] private List<string> visibleUnits = new List<string>(); 

	private Timer checkSectorTimer = new Timer(1.0f);
	private VisionArcComponent visionArcComponent;
	private bool playerUnitsUnderCamera = false;

	public override void ResetOwner()
	{
		base.ResetOwner();
		visionArcComponent.gameObject.SetActive(false);
	}

	public override void SetOwner(Player player)
	{
		base.SetOwner(player);
		if (player == Player.HumanPlayer)
			visionArcComponent.gameObject.SetActive(true);
	}

	private void Start()
	{
		visionArcComponent = GetComponent<VisionArcComponent>();
		cameraBeam = transform.Find("CameraBeam");
	}

	private void Update()
	{
		if(!Captured)
		{
			checkSectorTimer.UpdateTimer(Time.deltaTime);
			if(checkSectorTimer.IsSet)
			{
				CheckSector();
				checkSectorTimer.Reset();
			}
		}
	}
    
    private void CheckSector()
	{
		var groundPosition = cameraBeam.position;
        
		var playerUnits = Physics.OverlapSphere(groundPosition, viewRadius, LayerMask.GetMask("Unit"))
								 .Select(collider => collider.GetComponent<Unit>())
								 .Where(unit => unit != null)
		                         .Where(unit => unit.Owner == Player.HumanPlayer);
		
		if(playerUnits.Count() > 0)
		{
			if(!playerUnitsUnderCamera)
			{
				playerUnitsUnderCamera = true;
				if (AddGradePenaltyEvent != null) AddGradePenaltyEvent();
			}
		}
		else
		{
			if(playerUnitsUnderCamera)
			{
				playerUnitsUnderCamera = false;
				if (RemoveGradePenaltyEvent != null) RemoveGradePenaltyEvent();
			}
		}
	}

	private bool IsObjectInsideTheArc(Vector3 groundPosition, Unit enemy)
    {
		var vectorToEnemy = enemy.transform.position - groundPosition;

        var direction = transform.TransformDirection(Vector3.forward);
		return Vector3.Angle(vectorToEnemy, direction) < RTS.Constants.VisionArcAngle / 2;
    }
}
