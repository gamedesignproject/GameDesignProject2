using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    // シングルトン
    public static GameData gamedata;

    public List<int> Score;

    public enum SceneState
    {
        TITLE,
        PLAY,
        RESULT,
    }

    private void Awake()
    {
        if (gamedata == null)
        {
            gamedata = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // スコアの追加
    public void AddScore(int s)
    {
        Score.Add(s);
    }

    // スコアの整理
    public void ScoreOrganize()
    {
        // スコアを高い順に並べる
        Score.Sort((a, b) => b - a);

        // スコアが３つ以上の時一番小さいスコアを消す
        if (Score.Count < 3)
        {
            Score.RemoveRange(3, Score.Count - 1);
        }
    }

    // シーンの切り替え
    public void SceneChange(SceneState next)
    {
        SceneManager.LoadScene((int)next);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
