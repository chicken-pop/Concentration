using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSceneUtil : SingletonMonoBehaviour<GameSceneUtil>
{
    //�Ăяo�����Ƃ̃V�[����
    private string currentSceneName = string.Empty;

    /// <summary>
    /// �V�[�����Ăяo��
    /// </summary>
    /// <param name="sceneName">�Ăяo�����V�[����</param>
    ///  <param name="action">�Ăяo���ꂽ�V�[����ŉ������s�����߂�Callback</param>
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
