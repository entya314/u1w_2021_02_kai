using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGear : MonoBehaviour
{

    //接続しているギアを格納する
    public List<BaseGear> connectGear = new List<BaseGear>();

    //今の回転方向を格納する。
    [HideInInspector]
    public int dirRot = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 回転を行う。(updateから呼び出される前提)
    /// </summary>
    /// <param name="dtime">デルタタイム</param>
    /// <param name="lr">反時計回りマイナス、時計回りプラス</param>
    public void RotateGear(float dtime, int lr)
    {
        this.gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, lr * Const.GearSpeed * 110.0f * dtime));
    }
}
