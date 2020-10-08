using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerController : MonoBehaviour
{
    // 針の数
    private int remainingNeedle = 0;
    private const int remainingNeddleLimmite = 10;

    // 針の生成場所(Z軸)
    public float instancePositionZ;

    // 紙
    public Paper paperScript;
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

    // 紙に針を刺す
    public void bind(float positionX, float positionY)
    {
        var instancePosition = new Vector3(positionX, positionY, instancePositionZ);

        // 綴じる場所ですでに閉じているなら除去、そうでないなら綴じる
        if (paperScript.isAlreadyInstance(instancePosition))
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
        var instance = Instantiate(needle, instancePosition, Quaternion.identity);
        paperScript.addInstanceNeedle(instance, instancePosition);
        remainingNeedle--;
        Debug.Log(string.Format("({0}, {1})に針を生成\r\n残り:{2}", instancePosition.x, instancePosition.y, remainingNeedle));
    }

    // 針を削除する
    private void destroy(Vector3 target)
    {
        paperScript.destroyNeedle(target);
        Debug.Log(string.Format("({0}, {1})の針を削除", target.x, target.y));
    }
}
