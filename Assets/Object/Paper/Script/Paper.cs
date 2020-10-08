using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paper : MonoBehaviour
{
    //裏表プロパティ
    public enum Face { UP, DOWN }

    //整頓プロパティ
    public enum Orgenize { GOOD, BAD, }

    /*
     生成した針一覧
     key  : index
     value: 生成オブジェクト
     keyは常に増加、正の整数のみ。
    */
    public Dictionary<int, Needle> stapedNeedles = new Dictionary<int, Needle>();

    // 紙のステータス
    public Face faceStatus = Face.UP;
    public Orgenize organizeStatus = Orgenize.BAD;

    public AudioSource audioSource;
    public AudioClip flipOverClip;
    public AudioClip orgenizeClip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 指定した位置に針がなければ針で綴じる。
    /// 針があれば抜く。
    /// </summary>
    /// <param name="stapPosition">綴じる位置</param>
    public void stap(Vector2 stapPosition)
    {
        int index = checkNeedle(stapPosition);
        if (index > 0)
        {
            // 針を抜く
            pullOutNeedle(index);
        }
        else
        {
            // 針で綴じる
            stapNeedle(stapPosition);
        }
    }

    /// <summary>
    /// 裏返す
    /// </summary>
    public void flipOver()
    {
        faceStatus = faceStatus == Face.UP ? Face.DOWN : Face.UP;
        audioSource.PlayOneShot(flipOverClip);
        Debug.Log(string.Format("紙を裏返した。 裏返した後の状態:{0}", faceStatus));
    }

    /// <summary>
    /// 揃える
    /// </summary>
    public void arrange()
    {
        Debug.Log(string.Format("紙を揃える 揃える前の状態:{0}", organizeStatus));
        // 揃っているならステータス変更はしない
        if (organizeStatus != Orgenize.GOOD)
        {
            // より揃っているステータスに変更
            organizeStatus = (Orgenize)Enum.ToObject(typeof(Orgenize), organizeStatus - 1);
        }
        audioSource.PlayOneShot(orgenizeClip);
        Debug.Log(string.Format("紙を揃えた 揃えた後の状態:{0}", organizeStatus));
    }

    // 指定された位置で綴じる
    private void stapNeedle(Vector2 position)
    {

    }

    // 指定番号の針を抜く
    private void pullOutNeedle(int index)
    {
        try
        {
            Debug.Log(string.Format("針を抜く。 index:{0}", index));
            bool result = stapedNeedles.TryGetValue(index, out Needle target);
            if (result == false || target == null)
            {
                Debug.LogWarning(string.Format("抜くはずだった針がない…？ index:{0}", index));
                return;
            }
            stapedNeedles.Remove(index);
            Destroy(target);
            Debug.Log(string.Format("針を抜いた。 index:{0}", index));
        }
        catch (Exception ex)
        {
            Debug.LogWarning(string.Format("針抜きでError。 index:{0}\r\n{1}", index, ex));
        }
    }


    // 指定した位置に針があるか確認する
    private int checkNeedle(Vector2 position)
    {
        Debug.Log(string.Format("針があるか確認。 x:{0} y:{1}", position.x, position.y));
        foreach (var needle in stapedNeedles)
        {
            // 針があるならkeyを返す
            if (needle.Value.checkPosition(position))
            {
                Debug.Log(string.Format("針が見つかった。 index:{0}", needle.Key));
                return needle.Key;
            }
            
        }

        // 見つからなければ-1を返す。
        Debug.Log("針は見つからなかった。");
        return -1;
    }

}
