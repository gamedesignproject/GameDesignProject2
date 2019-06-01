using UnityEngine;

public class Point : MonoBehaviour
{
    // ポインタの番号
    [SerializeField] private int pointNum;

    [SerializeField] private LineGenerator lineGenerator;

    public int PointNum
    {
        get { return pointNum; }
    }
    
    public void OnClick()
    {
        Debug.Log("pointClick");
        if(lineGenerator.state == LineGenerator.STATE.NONE)
        {
            lineGenerator.AddLineObject(this.transform.position);
        }
    }
    
}
