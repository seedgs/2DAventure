using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage;

    public float attackRange;

    public float attackRate;



    private void OnTriggerStay2D(Collider2D collider)
    {
        //如果没有 ? 的话，unity终端会弹出下面的报错
        //NullReferenceException: Object reference not set to an instance of an object
        //Attack.OnTriggerStay2D(UnityEngine.Collider2D collider)
        
        //这里是获取碰撞体（也就是野猪）Character的组件内的TakeDamage的方法，因为传递TakeDamage本身的参数，（）内为this
        collider.GetComponent<Character>()?.TakeDamage(this);

    }

}
