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



    //2.11受伤和死亡的逻辑和动画
    //1、切图受伤与死亡的图片
    //2、创建新的场景动画，挂载受伤动画
    //3、创建受伤“Hurt”的trigger条件





    //2.11受伤和死亡的逻辑和动画
    //1、创建受伤动画
    //2、创建死亡动画
    //3、创建动画逻辑，玩家受伤后，会有闪烁、变红的状态
    //4、创建叠加层：Additive在之前层的基础上叠加动画状态
    //5、创建空的状态：add property会有各种状态可加
    //6、添加Trigger触发器：hurt
    //7、playerAnimation脚本创建新的方法（playHurt）
    //8、Character脚本：当人物受伤后，执行playHurt()方法
    //9、Character脚本：调用命名空间，开头处插入UnityEngine.Event
    //10、创建public unityEvent()：因为当人物受伤时，会收到一个反方向的力，也就是人物会被弹开，人物弹开理解为人物位移，人物位移需要用到transform，所以< >内是transform，并命名为OntakeDamage
    //11、当人物受到伤害时，需要执行PlayerAnimation脚本的动画代码，所以Event事件首先导入PlayerAnimation脚本，然后下拉选择palyerAnimation脚本的palyHurt()方法
    //12、？是检测是否为null，Invoke()是启动，谁攻击我，我就在()内传递睡的transform（当前是野猪的transform）
    //13、动画挂载受伤的动画
    //14、在 playerController脚本内创建一个受伤反弹的效果
    //15、创建 GetHurt()方法，之前传递了transform的参数，所以()内要传递野猪的transform
    //16、创建布尔变量IsHurt
    //17、创建 float hurtForce反弹力的变量
    //18、创建 GetHurt()方法（记得传参数）
    //19、布尔开关 isHurt打开
    //20、速度停下来！！！
    //21、创建一个 dir变量（Vector2）
    //22、当前人物的坐标 减去 碰撞物体的坐标，与防线保持不变
    //23、因为人物离碰撞体很远的时候，得到的数值会很大，反弹力为这个数值，会导致人物被弹开很远，所以需要一个.normalized来归 1 化（总量载0-1之间）
    //24、添加瞬时的力，可以参照Jump()方法来写
    //25、当人物受伤的时候，是不能被操控的
    //26、把写好的函数方法添加进入事件中
    //27、Event事件导入playerAnimation脚本的GetHurt()方法中
    //28、反弹力要设置参数

    //29、问题：IsHurt勾选后就无法被取消了

    //30、此时需要运用Unity为我们写好的动画方法，这个方法可以帮助我们载每一个动画状态中插入想要的条件
    //31、回到动画控制器中，载受伤动画下添加一个“HurtAnimation”脚本
    //32、把“HurtAnimation”脚本放进Play文件夹中
    //33、找到playerController脚本内的IsHurt布尔值参数，改成false即可
    //34、添加一个IsDead布尔参数，告诉我们玩家是否进入死亡的状态
    //35、状态：我玩家随时可以死亡
    //36、在Character脚本内，创建死亡的 Event 事件（不需要传参），并命名为OnDead
    //37、当CurrentHealth = 0时，就启动死亡机制
    //38、playerController脚本内，创建IsDead布尔值
    //39、创建PlayDead()方法
    //40、确认IsDead布尔被勾选
    //41、把控制人物的方法禁止！！！
    //42、最后在playerAnimation脚本内，把动画布尔与playerController脚本内的 IsDead布尔相关联
    //43、死亡事件导入playerDead()方法即可
    //44、因为人物死亡动画会循环播放的缘故，需要在所在动画里取消勾选 Loop time（循环时间）框




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

    //PlayerController脚本创建isAttack布尔值，创建comBoo（int）数值
    //PlayerController脚本创建关联控制按键的函数，并命名为PlayerAttack

    //PlayerAnimation脚本创建PlayerAttack()方法
    //方法内关联在Animation动画层Layer内的Attack的trigger

    //创建PlayerAttack()方法
    //方法内需要判定isAttack是否打开
    //执行PlayerAnimation脚本内，PlayerAttack()方法
    //comBoo需要自行递增
    //当Comboo数大于3时comBoo为0，为了还原数值，循环执行攻击状态

    
}
