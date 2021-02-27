using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGear : BaseGear
{
    //接続先の状態変数
    public GameObject connectObj_af;
    //旧接続先の保管
    private GameObject connectObj_bf;
    //押したキーを格納
    public KeyCode key;
    private Vector3 connectPos;
    private float connectRadius;
    //自分の状態変数
    private bool fixflg;//固定されているときtrue
    void Start()
    {
        connectPos = connectObj_af.transform.position;
        connectRadius = (connectPos - transform.position).magnitude;
        fixflg = false;
    }

    // Update is called once per frame
    public void MoveActive(float dtime)
    {
        //固定されている場合以下処理を行わない
        if (fixflg)
        {
            return;
        }

        if (Const.JustConnectState == 2)
        {        // 左に移動
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnTriggerEnter_After(KeyCode.LeftArrow);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                OnTriggerEnter_After(KeyCode.RightArrow);
            }

        }

        // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = KeyCode.LeftArrow;
            //回転を行う
            RotateGear(dtime, +1);
            //動く方向ベクトルを計算
            Vector3 vec = connectPos - transform.position;
            //半径取得
            vec = vec / connectRadius * dtime * Const.GearSpeed;
            transform.localPosition -= (new Vector3(-vec.y, vec.x, 0.0f));
            transform.localPosition = connectPos + (transform.localPosition - connectPos) / (transform.localPosition - connectPos).magnitude * connectRadius;
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = KeyCode.RightArrow;
            //回転を行う
            RotateGear(dtime, -1);
            //動く方向ベクトルを計算
            Vector3 vec = connectPos - transform.position;
            //半径取得
            vec = vec / connectRadius * dtime * Const.GearSpeed;
            transform.localPosition += (new Vector3(-vec.y, vec.x, 0.0f));
            transform.localPosition = connectPos + (transform.localPosition - connectPos) / (transform.localPosition - connectPos).magnitude * connectRadius;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //相手からのトリガーは除外
        if (this.gameObject.name != Const.ActiveObjectName)
        {
            return;
        }

        //接触したときにキーを離した動作をする。
        Const.JustConnectState = 1;

        ///自分のベースギア
        BaseGear my_baseGear = this.gameObject.GetComponent<BaseGear>();

        ///
        ///接触後ギア
        ///
        //ギアを保存
        connectObj_bf = connectObj_af;
        //接触したギアを上書き取得
        connectObj_af = other.gameObject;
        //接触ギアの座標を取得
        connectPos = connectObj_af.transform.position;
        //接触ギアとの半径を取得
        connectRadius = (connectPos - transform.position).magnitude;

        //結合リストに追加
        BaseGear afterBg = connectObj_af.GetComponent<BaseGear>();
        connectGear.Add(afterBg);
        //相手の結合リストに自分を追加
        afterBg.connectGear.Add(my_baseGear);

        //接触したギアがアクティブギアの場合固定
        if (connectObj_af.GetComponent<ActiveGear>() != null)
        {
            //アクティブギア取得
            ActiveGear nextgear = other.gameObject.GetComponent<ActiveGear>();
            nextgear.fixflg = true;
        }
    }

    private void OnTriggerEnter_After(KeyCode bfkeycd)
    {

        //元の動作をする。
        Const.JustConnectState = 0;

        ///自分のベースギア
        BaseGear my_baseGear = this.gameObject.GetComponent<BaseGear>();
        //接触していたギア
        BaseGear beforeBg;
        //逆戻り
        if (bfkeycd != key)
        {
            beforeBg = connectObj_af.GetComponent<BaseGear>();
            //取得したギア情報を削除
            beforeBg.connectGear.Remove(my_baseGear);
            //自分のリストから相手を削除
            connectGear.Remove(beforeBg);

            //接触したギアを上書き取得
            connectObj_af = connectObj_bf;
            //接触ギアの座標を取得
            connectPos = connectObj_af.transform.position;
            //接触ギアとの半径を取得
            connectRadius = (connectPos - transform.position).magnitude;

        }
        else
        {

            ///
            ///接触していたギア
            ///
            //前に結合していた相手のリストから自分を除外
            beforeBg = connectObj_bf.GetComponent<BaseGear>();
            beforeBg.connectGear.Remove(my_baseGear);
            //自分のリストから相手を削除
            connectGear.Remove(beforeBg);
        }

        bool fflg = false;
        //離れた相手がアクティブギアであり、connectGearが２つ以上でアクティブギアが場合を除いて解除
        if (beforeBg.gameObject.GetComponent<ActiveGear>() != null)
        {
            if (beforeBg.connectGear.Count <= 1)
            {
                beforeBg.gameObject.GetComponent<ActiveGear>().fixflg = false;
                return;
            }

            foreach (BaseGear bg in beforeBg.connectGear)
            {
                if (bg.gameObject.GetComponent<ActiveGear>() != null)
                {
                    fflg = true;
                }
            }

            if (!fflg)
            {
                beforeBg.gameObject.GetComponent<ActiveGear>().fixflg = false;
            }
        }

    }
}
