using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Answer : MonoBehaviour
{
    // 問題番号と答えの図形の頂点を管理するリスト
    private List< AnswerData> answerList;

    // 答えのデータ
    // データはここに追加していく
    List<AnswerData> answerDatas = new List<AnswerData>
    {
        new AnswerData(){activePointList = new List<int>{ 1, 3, 4, 5}, answerPointList = new List<AnswerExample>{ SetanswerLine(1,3,5) }, type = AnswerData.TYPE.REGULAR_TRIANGLE },
        new AnswerData(){activePointList = new List<int>{ 1, 2, 3, 4, 5}, answerPointList = new List<AnswerExample>{ SetanswerLine(1,2,4),SetanswerLine(1,2,5),SetanswerLine(1,4,5),SetanswerLine(2,4,5) }, type = AnswerData.TYPE.RIGHT_TRIANGLE },
        new AnswerData(){activePointList = new List<int>{ 2, 3, 4, 6}, answerPointList = new List<AnswerExample>{ SetanswerLine(2,3,4) }, type = AnswerData.TYPE.ISOSCELES_TRIANGLE },
        new AnswerData(){activePointList = new List<int>{ 1, 2, 4, 5 ,6}, answerPointList = new List<AnswerExample>{ SetanswerLine(1,2,4,5) }, type = AnswerData.TYPE.RECTANGULAR },
    };

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        // 答えのリストをランダムにソート
        answerList = answerDatas.OrderBy(_ => Guid.NewGuid()).ToList();
    }

    /// <summary>
    /// 答えを取得
    /// </summary>
    public AnswerData GetAnswer(int index)
    {
        return answerList[index];
    }

    /// <summary>
    /// 答え合わせ
    /// </summary>
    public bool CheckAnswer(int questionNum, List<Vector2> pointList)
    {
        // 点の数が一致してい無ければ不正解
        //if(answerList[questionNum].answerPointList.Count != pointList.Count)
        //{
        //    Debug.Log("不正解");
        //    return false;
        //}

        // ポイントが一致しているかを確認
        //return answerList[questionNum].answerPointList.All(answerPoint => pointList.Contains(answerPoint));
        for (int i = 0; i < answerList[questionNum].answerPointList.Count; i++) {
            Debug.Log("回答数" + answerList[questionNum].answerPointList.Count);
            if (answerList[questionNum].answerPointList[i].answerLine.All(answerPoint => pointList.Contains(answerPoint))) {
                Debug.Log("正解");
                return true;
            }
        }

        Debug.Log("不正解");
        return false;
    }

    // new Vector2 を簡易化する関数
    private static Vector2 Vec2(int a,int b)
    {
        return new Vector2(a, b);
    }

    public static AnswerExample SetanswerLine(int a = -1, int b = -1, int c = -1, int d = -1)
    {
        List<Vector2> set = new List<Vector2> { };

        if (d != -1)
        {
            set.Add(new Vector2(a, b));
            set.Add(new Vector2(b, c));
            set.Add(new Vector2(c, d));
            set.Add(new Vector2(a, d));
        }
        else if (c != -1)
        {
            set.Add(new Vector2(a, b));
            set.Add(new Vector2(b, c));
            set.Add(new Vector2(a, c));
        }

        AnswerExample answer = new AnswerExample();

        answer.answerLine = set;

        return answer;
    }
}
