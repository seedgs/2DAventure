using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{


    public override void OnEnter(EnemyController enemy)
    {
        //绑定传参，就可以通过currentEnemy调用EnemyController脚本的参数
        currentEnemy = enemy;

        currentEnemy.currentSpeed = currentEnemy.normalSpeed;

        currentEnemy.isWalk = true;

        //进入游戏后，敌人处于巡逻状态，
        //base.currentEnemy.Move();
        currentEnemy.anim.SetBool("isWalk", true);

        
    }

     
     
    public override void LogicUpdate()
    {

        //currentEnemy.anim.SetTrigger("Walk");
        currentEnemy.move();

        //当野猪的isGround为0，也就是野猪遇到悬崖的时候，野猪停止巡逻，进入等待模式
        //启动左边等待



        currentEnemy.anim.SetBool("isLeftGround", currentEnemy.isNotLeftWait);
        

        //启动右边等待
        currentEnemy.anim.SetBool("isRightGround", currentEnemy.isNotRightWait);
        currentEnemy.wait();

        currentEnemy.anim.SetBool("isWall", currentEnemy.isWall);

        //发现player 就切换到chase
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase); //当敌人发现了player就切换到chase状态
        }



    }




    public override void PhysicsUpdate()
    {
        
    }


    public override void OnExit(   )
    {
        currentEnemy.anim.SetBool("isWalk", false);
        currentEnemy.isWalk = false;
    }
}
