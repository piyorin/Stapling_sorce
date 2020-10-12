using UnityEngine;
using System.Collections;

/// <summary>
/// Paper上に生成された針
/// </summary>
public class StapedNeedle: MonoBehaviour
{
    // プレファブ
    public static Needle needlePrefab;

    // 実体
    private Needle entity;

    // 生成座標
    private Vector2 position;

    // 生成座標(Z軸)
    private float instancePositionZ;

    // 除去判定許容範囲
    public float pullOutRange;

    // 削除されるときは実体もろとも削除
    private void OnDestroy()
    {
        Destroy(entity);
    }

    /// <summary>
    /// 生成座標を記録
    /// </summary>
    /// <param name="position">座標</param>
    public void setPosition(Vector2 position)
    {
        this.position = position;
        Instantiate(needlePrefab, new Vector3(position.x, position.y, instancePositionZ), Quaternion.identity);

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
