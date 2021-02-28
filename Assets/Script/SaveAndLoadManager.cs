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
        PlayerPrefs.SetString("1", "{\"StageName\":\"StageName\",\"StageNo\":1,\"gearObjects\":[{\"Position\":{\"x\":0.6100000143051148,\"y\":2.0360000133514406,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"gol1\",\"DefaultConnectGearName\":[],\"ConnectGear\":\"\",\"ClearCondition\":1,\"StartCondition\":0},{\"Position\":{\"x\":-0.25999999046325686,\"y\":0.7749999761581421,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"fix1\",\"DefaultConnectGearName\":[\"act1\",\"sta1\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":-1.2599999904632569,\"y\":0.38999998569488528,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"act1\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"fix1\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":0.5,\"y\":0.0,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"sta1\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0}],\"arrowObjects\":[{\"Position\":{\"x\":1.159999966621399,\"y\":1.4700000286102296,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":180.0,\"z\":62.39925003051758},\"Scale\":{\"x\":8.0,\"y\":8.0,\"z\":8.0},\"MyName\":\"gol1_arrow\"},{\"Position\":{\"x\":-0.029999999329447748,\"y\":-0.49000000953674319,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":180.0,\"z\":0.0},\"Scale\":{\"x\":8.0,\"y\":8.0,\"z\":8.0},\"MyName\":\"sta1_arrow\"}]}");
        PlayerPrefs.SetString("2", "{\"StageName\":\"StageName\",\"StageNo\":1,\"gearObjects\":[{\"Position\":{\"x\":-1.0950000286102296,\"y\":0.8489999771118164,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":30.0,\"y\":30.0,\"z\":30.0},\"MyName\":\"act2\",\"DefaultConnectGearName\":[\"sta1\"],\"ConnectGear\":\"sta1\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":1.2899999618530274,\"y\":1.4140000343322755,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"gol1\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":0.3970000147819519,\"y\":0.8180000185966492,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"fix1\",\"DefaultConnectGearName\":[\"gol1\",\"act1\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":0.7559999823570252,\"y\":-0.08699999749660492,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":40.0,\"y\":40.0,\"z\":40.0},\"MyName\":\"act1\",\"DefaultConnectGearName\":[\"fix1\"],\"ConnectGear\":\"fix1\",\"ClearCondition\":0,\"StartCondition\":0},{\"Position\":{\"x\":-0.8799999952316284,\"y\":0.0,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"Scale\":{\"x\":50.0,\"y\":50.0,\"z\":50.0},\"MyName\":\"sta1\",\"DefaultConnectGearName\":[\"act2\"],\"ConnectGear\":\"\",\"ClearCondition\":0,\"StartCondition\":0}],\"arrowObjects\":[{\"Position\":{\"x\":0.5839999914169312,\"y\":1.621999979019165,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":180.0,\"z\":62.39925003051758},\"Scale\":{\"x\":8.0,\"y\":8.0,\"z\":8.0},\"MyName\":\"gol1_arrow\"},{\"Position\":{\"x\":-1.4299999475479127,\"y\":-0.49000000953674319,\"z\":0.0},\"Rotation\":{\"x\":0.0,\"y\":180.0,\"z\":0.0},\"Scale\":{\"x\":8.0,\"y\":8.0,\"z\":8.0},\"MyName\":\"sta1_arrow\"}]}");
        PlayerPrefs.Save();
    }

    public static StageData LoadData(string stageId)
    {
        string stageData = PlayerPrefs.GetString(stageId);
        StageData load = JsonUtility.FromJson<StageData>(stageData);
        return load;
    }
}
