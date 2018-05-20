using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GradesSettings {
    
    [SerializeField] private float maxValue;
    [SerializeField] private float decayRate1;
    [SerializeField] private float decayRate2;
    [SerializeField] private float decayRate3;
    [SerializeField] private float citizenKilled;
    [SerializeField] private float policeKilled;
    [SerializeField] private float DALYKilled;
    [SerializeField] private float neutralUnitKilled;
    [SerializeField] private float battle;
    [SerializeField] private float buildingCapture;
    [SerializeField] private float buildingCaptureWithHacker;
    [SerializeField] private float retreiveFromBuilding;
    [SerializeField] private float policeAttention;
    [SerializeField] private float DALYAttention;
    [SerializeField] private float droneCaptured;
    [SerializeField] private float turretCaptured;
    [SerializeField] private float agitation;
    [SerializeField] private float armedUnit;
    [SerializeField] private float unarmedUnit;
    [SerializeField] private float buildingImproved;
    [SerializeField] private float buildingLost;
    [SerializeField] private float underCamera;

    public float MaxValue
    {
        get
        {
            return maxValue;
        }
    }

    public float DecayRate1
    {
        get
        {
            return decayRate1;
        }
    }

    public float DecayRate2
    {
        get
        {
            return decayRate2;
        }
    }

    public float DecayRate3
    {
        get
        {
            return decayRate3;
        }
    }

    public float CitizenKilled
    {
        get
        {
            return citizenKilled;
        }
    }

    public float PoliceKilled
    {
        get
        {
            return policeKilled;
        }
    }

    public float NeutralUnitKilled
    {
        get
        {
            return neutralUnitKilled;
        }
    }

    public float Battle
    {
        get
        {
            return battle;
        }
    }

    public float BuildingCapture
    {
        get
        {
            return buildingCapture;
        }
    }

    public float BuildingCaptureWithHacker
    {
        get
        {
            return buildingCaptureWithHacker;
        }
    }

    public float RetreiveFromBuilding
    {
        get
        {
            return retreiveFromBuilding;
        }
    }

    public float PoliceAttention
    {
        get
        {
            return policeAttention;
        }
    }

    public float DroneCaptured
    {
        get
        {
            return droneCaptured;
        }
    }

    public float TurretCaptured
    {
        get
        {
            return turretCaptured;
        }
    }

    public float Agitation
    {
        get
        {
            return agitation;
        }
    }

    public float ArmedUnit
    {
        get
        {
            return armedUnit;
        }
    }

    public float UnarmedUnit
    {
        get
        {
            return unarmedUnit;
        }
    }

    public float BuildingImproved
    {
        get
        {
            return buildingImproved;
        }
    }

    public float BuildingLost
    {
        get
        {
            return buildingLost;
        }
    }

    public float UnderCamera
    {
        get
        {
            return underCamera;
        }
    }
}
