using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    // 除去判定許容範囲
    public float pullOutRange;

    // 生成座標
    private Vector2 position;

    public AudioSource audioSource;
    public AudioClip clipStart;
    public AudioClip clipEnd;

    void Start()
    {
        //生成時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipStart);
        Debug.Log("Needleの生成時SEが再生されました");
    }

    private void OnDestroy()
    {
        //破棄時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipEnd);
        Debug.Log("Needleの破棄時SEが再生されました");
    }

    /// <summary>
    /// 生成座標を記録
    /// </summary>
    /// <param name="position">座標</param>
    public void setPosition(Vector2 position)
    {
        this.position = position;
    }

    /// <summary>
    /// 指定座標と自身の座標があっているか判定
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool checkPosition(Vector2 position)
    {
        return checkWithinRange(this.position.x, position.x) && checkWithinRange(this.position.y, position.y);
    }

    // targetが、comparison+-pullOutRangeの範囲にいるか判定
    private bool checkWithinRange(float target, float comparison)
    {
        return comparison - pullOutRange < target && target < comparison + pullOutRange;
    }
}
