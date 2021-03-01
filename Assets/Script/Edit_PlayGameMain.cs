using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edit_PlayGameMain : MonoBehaviour
{
    //カメラ表示
    private Camera mcamera;
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物
    //今動かせるやつを格納
    private ActiveGear activeGear;
    //最初は動かさないようフラグ
    private bool activeflg;

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
    }
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
