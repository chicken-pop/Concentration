using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ConcentrationStartScene : MonoBehaviour
{
    [SerializeField]
    private Button OnePlayerStartButton;
     [SerializeField]
    private Button TwoPlayerStartButton;

    private void Start()
    {
        OnePlayerStartButton.onClick.AddListener(()=>{ 
            GameSceneUtil.Instance.SingleSceneTransration(
                ConcentrationGameStringResource.ONCENTRATION_GAME_MAIN_SCENE,
                ()=> OnePlayerStartButtonAction());
        });

         TwoPlayerStartButton.onClick.AddListener(()=>{ 
            GameSceneUtil.Instance.SingleSceneTransration(
                ConcentrationGameStringResource.ONCENTRATION_GAME_MAIN_SCENE);
        });
    }

    /// <summary>
    /// 呼び出し先のConcentrationGameProgressionManagerにCPUCardはComputerが選択したと伝える
    /// </summary>
    public void OnePlayerStartButtonAction()
    {
        //GameMainにいるConcentrationGameProgressionManagerの取得
        var gameProgressionManagerGameObject =
            GameSceneUtil.Instance.NextSceneRootGetGameObjects.
            Where(x=>x.GetComponent<ConcentrationGameProgressionManager>()).FirstOrDefault();

        if (gameProgressionManagerGameObject != null)
        {
            var gameProgressionManager = gameProgressionManagerGameObject.GetComponent<ConcentrationGameProgressionManager>();
            gameProgressionManager.GameMode = ConcentrationGameProgressionManager.GameModes.CPUCardIsComputerChoice;
        }
    }
}
