using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : EnemyController
{
    

    //override是可以修改父类方法
    //野猪的移动
    public override void boarMove()
    {
        base.boarMove();
        anim.SetBool("isWalk",isWalk);
        anim.SetTrigger("Walk"); 
    }

    //当人物 “进入” 野猪范围（指的是野猪 “前面” 的范围时）野猪开始奔跑
    public override void boarRun()
    {
        base.boarRun();
        anim.SetBool("isRun", isRun);
        anim.SetTrigger("Run");
    }

    //当人物 “离开” 野猪范围（指的是野猪 “背后” 的范围时）野猪停止奔跑
    public override void boarNoRun()
    {
        base.boarNoRun();
        anim.SetBool("isRun", false);
    }

    //方法注释
    #region
    //当野猪与墙面发生碰撞时
    //根据墙面碰撞的左右来判断野猪转向
    #endregion
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Bg_Rock_Left")
        {
            transform.localScale = new Vector3(-1, 1, 1);
            ////野猪退后
            //isRetreat = false;
        }

        if (collision.name == "Bg_Rock_Right")
        {
            transform.localScale = new Vector3(1, 1, 1);
            ////野猪退后
            //isRetreat = false;
        }

    }




    ////野猪退后
    //public override void boarAgainAttack()
    //{
    //    base.boarAgainAttack();
    //    anim.SetBool("isRetreat", isRetreat);
    //    anim.SetTrigger("Retreat");
    //}



}

