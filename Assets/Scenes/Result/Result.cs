using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = $"{GameData.gamedata.Score[GameData.gamedata.Score.Count - 1]} 問";
    }

    void Update()
    {
       
    }

    public void SceneChangeTitle()
    {
        GameData.gamedata.SceneChange(GameData.SceneState.TITLE);
    }
}
