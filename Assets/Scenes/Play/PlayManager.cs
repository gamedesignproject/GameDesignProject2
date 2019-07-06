using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    [SerializeField] LineGenerator lineGenerator;
    // 問題一覧
    [SerializeField] Answer answer;
    // 図形の説明
    [SerializeField] Text infoText;
    // 図形の名称
    [SerializeField] Text figureName;
    // Easyのポイント
    [SerializeField] GameObject easyPoint;

    // 現在の問題番号
    private int questionNum;
    // 現在の挑戦中の問題
    private AnswerData answerData;

    private void Start()
    {
        answer.Initialize();
        answerData = answer.GetAnswer(questionNum);
        SetFigureInfo();
        InitializePoint();
    }

    private void Update()
    {
        // 答えの合わせ
        if(lineGenerator.state == LineGenerator.STATE.ANSWER)
        {
            answer.CheckAnswer(questionNum, lineGenerator.linePointList);
            lineGenerator.state = LineGenerator.STATE.NONE;
        }
    }

    /// <summary>
    /// 図形の情報を登録
    /// </summary>
    void SetFigureInfo()
    {
        switch (answerData.type)
        {
            case AnswerData.TYPE.REGULAR_TRIANGLE:
                // 正三角形
                infoText.text = "3つの「辺」が同じ長さの三角形";
                figureName.text = "正三角形";
                break;
        }
    }

    /// <summary>
    /// 問題に使用するポイントを登録する
    /// </summary>
    void InitializePoint()
    {
        foreach(Transform childPoint in easyPoint.transform)
        {
            if(answerData.activePointList.Contains(childPoint.GetComponent<Point>().PointNum))
            {
                childPoint.gameObject.SetActive(true);
            }
            else
            {
                childPoint.gameObject.SetActive(false);
            }
        }
    }
}
