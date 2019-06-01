using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private float lineWidth;
    [SerializeField] private Color lineColor;

    /// <summary>
    /// 線を保存するリスト
    /// </summary>
    public List<LineRenderer> lineRendererList;

    public enum STATE
    {
        NONE,       // 状態なし
        DRAW,       // 描画中
    }

    public STATE state;
    
    void Start()
    {
        lineRendererList = new List<LineRenderer>();

        state = STATE.NONE;
    }
    
    void Update()
    {
        if(state == STATE.DRAW)
        {
            if (Input.GetMouseButton(0))
            {
                updateLineRendererEndPosition();
            }
        }
    }

    /// <summary>
    /// 線オブジェクト追加
    /// </summary>
    public void AddLineObject(Vector3 position)
    {
        Debug.LogError(position);

        state = STATE.DRAW;

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
}
