using System.IO;
using UnityEngine;

public static class SaveAndLoadManager
{
    /// <summary>
    /// 最初の最初にデータを保存する
    /// </summary>
    /// <param name="stageLevel"></param>
    public static void SaveStragePlayerPrefs()
    {
        PlayerPrefs.SetString("1", "{\"StageName\":\"StageName\",\"StageNo\":1,\"gearObjects\":[{\"Position\":{\"x\":-2.0829999446868898,\"y\":2.0360000133514406,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"gol1\",\"DefaultConnectGearName\":[],\"ConnectGear\":\"\",\"ClearCondition\":1,\"StartCondition\":0},{\"Position\":{\"x\":-1.7899999618530274,\"y\":0.3400000035762787,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"act1\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"fix1\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"sta1\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":-0.8700000047683716,\"y\":1.4900000095367432,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":20.0,\"y\":20.0,\"z\":20.0},\"MyName\":\"act3\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"fix1\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":-0.777999997138977,\"y\":0.7749999761581421,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":10.000003814697266},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"fix1\",\"DefaultConnectGearName\":[\"act1\",\"sta1\",\"act2\",\"act3\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":0.2199999988079071,\"y\":1.1799999475479127,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"act2\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"fix1\",\"ClearCondition\":0,\"StartCondition\":0}]}");
        PlayerPrefs.Save();
    }

    public static StageData LoadData(string stageId)
    {
        string stageData = PlayerPrefs.GetString(stageId);
        StageData load = JsonUtility.FromJson<StageData>(stageData);
        return load;
    }
}
