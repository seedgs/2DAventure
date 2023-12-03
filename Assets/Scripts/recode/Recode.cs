using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recode : MonoBehaviour
{
    //2.10人物属性及伤害计算

    //1、切割图片（野猪）
    //1-1、注意图片的尺寸、注意裁剪图片的大小、注意图片焦点位置

    //2、野猪绑定刚体组件（Rigidbody 2D）、盒子碰撞（Box Collider 2D）、胶囊碰撞（Capsule Collider 2D）、
    //2-1、刚体z轴锁定、Collision Detection（连续选择）
    //2-2、盒子碰撞“横向”编辑选择、排除敌人图层、人物图层（建立这两个图层），不要勾选碰撞选项，否则贵掉出场景
    //2-3、胶囊碰撞勾选碰撞选项


    //3、创建Attack脚本，创建参数：攻击伤害（AttackDamage）、攻击范围（AttackRange）、攻击频率（AttackRate）
    //3-1、以检测方法为基础（OnTriggerStay2D‘保持碰撞状态’），获取碰撞体的参数（Character）脚本内的方法（受伤方法‘TakeDamage’）

    //4、创建Character脚本，创建参数：创建最大血量（maxHealth）、当前血量（currentHealth）、无敌持续时间（invulnerableDuration）、无敌计数器（invulnerableCounter）、无敌的布尔值（invulnerable）
    //4-1、创建受伤方法（TakeDamage），方法内传参为Attack的参数
    //4-1-1、人物需要挂载Chararcter脚本，设定人物最大血量数值
    //4-1-2、野猪需要挂载Attack与Character脚本，设定最大血量数值、攻击伤害数值
    //4-2、回到Character脚本，首先在TakeDamage方法内，Debug.Log一下Attack内的attackDamage是否传参正确
    //4-3、传参正确后，当人物碰到野猪的时候，需要执行扣血行为，所以TakeDamage方法内，当前血量 = 最大血量 - 攻击伤害
    //4-4、人物一开始（State）的时候，当前血量 = 最大血量
    //4-5、人物不能一碰撞就多次扣血、需要规定时间内的无敌时间，所以需要创建一个OnTriggerInvlnerable方法
    //4-6、当人物扣血的时候，无敌的布尔值需要打开，无敌持续时间 = 无敌计数器（开始计数）
    //4-7、TakeDamage方法内，如果人物不扣血的时候，不执行往下的代码（return返回），否则执行扣血计算，且执行无敌的方法
    //4-8、每一次（start）人物发生碰撞的时候，都需要执行计数器倒计时计算，如果计数器时间被倒数到0的时候，无敌停止
    //4-9、人物在血量不足以承受伤害的时候，当前血量就为0，人物血量大于0的时候可以执行扣血计算




    //2.12.三段攻击动画的实现
    //创建攻击动画（Animation里面创建）
    //创建新的动画层Layers，命名为Attack Layer（攻击状态的动画层）
    //在Attack Layer里创建新的空状态
    //把三段动画层拖进Layers控制面板内
    //创建isAttack布尔值，为了检测攻击动画是否执行
    //创建Attack的Trigger
    //创建comBoo的int，为了计数
    //空状态指向Attack01动画，绑定上面创建的数值
    //Attack01动画指向Attack02动画，绑定上面创建的数值
    //Attack02动画指向Attack03动画，绑定上面创建的数值
    //创建控制的按键

    //PlayerController脚本创建关联攻击按键的代码,命名为Attack
    //PlayerController脚本创建这个Attack的函数方法（这个方法最好系统生成，因为需要传参数，可以参考Jump()方法）
    //Attack()方法内，关联PlayerAnimation脚本的PlayerAttack()方法（此时暂时无法关联，因为还没有创建，同时PlayerController脚本还没有关联PlayerAnimation脚本，所以需要GetComponent<>一下）

    //PlayerAnimation脚本内创建PlayerAttack()方法
    //PlayerAttack()方法内关联在Animation动画层Layer内的Attack的trigger

    //回到PlayerController脚本内，关联PlayerAnimation脚本的PlayerAttack()方法（此时就可以关联了）
    //PlayerController脚本内,创建isAttack布尔值
    //PlayerAttack()方法内，当按下攻击按键的时候，isAttack（布尔值）要打开
    //PlayerController脚本内,创建Combo的整数（int）值
    //PlayerController脚本创建isAttack布尔值，创建comBo（int）数值
    //comBoo需要自行递增
    //当Comboo数大于3时comBoo为0，为了还原数值，循环执行攻击状态

    //回到PlayerAnimation脚本，需要关联PlayerController脚本创建的isAttack、comBo相关数值（也就是anim的关联）


}
