using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftPhysicsCheck : MonoBehaviour
{
    [Header("左碰撞圆心偏移量")]
    public Vector2 LeftOffset;

    [Header("左碰撞半径")]
    public float CheckLeftRadius;

    [Header("选择碰撞图层")]
    public LayerMask GroundLeftlayer;

    [Header("左碰撞判定")]
    public bool isLeftGround;

    private void Update()
    {
        EnemyLeftCheck();
    }

    public void EnemyLeftCheck()
    {
        //地面检测
        isLeftGround = Physics2D.OverlapCircle((Vector2)transform.position + LeftOffset, CheckLeftRadius, GroundLeftlayer);
    }

    //绘制碰撞半径方法
    private void OnDrawGizmos()
    {
        //DrawWireSphere是绘制一个圆（半径，半径长度）
        Gizmos.DrawWireSphere((Vector2)transform.position + LeftOffset, CheckLeftRadius);

    }
}
