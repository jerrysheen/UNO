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
        
        // z 表示距离相机的距离
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.gameObject.transform.position.z));
        this.transform.position = worldPosition;

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
