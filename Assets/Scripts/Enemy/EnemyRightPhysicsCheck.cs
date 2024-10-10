using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightPhysicsCheck : MonoBehaviour
{
    [Header("右碰撞圆心偏移量")]
    public Vector2 RightOffset;

    [Header("右碰撞半径")]
    public float CheckRightRadius;

    [Header("右碰撞半径")]
    public LayerMask GroundRightlayer;

    [Header("右碰撞判定")]
    public bool isRightGround;

    private void Update()
    {
        EnemyRightCheck();
    }

    public void EnemyRightCheck()
    {
        //地面检测
        isRightGround = Physics2D.OverlapCircle((Vector2)transform.position + RightOffset, CheckRightRadius, GroundRightlayer);
    }

    //绘制碰撞半径方法
    private void OnDrawGizmos()
    {
        //DrawWireSphere是绘制一个圆（半径，半径长度）
        Gizmos.DrawWireSphere((Vector2)transform.position + RightOffset, CheckRightRadius);

    }
}
