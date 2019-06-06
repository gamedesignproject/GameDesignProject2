using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerData : MonoBehaviour
{
    // 使用するポイントリスト
    public List<int> activePointList;

    // 解答のポイントリスト
    public List<int> answerPointList;

    // 図形のタイプ
    public TYPE type;
    
    public enum TYPE
    {
        // 正三角形
        REGULAR_TRIANGLE,
    }

}
