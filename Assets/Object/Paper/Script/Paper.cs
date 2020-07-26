using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    //裏表プロパティ
    public enum Face { UP, DOWN }

    //整頓プロパティ
    public enum Orgenize { GOOD, BAD, }

    public Face faceStatus = Face.UP;
    public Orgenize organizeStatus = Orgenize.BAD;

    public AudioSource audioSource;
    public AudioClip flipOverClip;
    public AudioClip orgenizeClip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        //破棄時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(orgenizeClip);
        Debug.Log("Paperの破棄時SEが再生されました");
    }

    // 紙を裏返す
    public void flipOver()
    {
        faceStatus = faceStatus == Face.UP ? Face.DOWN : Face.UP;
        audioSource.PlayOneShot(flipOverClip);
        Debug.Log(string.Format("紙を裏返した 裏返した後の状態:{0}", faceStatus));
    }

    // 乱れた紙を揃える
    public void arrange()
    {
        // 揃っているならステータス変更はしない
        if (organizeStatus != Orgenize.GOOD)
        {
            // より揃っているステータスに変更
            organizeStatus = (Orgenize)Enum.ToObject(typeof(Orgenize), organizeStatus - 1);
        }
        audioSource.PlayOneShot(orgenizeClip);
        Debug.Log(string.Format("紙を揃えた 揃えた後の状態:{0}", organizeStatus));
    }
}
