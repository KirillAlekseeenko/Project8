using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Perk : MonoBehaviour {
	[SerializeField] private string perkName;
    [SerializeField] private Sprite perkImage;
    [SerializeField] private float reloadTime;

    protected Timer perkTimer;

	public string Name { get { return perkName; } }
    public Sprite Image { get { return perkImage; } }

	public abstract PerkType Type { get; }

	public bool IsReadyToFire { get { return perkTimer.IsSet; } }
    public float CurrentProgress { get { return perkTimer.CurrentProgress; } }

    #region MonoBehaviour

    void Start()
	{
        perkTimer = new Timer(reloadTime);
	}

	void Update()
	{
        perkTimer.UpdateTimer(Time.deltaTime);
	}

	#endregion

	public override bool Equals(object other)
    {
        if (other == null || !(other is Perk))
            return false;
        return (other as Perk).perkName == perkName;
    }

	public void Run (Unit performer, Vector3? place = null, Unit target = null)
	{
        var action = new PerkAction (perform, isReadyToPerform, finish, initialize, performer, place, target);
		if (Type == PerkType.Itself) {
			performer.DoAction (action);
		} else {
			performer.AssignAction (action);
		}
	}

	protected abstract void initialize (Unit performer, Vector3? place = null, Unit target = null);
	protected abstract void derivedPerform (Unit performer, Vector3? place = null, Unit target = null);
	protected abstract bool isReadyToPerform (Unit performer, Vector3? place = null, Unit target = null);
	protected abstract void finish (Unit performer, Vector3? place = null, Unit target = null);

    private void perform(Unit performer, Vector3? place = null, Unit target = null)
    {
        derivedPerform(performer, place, target);
        perkTimer.Reset();
    }

}

public enum PerkType{Itself, Ground, Target};
