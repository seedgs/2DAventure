using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("碰撞圆心偏移量")]
    public Vector2 Offset;

    [Header("碰撞半径")]
    public float CheckRadius;

    public LayerMask Groundlayer;

    [Header("碰撞判定")]
    public bool IsGround;

    private void Update(){
        Check();
    }

    public void Check(){
        //地面检测
        IsGround = Physics2D.OverlapCircle((Vector2)transform.position+Offset, CheckRadius, Groundlayer);
    }

    //绘制碰撞半径方法
    private void OnDrawGizmos()
    {
        //DrawWireSphere是绘制一个圆（半径，半径长度）
        Gizmos.DrawWireSphere((Vector2)transform.position + Offset, CheckRadius);
    }
	
}
