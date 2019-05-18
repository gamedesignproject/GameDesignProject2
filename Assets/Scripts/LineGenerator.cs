using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    LineRenderer line;
    public GameObject firstPoint;
    public GameObject endPoint;

    void Start()
    {
        /**
        firstPoint = GameObject.Find("Pointer");
        endPoint = GameObject.Find("Pointer (1)");
        /**/

        line = GetComponent<LineRenderer>();

        //線の幅を決める
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        //頂点の数を決める
        line.positionCount = 2;
    }
    
    void Update()
    {
        if (firstPoint = null)
        {
            line.SetPosition(0, firstPoint.transform.position);
            line.SetPosition(1, endPoint.transform.position);
        }
    }


}
