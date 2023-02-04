using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSceneUtil : SingletonMonoBehaviour<GameSceneUtil>
{
    //呼び出しもとのシーン名
    private string currentSceneName = string.Empty;

    /// <summary>
    /// シーンを呼び出す
    /// </summary>
    /// <param name="sceneName">呼び出されるシーン名</param>
    ///  <param name="action">呼び出されたシーン上で何かを行うためのCallback</param>
    public void SingleSceneTransration(string sceneName, UnityAction action = null)
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);

        StartCoroutine(LoadSceneAction(action));
    }

    IEnumerator LoadSceneAction(UnityAction action)
    {
        yield return new WaitUntil(()=>currentSceneName != SceneManager.GetActiveScene().name);
        if (action != null)
        {
            action.Invoke();
        }
    }

    public GameObject[] NextSceneRootGetGameObjects
    {
        get
        {
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            return rootGameObjects;
        }
    }
}
