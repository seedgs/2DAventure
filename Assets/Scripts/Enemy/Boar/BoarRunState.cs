using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarRunState : BaseState
{

    public override void OnEnter(EnemyController enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        //base.currentEnemy.Run();
        currentEnemy.anim.SetBool("isRun", currentEnemy.isRun);
        currentEnemy.anim.SetTrigger("Run");
        
    }

    

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }

    
}
