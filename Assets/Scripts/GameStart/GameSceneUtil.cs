using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUtil : SingletonMonoBehaviour<GameSceneUtil>
{
    public void SingleSceneTransration(string sceneName)
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }
}
