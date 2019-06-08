using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{
    GameObject _title;

    static public int game_select;



    public void Start()
    {
        _title = GameObject.Find("Title");

    }


    public void TopButton()
    {
       Debug.Log("topclick!");

    }




    public void LevelSelect_Easy()
    {

        Debug.Log("EasyClick!");
        game_select = game_select + 0;

    }
    public void LevelSelect_Nomal()
    {

        Debug.Log("NomalClick!");
        game_select = game_select + 1;
    }




    public void MoodSelect_Challenge()
    {

        Debug.Log("Challenge!");
        game_select = game_select + 10;
    }
    public void MoodSelect_Time()
    {

        Debug.Log("Time!");
        game_select = game_select + 20;
    }

    public void GameStart()
    {
        Debug.Log("Start");


    }

 



    //static public Title instance;
    //public int hoge;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //public void Hoge()
    //{
    //    Debug.Log("Hoge");
    //}
}

