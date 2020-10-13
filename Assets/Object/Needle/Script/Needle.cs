using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clipStart;
    public AudioClip clipEnd;

    void Start()
    {
        //生成時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipStart);
        Debug.Log("針が生成された。");
    }

    private void OnDestroy()
    {
        //破棄時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipEnd);
        Debug.Log("針を削除した。");
    }
}
