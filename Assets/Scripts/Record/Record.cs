using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
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


//——————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————


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


//——————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————


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


//——————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————


    //2.13实装攻击判定
    //在Unity编辑器内的Player下创建一个新的“creat empty”，命名为“Attack Aera”
    //在“Attack Area”内再创建三个新的“creat empty”，分别命名为“Attack01”、“Attack02”、“Attack03”
    //在Unity-Animaion，选择PlayerAttack01，一定要对应“Attack01”，选择PlayerAttack02，一定要对应“Attack02”，选择PlayerAttack03，一定要对应“Attack03”
    //举例：选择PlayerAttack01，对应“Attack01”下，在Attack01添加“Polygon Collider 2D”
    //选择攻击判定的范围（Edit Collider）
    //在人物攻击动画（3个攻击动画），的剑气范围去选定判定范围
    //（注意！只在Attack01、Attack02、Attack03层添加攻击判定层，人物的其他层不需要）
    //在Unity面板-Animation-Add Property中为分别以一一对应Attack01、Attack02、Attack03层添加Game Object Active
    //（注意！添加Game Object Active 要一一对应每个层，举例在Attack01子集里面添加Game Object Active）
    //添加Game Object Active后，需要在Attack动画的每一帧上检查，有剑气出现的动画，才需要显示Game Object Active，否则不需要显示

    //region

    //is Trigger一定要要勾选（否则无法产生碰撞）
    //在Layer Overrides-contack Caputure Layers选择取消选择nothing，选择Enemy（这个操作是因为认为在建立攻击范围的时候，会砍到自身从而造成自身扣血的状况，选择Enemy后，就只会对Enemy层的物体造成伤害）
    //以上操作在Attack01、Attack02、Attack03层都需要

    //endregion

    //在Boar层的Character上的Invulnerable Druation上设定Boar的无敌时间
    //运行查看Boar是否有扣血
    //当持续按下攻击按键的时候，人物会移动
    //在PlayerController脚本的FixedUpdate()方法内，修改如果按下攻击按键，人物就不能移动（通过isAttack布尔值来写）
    //再次运行，人物攻击是否会移动
    //如果人物在攻击的时候一闪一闪的移动的话，在Attack01、Attack02、Attack03挂载动画状态的脚本，就是说在人物攻击动画开始的时候，isAttack布尔值为打开状态；人物攻击动画结束的时候，isAttack布尔值为关闭状态
    //再次运行，人物攻击是否会移动
    //如果还是有那么一点点的移动，可以考虑在PlayerController脚本内的攻击方法（ PlayerAttack() ）,添加代码（当人物按下攻击后，人物的Velocity的x数值为0，
    //当然Velocity需要x，y都要设置，只要x为0，y保持原来的数值即可）
    

}
