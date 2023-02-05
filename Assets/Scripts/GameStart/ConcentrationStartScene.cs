using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ConcentrationStartScene : MonoBehaviour
{
    [SerializeField]
    private Button OnePlayerStartButton;
    [SerializeField]
    private Button TwoPlayerStartButton;

    private bool resourcesLoadComplete = false;

    private void Start()
    {
        GameSoundManager.Instance.Initualze();

        OnePlayerStartButton.onClick.AddListener(() =>
        {
            GameSceneUtil.Instance.SingleSceneTransration(
                ConcentrationGameStringResource.ONCENTRATION_GAME_MAIN_SCENE,
                () => OnePlayerStartButtonAction());
        });

        TwoPlayerStartButton.onClick.AddListener(() =>
        {
            GameSceneUtil.Instance.SingleSceneTransration(
                ConcentrationGameStringResource.ONCENTRATION_GAME_MAIN_SCENE);
        });

        StartCoroutine(resourcesLoad());

        StartCoroutine(startBGM());
    }

    IEnumerator resourcesLoad()
    {
        var bgmLoadhandle = Addressables.LoadAssetsAsync<AudioClip>("BGM", null);
        yield return bgmLoadhandle;

        if (bgmLoadhandle.Status == AsyncOperationStatus.Succeeded)
        {
            for (int i = 0; i < bgmLoadhandle.Result.Count; i++)
            {
                GameSoundManager.Instance.SetBGMAudioClips(bgmLoadhandle.Result[i]);
            }
        }

        Addressables.Release(bgmLoadhandle);

        var seLoadhandle = Addressables.LoadAssetsAsync<AudioClip>("SE", null);
        yield return seLoadhandle;
        for(int i = 0; i < seLoadhandle.Result.Count; i++)
        {
            GameSoundManager.Instance.SetSEAudioClips(seLoadhandle.Result[i]);
        }

        Addressables.Release(seLoadhandle);
        resourcesLoadComplete = true;

    }

    IEnumerator startBGM()
    {
        yield return new WaitUntil(()=> resourcesLoadComplete);

        GameSoundManager.Instance.PlayBGM(GameSoundManager.BGMTypes.GameStart);
    }

    /// <summary>
    /// åƒÇ—èoÇµêÊÇÃConcentrationGameProgressionManagerÇ…CPUCardÇÕComputerÇ™ëIëÇµÇΩÇ∆ì`Ç¶ÇÈ
    /// </summary>
    public void OnePlayerStartButtonAction()
    {
        //GameMainÇ…Ç¢ÇÈConcentrationGameProgressionManagerÇÃéÊìæ
        var gameProgressionManagerGameObject =
            GameSceneUtil.Instance.NextSceneRootGetGameObjects.
            Where(x => x.GetComponent<ConcentrationGameProgressionManager>()).FirstOrDefault();

        if (gameProgressionManagerGameObject != null)
        {
            var gameProgressionManager = gameProgressionManagerGameObject.GetComponent<ConcentrationGameProgressionManager>();
            gameProgressionManager.GameMode = ConcentrationGameProgressionManager.GameModes.CPUCardIsComputerChoice;
        }
    }
}
