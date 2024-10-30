using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{


    public override void OnEnter(EnemyController enemy)
    {
        //�󶨴��Σ��Ϳ���ͨ��currentEnemy����EnemyController�ű��Ĳ���
        currentEnemy = enemy;

        currentEnemy.currentSpeed = currentEnemy.normalSpeed;

        currentEnemy.isWalk = true;

        //������Ϸ�󣬵��˴���Ѳ��״̬��
        //base.currentEnemy.Move();
        currentEnemy.anim.SetBool("isWalk", true);

        
    }

     
     
    public override void LogicUpdate()
    {

        //currentEnemy.anim.SetTrigger("Walk");
        currentEnemy.move();

        //��Ұ���isGroundΪ0��Ҳ����Ұ���������µ�ʱ��Ұ��ֹͣѲ�ߣ�����ȴ�ģʽ
        //������ߵȴ�



        currentEnemy.anim.SetBool("isLeftGround", currentEnemy.isNotLeftWait);
        

        //�����ұߵȴ�
        currentEnemy.anim.SetBool("isRightGround", currentEnemy.isNotRightWait);
        currentEnemy.wait();

        currentEnemy.anim.SetBool("isWall", currentEnemy.isWall);

        //����player ���л���chase
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase); //�����˷�����player���л���chase״̬
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
