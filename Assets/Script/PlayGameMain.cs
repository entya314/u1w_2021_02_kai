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
    public static bool activeflg;
    //クリア画面
    public Canvas clearCanvas;
    //説明画面
    public Canvas firstcanvas;
    //使用アイテムをあらかじめプレハブに配置
    public GameObject PrefabGear_fix;
    public GameObject PrefabGear_active;
    public GameObject PrefabGear_start;
    public GameObject PrefabGear_goal;
    public GameObject PrefabArrow_start;
    public GameObject PrefabArrow_goal;

    // Start is called before the first frame update
    void Awake()
    {
        //初期値設定
        activeflg = false;

        if (Const.StageId != 1)
        {
            firstcanvas.gameObject.SetActive(false);
        }
        //カメラを読み込む
        mcamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //ステージを読み込む
        StageData stageData = SaveAndLoadManager.LoadData(Const.StageId.ToString());
        //ステージを配置する。
        SettingStage(stageData);
        //clearCanvas
        clearCanvas.gameObject.SetActive(false);
    }
    private void SettingStage(StageData stageData)
    {
        //矢印情報を入れる
        foreach (ArrowObject arrow in stageData.arrowObjects)
        {
            //初期データを入れる
            string name = arrow.MyName.Substring(0, 3);
            if (name == "sta")
            {
                GameObject obj = Instantiate(PrefabArrow_start, arrow.Position, Quaternion.Euler(arrow.Rotation));
                obj.transform.localScale = arrow.Scale;
                obj.name = arrow.MyName;
            }
            if (name == "gol")
            {
                GameObject obj = Instantiate(PrefabArrow_goal, arrow.Position, Quaternion.Euler(arrow.Rotation));
                obj.transform.localScale = arrow.Scale;
                obj.name = arrow.MyName;
            }
        }

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
                    BaseGear myGear = obj.GetComponent<BaseGear>();
                    foreach (string coName in grOjb.DefaultConnectGearName)
                    {
                        BaseGear getGear = GameObject.Find(coName).GetComponent<BaseGear>();
                        myGear.connectGear.Add(getGear);
                    }
                    //アクティブならconnectGearを代入
                    if (obj.GetComponent<ActiveGear>() != null)
                    {
                        ActiveGear myGear2 = obj.GetComponent<ActiveGear>();
                        GameObject getGear = GameObject.Find(grOjb.ConnectGear_af);
                        myGear2.connectObj_af = getGear;

                        GameObject getGear2 = GameObject.Find(grOjb.ConnectGear_bf);
                        myGear2.connectObj_bf = getGear2;

                        myGear2.key = (KeyCode)grOjb.key;
                        myGear2.JustConnectState = grOjb.JustConnectState;
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
                    //前のは保存しておく
                    ActiveGear bf_activeGear = activeGear;
                    activeGear = hit.collider.gameObject.GetComponent<ActiveGear>();
                    if (!activeGear.fixflg)
                    {
                        if (bf_activeGear == null)
                        {
                            activeGear.GetComponent<Renderer>().material.color = new Color32(128, 255, 128, 1);
                        }
                        else if (!bf_activeGear.fixflg)
                        {
                            bf_activeGear.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 1);
                            activeGear.GetComponent<Renderer>().material.color = new Color32(128, 255, 128, 1);
                        }
                    }
                    Const.ActiveObjectName = hit.collider.gameObject.name;
                    activeflg = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (activeGear.JustConnectState == 1)
            {
                activeGear.JustConnectState = 2;
            }
        }
        //更新毎に呼び出す
        if (activeflg && activeGear.JustConnectState != 1)
        {
            activeGear.MoveActive(Time.deltaTime);
        }

    }
}
