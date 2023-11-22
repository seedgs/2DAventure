using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    //2.10人物属性及伤害计算

    //1、切割图片（野猪）
    //2、野猪绑定刚体、碰撞
    //3、碰撞体设置（人物、野猪）
    //4、添加character（数值）、attack（攻击）脚本
    //5、attack：创建受伤（damage）、攻击范围（attackRanger）、攻击频率（attackRate），利用检测碰撞机制（当物体碰撞的时候，会得到另外一个物体的参数），获得另外一个物体的组件参数（该参数就是charater脚本的受伤的计算方法）
    //6、charater脚本：创建最大血量（maxHealth）、当前血量（currentHealth），创建受伤计算的方法（takeDamage()）
    //7、在charater脚本创建无敌的方法：创建无敌持续时间（invulnerableDuration）、无敌计数器（invulnerableCounter）、无敌的布尔值（invulnerable）
    //8、创建碰撞无敌的方法（TriggerInvulnerable()），当人物碰到野猪的时候，此时开始执行扣血操作，且进入无敌碰撞的方法，无敌布尔开关打开，系统开始计数（倒计时）；没有碰撞野猪的时候，不执行扣血的方法；
    //9、当无敌的时候，马上执行倒记时操作，当倒计时小于等于0的时候，无敌布尔值开关关闭（每次倒计时都需要每一帧检测调用，因为碰撞时时刻都存在的）
    //10、每次碰撞扣血都会出现负数的情况

}
