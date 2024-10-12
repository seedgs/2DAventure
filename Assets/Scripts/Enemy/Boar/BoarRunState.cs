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

        //currentEnemy.Run();
    }

    

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }

    
}
