using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private float lineWidth;
    [SerializeField] private Color lineColor;
    
    // 線を管理するリスト
    public List<LineRenderer> lineRendererList;

    // ポイントを管理するリスト
    public List<int> pointList;

    // 前回のポイント
    public int PastPoint { get; set; }

    // 線の始点と終点を管理するリスト
    // (番号小, 番号大)で登録
    public List<Vector2> linePointList;

    public enum STATE
    {
        NONE,       // 状態なし
        DRAW,       // 描画中
    }

    public STATE state;
    
    void Start()
    {
        lineRendererList = new List<LineRenderer>();
        linePointList = new List<Vector2>();

        state = STATE.NONE;

        PastPoint = -1;
    }
    
    void Update()
    {
        if(state == STATE.DRAW)
        {
            if (Input.GetMouseButton(0))
            {
                updateLineRendererEndPosition();
            }
            else
            {
                state = STATE.NONE;
                Destroy(lineRendererList.Last().gameObject);
                lineRendererList.Remove(lineRendererList.Last());
                DeletePointNum(PastPoint);
            }
        }
    }

    /// <summary>
    /// ポイントリストにポイントを追加
    /// </summary>
    private void AddPointList(int pointNum)
    {

        if (pointList != null && !pointList.Contains(pointNum))
        {
            pointList.Add(pointNum);
        }
    }

    /// <summary>
    /// 線オブジェクト追加
    /// </summary>
    /// <param name="isStart"> 描画の始まりか.</param>
    public void AddLineObject(Vector3 position, int pointNum, bool isStart = false)
    {
        state = STATE.DRAW;

        // 描画の始まりでないなら線のポイントを追加する
        if (!isStart)
        {
            AddLinePointList(pointNum);
            
            // 線が既にある場合は線の終点を更新
            updateLineEndPoint(position);
        }

        // ポイントの番号を登録
        PastPoint = pointNum;
        AddPointList(pointNum);

        // オブジェクトをインスタンス化
        GameObject lineObject = new GameObject();

        // オブジェクトにLineRendererコンポーネントの追加
        lineObject.AddComponent<LineRenderer>();

        var lineRenderer = lineObject.GetComponent<LineRenderer>();

        // 線と線をつなぐ点の数
        lineRenderer.positionCount = 2;

        // 線の色を初期化
        lineRenderer.material.color = lineColor;

        // 線の太さを初期化
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // 線の始点の座標の登録
        lineRenderer.SetPosition(0, position);

        // Order In layer登録
        lineObject.layer = 9;

        // リストに登録
        lineRendererList.Add(lineRenderer);
    }
 
    /// <summary>
    /// 線の終点位置を更新する
    /// </summary>
    public void updateLineRendererEndPosition()
    {
        // マウスの座標を取得
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // 現在描画中の線の終点の座標を更新
        lineRendererList.Last().SetPosition(1, mousePosition);
    }

    /// <summary>
    /// 線の終点をポイントの座標で更新
    ///</summary>
    private void updateLineEndPoint(Vector3 position)
    {
        if (lineRendererList != null && lineRendererList.Count > 0)
        {
            lineRendererList.Last().SetPosition(1, position);
        }
    }

    /// <summary>
    /// 既に線があるかを確認
    /// </summary>
    public bool CheckHaveLine(int pointNum)
    {
        // 同じ点なら持っていることにする
        if (pointNum == PastPoint) return true;
        // リストが空っぽならfalseを返す
        if (linePointList == null || linePointList.Count == 0) return false;

        var linePoint = pointNum < PastPoint ? new Vector2(PastPoint, pointNum) : new Vector2(pointNum, PastPoint);
        return linePointList.Any(_=>_ == linePoint);   
    }

    /// <summary>
    /// 線の点を管理するリストに点を登録
    /// </summary>
    public void AddLinePointList(int pointNum)
    {
        linePointList.Add(pointNum < PastPoint ? new Vector2(PastPoint, pointNum) : new Vector2(pointNum, PastPoint));
    }

    /// <summary>
    /// 線の始点・終点にポイントが使用されていない場合削除
    /// </summary>
    // TODO: メソッド名微妙、変更するかも
    public void DeletePointNum(int pointNum)
    {
        // 線の始点・終点にポイントが使用されていないならば削除
        if(!linePointList.Exists(_=>_.x == pointNum || _.y == pointNum))
        {
            pointList.Remove(pointNum);
        }
    }

    /// <summary>
    /// 最後に描画した線の終点かを返す
    /// 線がない場合もtrue
    /// </summary>
    public bool IsLineLastPoint(int pointNum)
    {
        return PastPoint == pointNum || PastPoint == -1;
    }
}
