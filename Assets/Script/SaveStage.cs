﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// デバッグ用、画面上のオブジェクトからテストの作成をjson形式で出力を行う。
/// </summary>
public class SaveStage : MonoBehaviour
{
    /// <summary>
    /// モニターのデータを取得し保存する
    /// </summary>
    public void SaveMonitor(int stageNo)
    {
        StageData stagedata = new StageData();
        stagedata.StageNo = stageNo;///stageNo;
        stagedata.StageName = "StageName";//stageName;

        //画面上の全ギアオブジェクトを取得
        BaseGear[] saveobj = GameObject.FindObjectsOfType<BaseGear>();

        //格納用リスト
        int cou = 0;
        GearObject[] list = new GearObject[saveobj.Length];
        foreach (BaseGear bobj in saveobj)
        {
            GameObject obj = bobj.gameObject;
            GearObject go = new GearObject();
            go.Position = obj.transform.position;
            go.Rotation = obj.transform.transform.localEulerAngles;
            go.Scale = obj.transform.localScale;
            go.MyName = obj.name;
            if (obj.GetComponent<StartGear>() != null)
            {
                go.StartCondition = obj.GetComponent<StartGear>().startRotate;
            }
            else
            {
                go.StartCondition = 0;
            }
            if (obj.GetComponent<GoalGear>() != null)
            {
                go.ClearCondition = obj.GetComponent<GoalGear>().clearRotate;
            }
            else
            {
                go.ClearCondition = 0;
            }
            if (obj.GetComponent<ActiveGear>() != null)
            {
                go.ConnectGear = obj.GetComponent<ActiveGear>().connectObj_af.name;
            }
            else
            {
                go.ConnectGear = "";
            }
            //初期接続関係性を取得する。
            string[] li = new string[bobj.connectGear.Count];
            int count = 0;
            foreach (BaseGear bj in bobj.connectGear)
            {
                li[count] = bj.name;
                count++;
            }
            go.DefaultConnectGearName = li;
            list[cou] = go;
            cou++;
        }
        stagedata.gearObjects = list;


        SaveStageDataToJson(stagedata);
    }
    public static void SaveStageDataToJson(StageData stageData)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(stageData);

        writer = new StreamWriter(Application.dataPath + "/Data/" + stageData.StageNo + ".json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}