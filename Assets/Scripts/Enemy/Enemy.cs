using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;

    protected Rigidbody2D rb;



    [Header("»ù´¡¹¦ÄÜ")]
    public float normalSpeed;

    public float chaseSpeed;

    public float currentSpeed;

    public Vector3 facdir;

    private void Update()
    {
        facdir = new Vector3(-transform.localScale.x, 0, 0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        currentSpeed = normalSpeed;
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(facdir.x * currentSpeed * Time.deltaTime, 0);
    }


}
