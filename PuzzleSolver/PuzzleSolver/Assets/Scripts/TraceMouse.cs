using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TraceMouse : MonoBehaviour
{
    public bool hasBounds;
    public bool NeedFollow = true;
    public Vector3 min;
    public Vector3 max;
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
        if (NeedFollow)
        {
            RectTransform rectTrans = GetComponent<RectTransform>();
        
            // z 表示距离相机的距离
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.gameObject.transform.position.z));
            if (hasBounds)
            {

                    this.transform.position = new Vector3(Mathf.Clamp(worldPosition.x, min.x, max.x), Mathf.Clamp(worldPosition.y, min.y, max.y), Mathf.Clamp(worldPosition.z, min.z, max.z));
                    return;

            }
            this.transform.position = worldPosition;
        }

    }

    private void ClickScreen()
    {
        //this.transform.position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay ( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f) );
        //Debug.Log(ray);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection ( ray );
        if (Physics.Raycast(Camera.main.transform.position, ray.direction, 10000))
        {
            print("There is something in front of the object!");
        }

        if (hit2D.collider != null && Input.GetMouseButtonDown(0))
        {
            Debug.Log ("Ray tracing hit : " + hit2D.collider.name );
            hit2D.transform.gameObject.GetComponent<UIControllBase>()?.OnClicked();
        }
    }
}
