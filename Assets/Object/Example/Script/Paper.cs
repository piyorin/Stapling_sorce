﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{

    public enum PAPER_REVERSE //裏表プロパティ
    {
        OMOTE, //表
        URA, //裏
    }

    public enum PAPER_ORGANIZE //整頓プロパティ
    {
        GOOD, //整理されている
        BAD, //整理されていない
    }

    public PAPER_REVERSE reverse = PAPER_REVERSE.OMOTE;
    public PAPER_ORGANIZE organize = PAPER_ORGANIZE.BAD;

    public AudioSource audioSource;
    public AudioClip clipStart;
    public AudioClip clipEnd;


    // Start is called before the first frame update
    void Start()
    {
        //生成時にSEを鳴らす
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipStart);
        Debug.Log("Paperの生成時SEが再生されました");
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
        Debug.Log("Paperの破棄時SEが再生されました");
    }
}
