using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clipStart;
    public AudioClip clipEnd;

    // Start is called before the first frame update
    void Start()
    {
        //生成時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipStart);
        Debug.Log("Needleの生成時SEが再生されました");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        //破棄時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipEnd);
        Debug.Log("Needleの破棄時SEが再生されました");
    }
}
