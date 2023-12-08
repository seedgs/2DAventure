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

        //检测是否在地面
        anim.SetBool("isGround", pc.IsGround);

        //检测是否死亡
        anim.SetBool("isDeath", pcl.isDeath);

        //检测是否攻击
        anim.SetBool("isAttack", pcl.isAttack);

        //检测是否爬墙
        anim.SetBool("isClimb", pcl.isClimb);
        
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

       
        

    }

    public void PlayerHurt()
    {
        anim.SetTrigger("Hurt");
    }

    //攻击动画
    public void PlayerAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayerClimb()
    {
        anim.SetTrigger("Climb");
    }

}
