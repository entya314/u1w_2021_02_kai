using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //画面遷移用メソッド
    public void ChangeScene(string sceneName)
    {
        //シーンに遷移追加
        SceneManager.LoadScene(sceneName);
    }
}
