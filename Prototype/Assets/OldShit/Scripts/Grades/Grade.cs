using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grade : MonoBehaviour
{
    const float UpdateTime = 1.0f;

    [SerializeField] protected GradesSettings gradesSettings;
    [SerializeField] protected GradesViewController gradesViewController;

    protected float currentValue;
    protected float maxValue;

    private List<OngoingProcessType> ongoingProcesses;
    private WaitForSeconds updateInterval = new WaitForSeconds(UpdateTime);

	private void Awake()
	{
        ongoingProcesses = new List<OngoingProcessType>();
	}

	private void OnEnable()
    {
        SubscribeToEvents();
		UnsubscribeFromEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

	private void Start()
	{
        currentValue = 0;
        maxValue = gradesSettings.MaxValue;
        StartCoroutine(UpdateValue());
	}

	protected abstract void SubscribeToEvents();
    protected abstract void UnsubscribeFromEvents();
    protected abstract void UpdateViewController();
	protected abstract void HandleValue();

    protected void HandleInstantEvent(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
    }

    protected void AddOngoingProcess(OngoingProcessType processType)
    {
        ongoingProcesses.Add(processType);
    }

    protected void RemoveOngoingProcess(OngoingProcessType processType)
    {
        for (int i = 0; i < ongoingProcesses.Count;i++)
        {
            if(ongoingProcesses[i] == processType)
            {
                ongoingProcesses.RemoveAt(i);
                break;
            }
        }
    }

    private IEnumerator UpdateValue()
    {
		while (true) {
			if (ongoingProcesses.Count == 0) {	
				Decay ();
			} else {
				foreach (var process in ongoingProcesses) {
					HandleOngoingProcess (process);
				}
			}

			currentValue = Mathf.Clamp (currentValue, 0, maxValue);
			HandleValue ();
			UpdateViewController ();

			yield return updateInterval;
		}
    }

    private void Decay()
    {
        if(currentValue > 0 && currentValue < 50)
        {
            currentValue -= gradesSettings.DecayRate1;
        }
        else if(currentValue > 50 && currentValue < 80)
        {
            currentValue -= gradesSettings.DecayRate2;
        }
        else
        {
            currentValue -= gradesSettings.DecayRate3;
        }
    }

    private void HandleOngoingProcess(OngoingProcessType processType)
    {
        switch(processType)
        {
            case OngoingProcessType.Battle:
                {
                    currentValue += gradesSettings.Battle;
                    break;
                }
            case OngoingProcessType.BuildingCapture:
                {
                    currentValue += gradesSettings.BuildingCapture;
                    break;
                }
            case OngoingProcessType.BuildingCaptureWithHack:
                {
                    currentValue += gradesSettings.BuildingCaptureWithHacker;
                    break;
                }
            case OngoingProcessType.BuildingRetreive:
                {
                    currentValue += gradesSettings.RetreiveFromBuilding;
                    break;
                }
            case OngoingProcessType.UnderCamera:
                {
                    currentValue += gradesSettings.UnderCamera;
                    break;
                }
        }
    }

}

public enum OngoingProcessType { Battle, BuildingCapture, BuildingCaptureWithHack, BuildingRetreive, UnderCamera }
