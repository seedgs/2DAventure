using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Animator anim;

    protected Rigidbody2D rb;

    [Header("基础信息")]
    public float normalSpeed;

    public float currentSpeed;

    public float chaseSpeed;

    public Vector3 faceDir;
    
    private void Update()
    {
        //把野猪的朝向修正
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        currentSpeed = normalSpeed;
    }

    public void FixedUpdate()
    {
        Move();
    }

    //virtual是可以允许子类修改此方法
    public virtual void Move()
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, 0);
    }

    
    
}
