using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タップ入力を判定するクラス
public class TapController : MonoBehaviour
{
    public StaplerController staplerController;
    public Paper paper;

    // タップ開始終了判定
    private enum StartOrStop { START, STOP };

    // タップ・フリック判定
    private enum TapOrFlick { TAP, VERTICAL, UP, DOWN }

    // タップ開始時のカーソル位置
    private float tapStartPositionX = 0;
    private float tapStartPositionY = 0;

    // タップ中のカーソル位置
    private float tapEndPositionX = 0;
    private float tapEndPositionY = 0;

    // フリック判定となる値
    public float flicJudgeDistance = 1;

    void Update()
    {
        // クリック開始判定
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Debug.Log("タップが開始された");
            updateTapPoint(StartOrStop.START);
        }

        // クリック終了判定
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            updateTapPoint(StartOrStop.STOP);
            mouseButtonUpExecute();
            Debug.Log("タップが終了した");
        }
    }

    // 入力終了時の処理を実行
    private void mouseButtonUpExecute()
    {
        // 入力判定
        switch (judgeTapOrFlick())
        {
            // タップの場合はホッチキスの針生成処理を呼び出し
            case TapOrFlick.TAP:
                Debug.Log("判定：タップ");
                staplerController.instance(
                    (float)Math.Round(tapStartPositionX, 2, MidpointRounding.AwayFromZero),
                    (float)Math.Round(tapStartPositionY, 2, MidpointRounding.AwayFromZero)
                );
                break;
            // 横フリックの場合は紙を裏返す処理を呼び出し
            case TapOrFlick.VERTICAL:
                Debug.Log("判定：横スワイプ");
                if (paper != null)
                {
                    paper.flipOver();
                }
                break;
            // 上フリックの場合は提出処理を呼び出し
            case TapOrFlick.UP:
                Debug.Log("判定：上フリック");
                break;
            // 下フリックの場合は紙を揃える処理を呼び出し
            case TapOrFlick.DOWN:
                Debug.Log("判定：下フリック");
                if (paper != null)
                {
                    paper.arrange();
                }
                break;
        }
    }

    // タップ開始位置と終了位置の差からタップ・横フリック・縦フリックを判定
    private TapOrFlick judgeTapOrFlick()
    {
        // 左右移動が一定以上あれば横フリック
        if (Mathf.Abs(tapStartPositionX - tapEndPositionX) > flicJudgeDistance)
        {
            return TapOrFlick.VERTICAL;
        }
        // 上下移動が一定以上で下方向への移動なら下フリック
        else if (
            Mathf.Abs(tapStartPositionY - tapEndPositionY) > flicJudgeDistance &&
            tapStartPositionY - tapEndPositionY > 0
        ){
            return TapOrFlick.DOWN;
        }
        // 上下移動が一定以上で上方向への移動ならしたフリック
        else if (
            Mathf.Abs(tapStartPositionY - tapEndPositionY) > flicJudgeDistance &&
            tapStartPositionY - tapEndPositionY < 0
        ){
            return TapOrFlick.UP;
        }
        // それ以外はタップ
        else
        {
            return TapOrFlick.TAP;
        }
    }

    // マウス位置をワールド座標でX軸Y軸それぞれ取得
    private void updateTapPoint(StartOrStop order)
    {
        var cursorPos = Input.mousePosition;
        cursorPos.z = 1;
        var screeenCursorPos = Camera.main.ScreenToWorldPoint(cursorPos);

        // タップ位置を更新
        switch (order)
        {
            // 開始位置
            case StartOrStop.START:
                tapStartPositionX = screeenCursorPos.x;
                tapStartPositionY = screeenCursorPos.y;
                Debug.Log(string.Format("タップ開始位置を更新 X:{0} Y;{1}", tapStartPositionX, tapStartPositionY));
                break;
            // 終了位置
            case StartOrStop.STOP:
                tapEndPositionX = screeenCursorPos.x;
                tapEndPositionY = screeenCursorPos.y;
                Debug.Log(string.Format("タップ終了位置を更新 X:{0} Y;{1}", tapEndPositionX, tapEndPositionY));
                break;
        }
    }
}
