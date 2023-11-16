using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private PhysicsCheck pc;

    private PlayerController pcl;

    private void Awake()
    {
        //获取Animator(动画)组件
        anim = GetComponent<Animator>();

        //获取Rigidbody2D(刚体)组件
        rb = GetComponent<Rigidbody2D>();

        //获取PhysicsCheck脚本组件
        pc = GetComponent<PhysicsCheck>();

        //获取PlayerController脚本组件
        pcl = GetComponent<PlayerController>();
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

        //Velocity是Animator中自定义的参数
        //rb.velocity.y就是人物移动的时候刚体组件的纵轴速度
        anim.SetFloat("VelocityY", rb.velocity.y);

        //IsGround是检测刚体是触碰地面的布尔值，为true表示人物在地面上，为false表示人物不在地面上
        //不在在地面上的时候动画播放
        anim.SetBool("IsGround", pc.isGround);

        //下蹲动画控制
        anim.SetBool("IsCrouch", pcl.IsCrouch);
    }
}
