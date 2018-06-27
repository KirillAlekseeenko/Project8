using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAI : MonoBehaviour {

    private const float TimeForIdleness = 1.0f;
    private const float LostSightTime = 5.0f;
    private const float DirectorNotificationInterval = 20.0f;

    private Unit unitComponent;
	private Director director;
	
	private float time = 0;

    private Timer lostSightTimer;
    private Timer directorNotificationTimer;

	void Awake()
	{
        lostSightTimer = new Timer(LostSightTime);
        directorNotificationTimer = new Timer(DirectorNotificationInterval);
		unitComponent = GetComponent<Unit> ();
	}

	void OnEnable()
	{
		if (!unitComponent.Owner.IsHuman) {
			director = unitComponent.Owner.GetComponent<Director> ();
			director.spawnedUnit (unitComponent);
		}
	}

	void OnDisable()
	{
		director.deadUnit (unitComponent);
	}

	void Update()
	{
		if (unitComponent.isAttacking ())
        {
            var enemyUnitPos = ((AttackInteraction)unitComponent.ActionQueue.Peek()).EnemyPosition;
            directorNotificationTimer.UpdateTimer(Time.deltaTime);

            if (Vector3.Distance(enemyUnitPos, transform.position) > unitComponent.pLOS)  // visibility is not taken into account
            {
                lostSightTimer.UpdateTimer(Time.deltaTime);
            }
            else
            {
                if (lostSightTimer.CurrentProgress > 0) lostSightTimer.Reset();
            }


            if(lostSightTimer.IsSet)
            {
                unitComponent.Stop();
                lostSightTimer.Reset();
            }
            if(directorNotificationTimer.IsSet)
            {
                director.Alarm(enemyUnitPos, transform);
                directorNotificationTimer.Reset();
            }
		} 
        else
        {
            if (lostSightTimer.CurrentProgress > 0) lostSightTimer.Reset();
            if (directorNotificationTimer.CurrentProgress > 0) directorNotificationTimer.Reset();

            CheckForEnemies();
            CheckIdleness();
        }
	}

    private void CheckForEnemies()
	{
		
		var colliders = Physics.OverlapSphere (transform.position, unitComponent.pLOS, LayerMask.GetMask("Unit"));

		foreach (var collider in colliders) {
			var unit = collider.gameObject.GetComponent<Unit> ();
			if (unit == null)
				continue;
			if (unitComponent.Owner != unit.Owner) { // DANGER если юнит не принадлежит к той же фракции
				var vectorToEnemy = unit.transform.position - transform.position;
				if (!Physics.Raycast (new Ray (transform.position, vectorToEnemy), vectorToEnemy.magnitude, LayerMask.GetMask("Building")) // проверка на отсутствие препятствий
					&& (vectorToEnemy.magnitude < unitComponent.pLOS * RTS.Constants.HearRadiusCoefficient || IsObjectInsideTheArc(unitComponent, unit) || unit.isAttacking())) { // + еще условия связанные с конусом, кругом слышимости и инвизом у юнита
    

					if (Player.HumanPlayer.isFriend (unitComponent.Owner) && !Player.HumanPlayer.isFriend(unit.Owner)) {
						unit.SetVisible (); // если это союзник игрока и он видит не союзника игрока
					}

					if(unitComponent.isEnemy(unit))
					{
						if (unit.Owner == Player.HumanPlayer)
							unit.SetVisible();
						unitComponent.AssignAction (new AttackInteraction (unitComponent, unit));
						director.Alarm (unit.transform.position, unitComponent.transform); // зовет всех на помощь (радиус у всех одинаковый и является свойством экземпляра Director)
					}				
				}
			}
		}
	}

    private void CheckIdleness()
	{
		if (unitComponent.isIdle ()) {
			time += Time.deltaTime;
			if (time >= TimeForIdleness) {
				director.becameIdle (unitComponent);
				time = 0;
			}
		} else {
			if (time > 0)
				time = 0;
		}
	}

    private bool IsObjectInsideTheArc(Unit unit, Unit enemy)
	{
		var vectorToEnemy = enemy.transform.position - unit.transform.position;
		if (unit.isEnemy(enemy) && unit.HalfVisible && vectorToEnemy.magnitude > unit.pLOS / 2)
			return false;
		
		var direction = unit.transform.TransformDirection (Vector3.forward);
		return Vector3.Angle (vectorToEnemy, direction) < RTS.Constants.VisionArcAngle / 2;

	}

}
