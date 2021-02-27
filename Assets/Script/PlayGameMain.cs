using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameMain : MonoBehaviour
{
    //カメラ表示
    private Camera mcamera;
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物
    //今動かせるやつを格納
    private ActiveGear activeGear;
    //最初は動かさないようフラグ
    private bool activeflg;

    //使用アイテムをあらかじめプレハブに配置
    public GameObject PrefabGear_fix;
    public GameObject PrefabGear_active;
    public GameObject PrefabGear_start;
    public GameObject PrefabGear_goal;

    // Start is called before the first frame update
    void Awake()
    {
        //初期値設定
        activeflg = false;
        //カメラを読み込む
        mcamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //保存（後で消す）
        //SaveAndLoadManager.SaveStragePlayerPrefs();
        //ステージを読み込む
        StageData stageData = SaveAndLoadManager.LoadData("1");
        //ステージを配置する。
        SettingStage(stageData);
    }
    private void SettingStage(StageData stageData)
    {
        List<GameObject> objs = new List<GameObject>();
        foreach (GearObject grOjb in stageData.gearObjects)
        {
            //初期データを入れる
            string name = grOjb.MyName.Substring(0, 3);
            if (name == "fix")
            {
                GameObject obj = Instantiate(PrefabGear_fix, grOjb.Position, Quaternion.Euler(grOjb.Rotation));
                obj.transform.localScale = grOjb.Scale;
                obj.name = grOjb.MyName;
                objs.Add(obj);
            }
            else if (name == "gol")
            {
                GameObject obj = Instantiate(PrefabGear_goal, grOjb.Position, Quaternion.Euler(grOjb.Rotation));
                obj.transform.localScale = grOjb.Scale;
                obj.GetComponent<GoalGear>().clearRotate = grOjb.ClearCondition;
                obj.name = grOjb.MyName;
                objs.Add(obj);
            }
            else if (name == "sta")
            {
                GameObject obj = Instantiate(PrefabGear_start, grOjb.Position, Quaternion.Euler(grOjb.Rotation));
                obj.transform.localScale = grOjb.Scale;
                obj.GetComponent<StartGear>().startRotate = grOjb.StartCondition;
                obj.name = grOjb.MyName;
                objs.Add(obj);
            }
            else if (name == "act")
            {
                GameObject obj = Instantiate(PrefabGear_active, grOjb.Position, Quaternion.Euler(grOjb.Rotation));
                obj.transform.localScale = grOjb.Scale;
                obj.name = grOjb.MyName;
                objs.Add(obj);
            }
            else
            {
                Debug.LogError("エラー");
            }

        }
        //結合情報を入れる
        foreach (GameObject obj in objs)
        {
            foreach (GearObject grOjb in stageData.gearObjects)
            {
                if (grOjb.MyName == obj.name)
                {
                    foreach (string coName in grOjb.DefaultConnectGearName)
                    {
                        BaseGear getGear = GameObject.Find(coName).GetComponent<BaseGear>();
                        BaseGear myGear = obj.GetComponent<BaseGear>();
                        myGear.connectGear.Add(getGear);
                    }
                    //アクティブならconnectGearを代入
                    if (obj.GetComponent<ActiveGear>() != null) 
                    {
                        GameObject getGear = GameObject.Find(grOjb.ConnectGear);
                        ActiveGear myGear = obj.GetComponent<ActiveGear>();
                        myGear.connectObj_af = getGear;
                    }

                    break;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //タップしたオブジェクトを取得する。
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            Ray ray = mcamera.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入
            if (Physics.Raycast(ray, out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                //アクティブギア取得
                if (hit.collider.gameObject.GetComponent<ActiveGear>() != null)
                {
                    activeGear = hit.collider.gameObject.GetComponent<ActiveGear>();
                    Const.ActiveObjectName = hit.collider.gameObject.name;
                    activeflg = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Const.JustConnectState == 1)
            {
                Const.JustConnectState = 2;
            }
        }
        //更新毎に呼び出す
        if (activeflg && Const.JustConnectState != 1)
        {
            activeGear.MoveActive(Time.deltaTime);
        }

    }
}
