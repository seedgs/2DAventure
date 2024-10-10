using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Boar : EnemyController
{

    //这里的Awake是在EnemyController脚本的Awake执行之下，再执行这里的Awake
    public override void Awake()
    {
        base.Awake();

        //new一个野猪的巡逻模式出来，也可以理解为创建一个野猪巡逻模式
        patrolState = new BoarPatrolState();

        runState = new BoarRunState();
    }

    
}
