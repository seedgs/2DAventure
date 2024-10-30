using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarChaseState : BaseState
{

    public override void OnEnter(EnemyController enemy)
    {
        currentEnemy = enemy;


        currentEnemy.isRun = true;



        //野猪速度提升
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;





        //启动奔跑动画
        //一旦发现玩家就进入追击（奔跑）状态
        currentEnemy.anim.SetBool("isRun", true);
        //currentEnemy.Run();
        //currentEnemy.lostPlayer();




    }
    public override void LogicUpdate()
    {

        //进入奔跑状态，等待时间为0
        currentEnemy.CurrentWaitTime = 0;

        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }

        if (currentEnemy.collisionLeft)
        {
           currentEnemy.transform.localScale = new Vector3(-1, 1, 1);

           currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        }

        if (currentEnemy.collisionRight)
        {
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);

            currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        }
    }



    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;

        currentEnemy.anim.SetBool("isRun", false);

        currentEnemy.isRun = false;

        currentEnemy.isWall = false;
    }
}
