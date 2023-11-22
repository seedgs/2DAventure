using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本参数")]
    public float maxHealth;

    public float currentHealth;

    [Header("受伤无敌")]
    //无敌持续时间
    //这个是可以手动输入的
    public float invulnerableDuration;

    //无敌时的计数器
    private float invulnerableCounter;

    //无敌的判断（布尔值）
    public bool invulnerable;

    private void Start()
    {
        //游戏一开始的时候玩家都是满血状态的
        currentHealth = maxHealth;
    }

    //Update每次检测
    private void Update()
    {
            //开始计数（倒计时）
            invulnerableCounter -= Time.deltaTime;
            //当倒计时为0的时候
            if (invulnerableCounter <= 0)
            {
                //无敌开关闭上（不打钩）
                invulnerable = false;
            }
    }

    //()里面Attack是Attack脚本的命名，attacker是自己命名的
    public void takeTrauma(Attack attacker)
    {
        //如果无敌的时候，不执行下面的扣血伤害
        //不执行的话就返回到Update上面由头执行
        //()为invulnerable = true
        if (invulnerable)
            return;


        //打印Attack(这里已用attacker承载)的trauma的数值
        //Debug.Log(attacker.trauma);

        //当人物受伤后，数值不为负，且不为0，可以继续执行扣血处理
        if (currentHealth - attacker.trauma > 0)
        {
            //血量等于两个物体碰撞后，当前血量 - 收到的伤害的数值 
            currentHealth -= attacker.trauma;

            //当上面扣血的时候，执行方法 TriggerInvulnerAble()
            TriggerInvulnerAble();
        }
        //否则，人物血量直接为0
        else
        {
            //人物死亡
            currentHealth = 0;
        }

 
    }


    private void TriggerInvulnerAble()
    {
            //无敌的布尔值打开（打钩）
            invulnerable = true;

            //无敌的持续时间 等于 认为设定的无敌时间
            invulnerableCounter = invulnerableDuration;
    }


}
