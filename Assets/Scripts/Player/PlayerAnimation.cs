using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;

    private Rigidbody2D rb;

    private bool Atk;

    private bool Sd;

    private PhysicsCheck pc;

    private CapsuleCollider2D cc;

    private PlayerController pcl;


    //下蹲刚体数值
    #region
    //下蹲瞬间的刚体数值
    private Vector2 setOffsetDown = new(-0.0944f, 0.79f);
    private Vector2 setSizeDown = new(0.48678f, 1.57f);
    //起身瞬间的刚体数值
    private Vector2 setOffsetUp = new(-0.09440199f, 0.939046f);
    private Vector2 setSizeUp = new(0.4867882f, 1.878092f);
    #endregion

    public void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();
        Atk = false;
        cc = GetComponent<CapsuleCollider2D>();
        pcl = GetComponent<PlayerController>();
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

        anim.SetBool("isDeath", pcl.isDeath); 

        //下蹲动画
        #region
        if (Input.GetKeyDown(KeyCode.S))
        {
            Sd = true;

            //当下蹲瞬间，刚体轮廓的数值变换
            cc.offset = setOffsetDown;
            cc.size = setSizeDown;
            //Debug.Log("squatDown");
        }else if (Input.GetKeyUp(KeyCode.S))
        {
            Sd = false;

            //当起身瞬间，刚体轮廓的数值变换
            cc.offset = setOffsetUp;
            cc.size = setSizeUp;
        }
        if (Sd == false)
        {
            anim.SetBool("squatDown", false);
        }
        if (Sd == true)
        {
            anim.SetBool("squatDown", true);
        }
        #endregion

        //攻击动画
        #region
        if (Input.GetKeyDown(KeyCode.F))
        {
            //按下F按键，执行攻击动画
            Atk = true;
            //Debug.Log("Attack");
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

    public void playerHurt()
    {
        anim.SetTrigger("Hurt");
    }





}
