using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    public float speed;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        setanimation();
    }

    public void setanimation()
    {
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
    }
}
