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
        new AnswerData(){activePointList = new List<int>{1, 3, 4, 5}, answerPointList = new List<int>(){1, 3, 5}, type = AnswerData.TYPE.REGULAR_TRIANGLE },
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
    public bool CheckAnswer(int questionNum, List<int> pointList)
    {
        // 点の数が一致してい無ければ不正解
        if(answerList[questionNum].answerPointList.Count != pointList.Count)
        {
            Debug.Log("不正解");
            return false;
        }

        // ポイントが一致しているかを確認
        //return answerList[questionNum].answerPointList.All(answerPoint => pointList.Contains(answerPoint));
        if(answerList[questionNum].answerPointList.All(answerPoint => pointList.Contains(answerPoint))){
            Debug.Log("正解");
            return true;
        }else{
            Debug.Log("不正解");
            return false;
        }
    }
}
