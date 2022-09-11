using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 2;
    public Animator animator;

    private Rigidbody2D m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 velocity = movement * speed * Time.deltaTime;
        movement = Vector2.ClampMagnitude(movement, 1);
        if (movement != Vector2.zero)
        {
            m_Rigidbody.MovePosition(pos + velocity);
            animator.SetFloat("moveX", x);
            animator.SetFloat("moveY", y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.name);
    }
}
