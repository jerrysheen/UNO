using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 2;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, y, 0);
        movement = Vector3.ClampMagnitude(movement, 1);
        if (movement != Vector3.zero)
        {
            transform.Translate(movement * speed * Time.deltaTime);
            // send data to blend tree.
            animator.SetFloat("moveX", x);
            animator.SetFloat("moveY", y);
            animator.SetBool("isWalking", true);
            Debug.Log(x + " , " + y);  
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    
}
