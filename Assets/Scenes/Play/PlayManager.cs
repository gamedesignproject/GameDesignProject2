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
    // 正解アニメーション
    [SerializeField] GameObject CorrectAnime;

    // 現在の問題番号
    private int questionNum;
    // 現在の挑戦中の問題
    private AnswerData answerData;

    // 問題の回転角度
    private const int EasyAngle = 60;

    // ゲーム時間
    private const int Timelimit = 60;

    private float GameTime;

    // 正解数
    private int ScoreCount;

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
        ScoreCount = 0;

        Debug.Log(answerData.type);
    }

    private void Update()
    {
        PlayUI();

        // 答えの合わせ
        if (lineGenerator.state == LineGenerator.STATE.ANSWER && state == State.NONE)
        {
            Debug.Log("チェック");
            if (answer.CheckAnswer(questionNum, lineGenerator.linePointList))
            {
                //正解
                lineGenerator.LineReset();
                state = State.CORRECT;
                ScoreCount += 1;
            }
            else
            {
                lineGenerator.LineReset();
                lineGenerator.state = LineGenerator.STATE.NONE;
            }
          
        }

        if (state == State.CORRECT)
        {
            // NextQestion();
            StartCoroutine(ShowCorrectAnimater());
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

            case AnswerData.TYPE.TAKOGATA:
                // 凧形
                infoText.text = "隣り合った2本の辺の長さが等しい組が2組ある図形";
                figureName.text = "凧形";
                break;

            case AnswerData.TYPE.TRAPEZOID:
                // 台形
                infoText.text = "四角形の一部で、少なくとも一組の対辺が互いに平行であるような図形";
                figureName.text = "台形";
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
    public void NextQestion()
    {
        questionNum += 1;

        questionNum = answer.CheakAnswerList(questionNum);
        answerData = answer.GetAnswer(questionNum);

        //if ((answerData = answer.GetAnswer(questionNum)) == default)
        //{
        //    questionNum = 0;
        //    answerData = answer.GetAnswer(questionNum);
        //}
        InitializePoint();
        SetFigureInfo();
        state = State.NONE;
        lineGenerator.state = LineGenerator.STATE.NONE;

        Debug.Log("次");
    }

    // 正解アニメーションと次の問題への切り替え
    IEnumerator ShowCorrectAnimater()
    {
        CorrectAnime.SetActive(true);
        Animator animator = CorrectAnime.transform.GetChild(0).GetComponent<Animator>();
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0); 

        while(state.normalizedTime < 1)
        {
            yield return null;
        }

        CorrectAnime.SetActive(false);
        NextQestion();
    }

    // UI描画
    void PlayUI()
    {
        GameTime -= Time.deltaTime;

        string time = ((int)GameTime).ToString(); ;

        Timetext.text = $"残り時間 {time} : 正解数 {ScoreCount}";

        // ゲーム終了
        if (GameTime < 0)
        {
            GameData.gamedata.AddScore(ScoreCount);
            GameData.gamedata.SceneChange(GameData.SceneState.RESULT);
        }
    }
}
