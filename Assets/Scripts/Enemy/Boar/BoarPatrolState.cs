using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{


    public override void OnEnter(EnemyController enemy)
    {
        //绑定传参，就可以通过currentEnemy调用EnemyController脚本的参数
        currentEnemy = enemy;
        
    }

     
     
    public override void LogicUpdate()
    {
        //进入游戏后，敌人处于巡逻状态，一旦发现玩家就进入追击（奔跑）状态
        //base.currentEnemy.Move();
        currentEnemy.anim.SetBool("isWalk", currentEnemy.isWalk);
        currentEnemy.anim.SetTrigger("Walk");
        currentEnemy.move();

        //当野猪的isGround为0，也就是野猪遇到悬崖的时候，野猪停止巡逻，进入等待模式
        //启动左边等待
        
        currentEnemy.anim.SetBool("isLeftGround", currentEnemy.isNotLeftWait);
        currentEnemy.wait();


        //启动右边等待
        currentEnemy.anim.SetBool("isRightGround", currentEnemy.isNotRightWait);
        currentEnemy.wait();


        //启动奔跑动画
        currentEnemy.anim.SetBool("isRun", currentEnemy.isRun);
        currentEnemy.Run();
    }




    public override void PhysicsUpdate()
    {
        
    }


    public override void OnExit()
    {
        
    }
}
