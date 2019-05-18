using UnityEngine;

public class MouseMove : MonoBehaviour
{
    GameObject lineManeger;

    private void Start()
    {
        lineManeger = GameObject.Find("LineManeger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pointer")
        {
            if (lineManeger.GetComponent<LineGenerator>().firstPoint == null)
            {
                lineManeger.GetComponent<LineGenerator>().firstPoint = collision.gameObject;
            }
            else if(lineManeger.GetComponent<LineGenerator>().firstPoint != collision.gameObject)
            {
                lineManeger.GetComponent<LineGenerator>().endPoint = collision.gameObject;
            }
        }
    }
}
