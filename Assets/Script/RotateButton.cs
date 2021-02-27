using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateButton : MonoBehaviour
{
    //スタートギア取得
    StartGear startGear;
    //ゴールギア取得
    GoalGear[] goalGear;

    //回転順番ギアリスト
    List<GearOrder> connectGear;

    //タップフラグ
    bool updateflg;

    //自分を取得
    Button mybutton;
    Text mybuttontext;

    // Start is called before the first frame update
    void Start()
    {
        mybutton = this.gameObject.GetComponent<Button>();
        mybuttontext = mybutton.GetComponentInChildren<Text>();

        startGear = GameObject.FindObjectOfType<StartGear>();
        goalGear = GameObject.FindObjectsOfType<GoalGear>();
        updateflg = false;
    }
    public void RotateClicked()
    {

        if (updateflg)
        {
            updateflg = false;
            mybuttontext.text = "回転してみる";
            return;
        }
        else
        {
            mybuttontext.text = "停止";
            updateflg = true;
        }

        //システム内のGoalGearを取得しdirrotをリセット
        GoalGear[] ggr = GameObject.FindObjectsOfType<GoalGear>();
        foreach (GoalGear gr in ggr)
        {
            gr.dirRot = 0;
        }

        //スタートギアから順番に入れていくリスト
        //順番を格納するリスト
        connectGear = new List<GearOrder>();
        //ループ中格納用リスト
        List<BaseGear> allGear = new List<BaseGear>();
        List<BaseGear> newGr = new List<BaseGear>();

        int count = 1;

        //一番目はスタートギアを格納
        GearOrder setGear = new GearOrder();
        allGear.Add(startGear);
        newGr.Add(startGear);
        setGear.orderGear = newGr;
        setGear.orderNum = count;
        connectGear.Add(setGear);
        count++;

        //二番目以降
        bool chk;
        do
        {
            newGr = new List<BaseGear>();
            foreach (BaseGear gr in connectGear[count - 2].orderGear)
            {
                foreach (BaseGear gr2 in gr.connectGear)
                {
                    chk = true;
                    foreach (BaseGear chkGear in allGear)
                    {
                        if (ReferenceEquals(chkGear, gr2))
                        {
                            chk = false;
                        }
                    }
                    if (chk)
                    {
                        allGear.Add(gr2);
                        newGr.Add(gr2);
                        //BaseGearの回転方向変数にも値を登録
                        gr2.dirRot = count;
                    }
                }
            }
            if (newGr.Count != 0)
            {
                setGear = new GearOrder();
                setGear.orderGear = newGr;
                setGear.orderNum = count;
                connectGear.Add(setGear);
                count++;
            }
        } while (newGr.Count != 0);


        updateflg = true;

        //クリア判定
        bool clearFlg = true;
        foreach (GoalGear gg in goalGear)
        {
            if (gg.clearRotate != CalcRotate(gg.dirRot))
            {
                clearFlg = false;
            }
        }
        if (clearFlg)
        {
            Debug.Log("ゲームクリア");
        }

    }

    private void Update()
    {
        if (updateflg)
        {
            foreach (GearOrder gro in connectGear)
            {
                foreach (BaseGear gr in gro.orderGear)
                {
                    gr.RotateGear(Time.deltaTime, CalcRotate(gro.orderNum));
                }
            }
        }
    }
    /// <summary>
    /// 順番から回転方向を計算する。
    /// </summary>
    private int CalcRotate(int num)
    {
        return (((num + startGear.startRotate) % 2) * 2) - 1;
    }
}
