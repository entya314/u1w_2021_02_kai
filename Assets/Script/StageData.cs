using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public string StageName;
    public int StageNo;
    public GearObject[] gearObjects;

}

[System.Serializable]
public class GearObject
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public string MyName;
    public string[] DefaultConnectGearName;
    public string ConnectGear;//ActiveOnly
    public int ClearCondition;//GoalOnly
    public int StartCondition;//StartOnly

}