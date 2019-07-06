using UnityEngine;

public class Point : MonoBehaviour
{
    // ポインタの番号
    [SerializeField] private int pointNum;

    public int PointNum { get { return pointNum; } }

    [SerializeField] private LineGenerator lineGenerator;
    
    // マウスが押された時の処理
    public void MouseDown()
    {
        Debug.Log("mouse down");
        if(lineGenerator.state == LineGenerator.STATE.NONE && lineGenerator.IsLineLastPoint(pointNum))
        {
            lineGenerator.AddLineObject(this.transform.GetChild(0).position, pointNum, true);
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
            lineGenerator.AddLineObject(this.transform.GetChild(0).position, pointNum);
        }
    }


}
