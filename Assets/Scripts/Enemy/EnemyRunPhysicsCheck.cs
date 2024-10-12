using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunPhysicsCheck : MonoBehaviour
{
    [Header("碰撞圆心偏移量")]
    public Vector2 RunOffset;

    [Header("碰撞半径")]
    public float CheckRunRadius;

    [Header("选择碰撞图层")]
    public LayerMask Runlayer;

    [Header("碰撞判定")]
    public bool isRunCheck;

    private void Update()
    {
        EnemyRunCheck();
    }

    public void EnemyRunCheck()
    {
        //地面检测
        isRunCheck = Physics2D.OverlapCircle((Vector2)transform.position + RunOffset, CheckRunRadius, Runlayer);
    }

    //绘制碰撞半径方法
    private void OnDrawGizmos()
    {
        //DrawWireSphere是绘制一个圆（半径，半径长度）
        Gizmos.DrawWireSphere((Vector2)transform.position + RunOffset, CheckRunRadius);
    }
}
