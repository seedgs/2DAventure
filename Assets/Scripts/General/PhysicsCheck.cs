using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    public Vector2 bottomOffset;

    public float checkRaduis;

    public LayerMask groundLayer;

    public bool isGround;

    private void Update()
    {
        Check();
    }

    public void Check()
    {
        //检测地面
       isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis,groundLayer);
      
    }

    //绘制方法
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
