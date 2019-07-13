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
    // 残り時間
    [SerializeField] Text Timetext;

    // 現在の問題番号
    private int questionNum;
    // 現在の挑戦中の問題
    private AnswerData answerData;

    // 問題の回転角度
    private const int EasyAngle = 60;

    // ゲーム時間
    private const int Timelimit = 60;

    private float GameTime;

    private enum State
    {
        NONE,
        CORRECT,
    }

    private State state;

    private void Start()
    {
        answer.Initialize();
        answerData = answer.GetAnswer(questionNum);
        SetFigureInfo();
        InitializePoint();
        state = State.NONE;
        GameTime = Timelimit;
        

        Debug.Log(answerData.type);
    }

    private void Update()
    {
        GameTime -= Time.deltaTime;

        Timetext.text = ((int)GameTime).ToString();

        // 答えの合わせ
        if (lineGenerator.state == LineGenerator.STATE.ANSWER && state == State.NONE)
        {
            if (answer.CheckAnswer(questionNum, lineGenerator.linePointList))
            {
                //正解
                lineGenerator.LineReset();
                state = State.CORRECT;
            }
            else
            {
               // lineGenerator.LineReset();
                lineGenerator.state = LineGenerator.STATE.NONE;
            }
          
        }

        if (state == State.CORRECT)
        {
            NextQestion();
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

            case AnswerData.TYPE.RIGHT_TRIANGLE:
                // 直角三角形
                infoText.text = "１つの角が「直角」になる三角形";
                figureName.text = "直角三角形";
                break;

            case AnswerData.TYPE.ISOSCELES_TRIANGLE:
                // 二等辺三角形
                infoText.text = "２つの「辺」が同じ長さの三角形";
                figureName.text = "二等辺三角形";
                break;

            case AnswerData.TYPE.RECTANGULAR:
                // 長方形
                infoText.text = "４つの内角がすべて直角である四辺形";
                figureName.text = "長方形";
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

        SetAngle(easyPoint);
    }

    // 問題の角度
    void SetAngle(GameObject point)
    {
        int Rand = Random.Range(0,point.transform.childCount);
        int Angle = Rand * EasyAngle;
        Vector3 rot = new Vector3(0, 0, Angle);

        point.transform.Rotate(rot);
    }

    // 次の問題への切り替え
    void NextQestion()
    {
        questionNum += 1;
        answerData = answer.GetAnswer(questionNum);
        InitializePoint();
        SetFigureInfo();
        state = State.NONE;
        lineGenerator.state = LineGenerator.STATE.NONE;

        Debug.Log("次");
    }
}
