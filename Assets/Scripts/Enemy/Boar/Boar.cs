using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Boar : EnemyController
{

    //�����Awake����EnemyController�ű���Awakeִ��֮�£���ִ�������Awake
    public override void Awake()
    {
        base.Awake();

        //newһ��Ұ���Ѳ��ģʽ������Ҳ�������Ϊ����һ��Ұ��Ѳ��ģʽ
        patrolState = new BoarPatrolState();

        //newһ��׷��ģʽ
        chaseState = new BoarChaseState();
    }

    
}
