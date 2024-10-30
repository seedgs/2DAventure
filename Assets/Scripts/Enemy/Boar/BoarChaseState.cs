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



        //Ұ���ٶ�����
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;





        //�������ܶ���
        //һ��������Ҿͽ���׷�������ܣ�״̬
        currentEnemy.anim.SetBool("isRun", true);
        //currentEnemy.Run();
        //currentEnemy.lostPlayer();




    }
    public override void LogicUpdate()
    {

        //���뱼��״̬���ȴ�ʱ��Ϊ0
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
