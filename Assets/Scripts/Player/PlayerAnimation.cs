using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;

    private Rigidbody2D rb;

    public bool Atk;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Atk = false;
    }

    private void Update()
    {
        setanimation();
        
    }

    public void setanimation()
    {
        //跑步动画
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        //攻击动画
        if (Input.GetKey(KeyCode.F))
        {
            //按下F按键，执行攻击动画
            Atk = true;
            //Debug.Log("1");
        }
        else 
        {
            //抬起F按键，结束攻击动画
            Atk = false;
        }


        if (Atk == false)
        {
            anim.SetBool("Attack", false);
        }
        if (Atk == true)
        {
            anim.SetBool("Attack", true);
        }


    }
}
