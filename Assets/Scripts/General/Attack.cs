using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{


    public int trauma;

    public float attackRange;

    public float attackRate;


    //此方法为碰撞检测，当前两个物体进行碰撞
    public void OnTriggerStay2D(Collider2D collision)
    {
        //gameObject.GetComponent<Character>().enabled = true;
        //获得碰撞物体的组件（获得Character脚本），脚本内的takeTrauma的方法，（this）为传递当前数值（也就是attack）
        collision.GetComponent<Character>()?.takeTrauma(this);
        //?意思是相当于不为空的判断：!=null
        
    }


}