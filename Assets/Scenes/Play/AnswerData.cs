using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerData : MonoBehaviour
{
    // 使用するポイントリスト
    public List<int> activePointList;

    // 解答のポイントリスト
    public List<AnswerExample> answerPointList;

    // 図形のタイプ
    public TYPE type;
    
    public enum TYPE
    {
        // 正三角形
        REGULAR_TRIANGLE,
        // 直角三角形
        RIGHT_TRIANGLE,
        // 二等辺三角形
        ISOSCELES_TRIANGLE,
        // 長方形
        RECTANGULAR,
        // 凧形
        TAKOGATA,
        // 台形
        TRAPEZOID,

    }

}
