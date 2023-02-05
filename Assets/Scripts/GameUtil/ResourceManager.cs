using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(loadMusic());
    }

    /// <summary>
    /// Addressables�Ń��[�h�����{�`
    /// </summary>
    /// <returns></returns>
    IEnumerator loadMusic()
    {
        var handle = Addressables.LoadAssetAsync<AudioClip>("Assets/Music/BGM/�g�����v�E�Q�[��.mp3");

        yield return handle;

        if(handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            audioSource.clip = handle.Result;
            audioSource.Play();
        }
    }
}
