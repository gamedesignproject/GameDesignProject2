﻿using UnityEngine;

public class Point : MonoBehaviour
{
    // ポインタの番号
    [SerializeField] private int pointNum;

    [SerializeField] private LineGenerator lineGenerator;
    
    // マウスが押された時の処理
    public void MouseDown()
    {
        Debug.Log("mouse down");
        if(lineGenerator.state == LineGenerator.STATE.NONE)
        {
            lineGenerator.AddLineObject(this.transform.position, pointNum, true);
        }
    }

    // マウスがポイントと接触したときの処理
    public void EnterMousePoint()
    {
        // 描画中かつ前回とは違うポイントの時は線を追加
        // 同じ線がすでにある場合は追加しない
        if(lineGenerator.state == LineGenerator.STATE.DRAW && lineGenerator.PastPoint != pointNum && !lineGenerator.CheckHaveLine(pointNum))
        {
            Debug.Log("線追加");
            lineGenerator.AddLineObject(this.transform.position, pointNum);
        }
    }


}
