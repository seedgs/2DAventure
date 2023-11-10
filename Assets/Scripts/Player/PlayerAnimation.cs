using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;

    private Rigidbody2D rb;

    public bool Atk;

    private PhysicsCheck pc;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();
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

        //跳跃动画
        anim.SetFloat("VelocityY", rb.velocity.y);

        anim.SetBool("isGround", pc.IsGround);

        //攻击动画
        #region
        if (Input.GetKeyDown(KeyCode.F))
        {
            //按下F按键，执行攻击动画
            Atk = true;
            //Debug.Log("1");
        }
        else if(Input.GetKeyUp(KeyCode.F))
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
        #endregion

    }
}
