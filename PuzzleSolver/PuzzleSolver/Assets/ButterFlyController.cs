using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyController : MonoBehaviour
{
    private Vector3 lastPos;
    private Vector3 lastScale;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = this.transform.position;
        lastScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        var currPos = this.transform.position;
        var velocity = currPos - lastPos;
        if (velocity.x > 0)
        {
            this.transform.localScale = new Vector3(-lastScale.x, lastScale.y, lastScale.z);
  //          Debug.Log("true");
        }
        else
        {
            this.transform.localScale = new Vector3(lastScale.x, lastScale.y, lastScale.z);
//            Debug.Log("false");

        }

        lastPos = currPos;
    }
}
