using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : EnemyController
{
    //override是可以修改父类方法
    public override void Move()
    {
        base.Move();
        anim.SetBool("isWalk",true);
    }
}
