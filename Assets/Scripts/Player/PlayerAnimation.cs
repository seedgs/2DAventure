using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;


    private void Awake()
    {
        //最开始就要获取组件
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //每一帧都要执行
        setAnimation();
    }

    public void setAnimation()
    {
        //Velocity是Animator中自定义的参数
        //rb.velocity.x就是人物移动的时候刚体组件的横轴速度
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
    }
}
