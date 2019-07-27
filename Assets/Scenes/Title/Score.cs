using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameData.gamedata.ScoreOrganize();

        ScoreText = transform.GetComponent<Text>();

        int num = 1;
        ScoreText.text = "";

        foreach(int score in GameData.gamedata.Score)
        {
            ScoreText.text += $"{num}位 ・ {score}点\n";
            num += 1;
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
