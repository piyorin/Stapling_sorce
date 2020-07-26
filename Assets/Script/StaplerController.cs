using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerController : MonoBehaviour
{
    private List<Vector3> instanceNeedlePositionList = new List<Vector3>();

    // 針の数
    private int remainingNeedle = 0;
    private const int remainingNeddleLimmite = 10;

    // 針の生成場所(Z軸)
    public float instancePositionZ;

    // 針
    public GameObject needle;

    void Start()
    {
        reload();
    }

    private void Update()
    {
        //【仮置き】リロード機能追加
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reload();
        }
    }

    // 針を補充する
    private void reload()
    {
        remainingNeedle = remainingNeddleLimmite;
        Debug.Log("針をリロードした");
    }

    // 指定の場所に針がなければ生成、あれば削除する
    public void instance(float positionX, float positionY)
    {
        var instancePosition = new Vector3(positionX, positionY, instancePositionZ);

        if (instanceNeedlePositionList.Contains(instancePosition))
        {
            destroy(instancePosition);
        }
        else
        {
            instance(instancePosition);
        }
    }

    // 針を生成する
    private void instance(Vector3 instancePosition)
    {
        // 針が残っていなければ生成しない
        if (remainingNeedle <= 0)
        {
            return;
        }

        // 指定された場所にオブジェクトを生成
        Instantiate(needle, instancePosition, Quaternion.identity);
        instanceNeedlePositionList.Add(instancePosition);
        remainingNeedle--;
        Debug.Log(string.Format("({0}, {1})に針を生成\r\n残り:{2}", instancePosition.x, instancePosition.y, remainingNeedle));
    }

    // 針を削除する
    private void destroy(Vector3 target)
    {
        instanceNeedlePositionList.Remove(target);
        Debug.Log(string.Format("({0}, {1})の針を削除", target.x, target.y));
    }
}
