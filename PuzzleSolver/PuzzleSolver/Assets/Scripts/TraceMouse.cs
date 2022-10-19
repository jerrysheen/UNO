using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TraceMouse : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ClickScreen();
        FollowMouse();
    }

    private void FollowMouse()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();
        this.transform.position = Input.mousePosition;
        // Vector2 pos;
        // GameObject myCanvas = GameObject.Find("Canvas");
        // Camera cam = Camera.current;;
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, cam, out pos);
        // transform.position = myCanvas.transform.TransformPoint(pos);
    }

    private void ClickScreen()
    {
        //this.transform.position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay ( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f) );
        //Debug.Log(ray);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection ( ray );

        if (hit2D.collider != null && Input.GetMouseButtonDown(0))
        {
            Debug.Log ("Ray tracing hit : " + hit2D.collider.name );
            hit2D.transform.gameObject.GetComponent<UIControllBase>()?.OnClicked();
        }
    }
}
