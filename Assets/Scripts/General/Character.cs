using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;

    public float currentHealth;

    public float invulnerableDuration;

    private float invulnerableCounter;

    public bool invulnerable;

    public UnityEvent<Transform> OntakeDamage;

    public UnityEvent OnDead;

    //Awake()为一个生命周期只执行一次
    //如果人物重生，人物的buffer就会叠加，因为重生只有一次，所以叠buffer也就只有有一次




    //对象发生时候开始执行
    //这里是人物与野猪发生碰撞的时候开始执行
    //这个发生可以是多次
    private void Start()
    {
        //当前血量就是最大血量
        currentHealth = maxHealth;
    }


    //每一帧都需要执行这个计数器倒计时
    private void Update()
    {
        //当无敌的时候
        if (invulnerable)
        {
            //计数器开始倒计时
            invulnerableCounter -= Time.deltaTime;
            //当计数器为0的时候
            if (invulnerableCounter <= 0)
            {
                //无敌停止
                invulnerable = false;
            }
        }
    }

    //这里（）内是获取Attack脚本的参数，并命名为attacker
    public void TakeDamage(Attack attacker)
    {
       

        if (invulnerable)
            return;

        if (currentHealth > 0)
        {
            //Debug.Log(attacker.attackDamage);
            currentHealth -= attacker.attackDamage;
            OnTriggerInvlnerable();
            OntakeDamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //死亡
            OnDead?.Invoke();
        }
        
    }


    public void OnTriggerInvlnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

 
}
