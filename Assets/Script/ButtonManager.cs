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
        Const.StageId = 1;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GoToNextStage()
    {
        Const.StageId++;
        SceneManager.LoadScene("PlayGame", LoadSceneMode.Single);
    }

    public void Retry()
    {
        SceneManager.LoadScene("PlayGame", LoadSceneMode.Single);
    }

}
