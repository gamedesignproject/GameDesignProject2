using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    static Title _TopCanvas;
    void Start()
    {
      //保持
        _TopCanvas = GetComponent<Title>();
    }

    /// 表示・非表示を設定する
    public static void SetActive(string name, bool b)
    {
        foreach (Transform child in _TopCanvas.transform)
        {
           
            if (child.name == name)
            {

                child.gameObject.SetActive(b);
         
                return;
            }
        }
        //見つからない場合
        Debug.LogWarning("Not found objname:" + name);
    }
}