using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{


    // Start is called before the first frame update
    //测试测试测试测试测试
    void Start()
    {
        
    }


    //方法注释
    #region
    //与FixedUpdate()一样都是每帧更新，
    //Update()是平均帧率更新（系统设置）
    //FixedUpdate()是固定帧更新（人为设置）
    #endregion
    void Update()
    {

        //获取inputControl里面的 GamePalyer 里面的 Move 的 Vector2 存进inputDirection，但是这个Vector2需要ReadValue

        inputDirection =inputControl.GamePlayer.Move.ReadValue<Vector2>();
        CheckMaterial();

        //当触地面的时候，滑落效果停止
        if (physicsCheck.IsGround)
            isClimb = false;

        if (isClimb)
        {
            //当滑落效果开始时，不允许人物左右操作
            inputControl.GamePlayer.Move.Disable();
            Debug.Log("Not Move");

            //当人物碰墙的时候，添加一个向上的力（摩擦力）
            Vector2 DownForce = new Vector2(0, climbForce);
            //Debug.Log(transform.position.y);
            rb.AddForce(DownForce/100, ForceMode2D.Impulse);

        }
        else if (physicsCheck.IsGround && !isDeath)
        {
            //当接触地面时,且不是死亡的时候，允许人物左右移动
            inputControl.GamePlayer.Move.Enable();
            //Debug.Log("Move");
        }

        //开启滑步方法
        CheckGlissade();

        //当滑铲结束的时候，恢复野猪Capsule Collider 2D组件显示
        if (!isGlissade)
        {
            //Boar.GetComponentInChildren<CapsuleCollider2D>().enabled = true; 
        }

        
    }


    //方法
    #region
    private PhysicsCheck physicsCheck;

    //获取 PlayerInputControl(输入设备)，存进inputControl
    //PlayerInputControl在Seetings文件夹里面的InputSystem文件里面
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;

    //获取PlayerAnimation脚本组件
    private PlayerAnimation PlayerAnimation;

    //加载刚体
    public Rigidbody2D rb;


    //加载“灵巧”渲染器
    public SpriteRenderer sp;

    private CapsuleCollider2D cc2;

    public GameObject Boar;



    #endregion



    //title命名
    [Header("基本参数")]

    #region
    //速度
    public float speed;

    //奔跑速度
    private float runSpeed;

    //行走速度
    private float walkSpeed;

    //当人物起跳时，添加一个瞬时向上的阻力
    public float jumpForce;

    //当人物受伤时，添加一个瞬时横向的弹力
    public float hurtForce;

    //当人物滑墙的时候，添加一个向上的阻力
    public float climbForce;

    //当人物滑铲的时候，添加一个向前的力
    public float glissadeForce;

    //检测受伤的布尔值
    public bool isHurt;

    //检测死亡的布尔值
    public bool isDeath;

    //创建攻击布尔值
    public bool isAttack;

    //爬墙布尔值
    public bool isClimb;

    //滑步的布尔值
    public bool isGlissade;



    [Header("物理材质")]

    public PhysicsMaterial2D Normal;

    public PhysicsMaterial2D Rock;

    #endregion



    #region
    //滑步参数
    private float LeftTimer = 0;

    private float RightTimer = 0;

    //enum为枚举方法
    public enum clickRightCount
    {
        //第一个参数默认为 firstRightTime = 0，第二个参数 econdRightTime = 1，以此类推
        firstRightTime,
        secondRightTime,
        zeroRightTime,
    }

    public enum clickLeftCount
    {
        firstLefttime,
        secondLefttime,
        zeroLeftTime,
    }

    clickRightCount Right = clickRightCount.zeroRightTime;

    clickLeftCount Left = clickLeftCount.zeroLeftTime;

    #endregion


    [Header("事件")]
    public UnityEvent OffTakeTruma;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        physicsCheck = GetComponent<PhysicsCheck>();

        PlayerAnimation = GetComponent<PlayerAnimation>();

        cc2 = GetComponent<CapsuleCollider2D>();


        inputControl = new PlayerInputControl();
        //started那就按下那一刻
        //把Jump这个函数方法添加到你按键按下的按键按下的那一刻（started）里面执行
        inputControl.GamePlayer.Jump.started += Jump;


        //攻击判定
        #region

        inputControl.GamePlayer.Attack.started += PlayerAttack;

        #endregion

        //蹲下判定
        #region

        inputControl.GamePlayer.SquatDown.performed += ctx =>
        {
            speed = 0f;
        };
        inputControl.GamePlayer.SquatDown.canceled += ctx =>
        {
            speed = 290f;
        };
        #endregion


        //走路与跑步切换
        #region
        //走路为速度时的约1/2.5倍
        walkSpeed = speed / 2.5f;

        //跑步就是速度的数值
        runSpeed = speed;

        //方法注释
        #region
        //获取控制中控制走路的组件，在“按下”(performed)的时候,调用回调函数(+= ctx =>)
        //回调函数检测人物碰撞地面后开始执行
        //“按下”后为走路模式
        #endregion
        inputControl.GamePlayer.Walkbutton.performed += ctx =>
        {
            if (physicsCheck.IsGround)
            {
                speed = walkSpeed;
            }
        };

        //方法注释
        #region
        //获取控制中控制走路的组件，在“松开”(canceled)的时候,调用回调函数(+= ctx =>)
        //回调函数检测人物碰撞地面后开始执行
        //“松开”后为跑步模式
        #endregion
        inputControl.GamePlayer.Walkbutton.canceled += ctx =>
        {
            if (physicsCheck.IsGround)
            {
                speed = runSpeed;
            }
        };
        #endregion
    }


    //跳跃方法

    //当前物体启动的时候
    private void OnEnable()
    {
        //控制器也启动起来
        inputControl.Enable();
    }


    //当前物体关闭的时候
    private void OnDisable()
    {
        //控制器也跟着关闭
        inputControl.Disable();
    }

    //方法注释
    #region
    //与Update()一样都是每帧更新，
    //Update()是平均帧率更新（系统设置）
    //FixedUpdate()是固定帧更新（人为设置）
    #endregion
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        Move();
    }

    //移动方法
    #region
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //方法注释
        #region
        //方法1
        //这个方法是用了Sprite Renderer里面的Flip的X轴是被点选（也就是布尔值的True和Flase）来判断
        //Sprite Renderer里面的Flip是，沿着裁剪图片时设定的焦点来进行镜像翻转
        #endregion
        if (inputDirection.x > 0)
        {
            sp.flipX = false;
        }
        else if(inputDirection.x < 0)
        {
            sp.flipX = true;
        }

        //实现人物翻转的第二个方法
        #region
        /*
  
        //方法2
        //这个是常规改变翻转x的方法
        //我们需要FaceDir是整数类型，所以设为int数据类型，但是localScale.x是float浮点类型，所以需要强制转换成int
        int FaceDir = (int)transform.localScale.x;

        //当inputDirection.x > 0 时我们实在按方向右键，此时德FaceDir为1
        if (inputDirection.x > 0)
        {
            FaceDir = 1;
            //当inputDirection.x < 0 时我们实在按方向右键，此时德FaceDir为-1
        }
        else if(inputDirection.x < 0)
        {
            FaceDir = -1;
        }

        //要想人物根据输出设备（键盘、手柄）实现翻转，思路是把人物沿着x轴翻转，所以先要获取到tranform里面的Scale的x轴组件
        //下面就是获取到Scale（旋转）的组件，x轴是-1或者1的时候是镜像翻转的，所以要改变x轴的数值，我们设为FackDir，其他的y、z保持不变
        transform.localScale = new Vector3(FaceDir, 1, 1);

        */
        #endregion
        #endregion
    }


    //向跳跃施加一个向下的阻力
    #region
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");

        //检测人物碰撞地面后开始执行
        //施加一个向上的力
        if (physicsCheck.IsGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

    }
    #endregion

    //施加一个反弹力
    public void getHurt(Transform attacker)
    {
        //滑铲状态下无伤通过
        //!Boar.GetComponent<EnemyController>().isRun 进入野猪视野范围，判定为奔跑
        if (!isGlissade && !Boar.GetComponent<EnemyController>().isRun)
        {
            
            isHurt = true;
            //受伤的时候，停止一切操控，所以velocity的x和y轴方向速度都为0
            rb.velocity = Vector2.zero;

            //方法注释
            #region
            //人物受伤后反弹的距离是当前人物的X轴数值 减去 被碰撞（野猪）的当前的X轴的数值
            //例子：人物在x轴数值为1，野猪x轴数值为0，相减为1，人物反弹1的距离
            //但是如果人物距离野猪很远，人物x轴数值为100的话，野猪不变，相减为100，人物反弹力为100
            //添加.normalized就是无论相减为多少，都在0-1之间，就是说把相减的数值归1化
            #endregion
            Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

            PlayerAnimation.PlayerHurt();
        }

    }

    public void playerDeath()
    {
        //当isDeath为打开状态的时候
        //停止所有操作
        isDeath = true;
        inputControl.GamePlayer.Disable();
    }

    //人物死亡后，避免敌人再次攻击
    private void CheckState()
    {
        if (isDeath)
        {
            //当人物死亡的时候，layer改成“Enemy”，因为野猪（敌人）也是“Enemy”所以此时不会被攻击
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        else
        {
            //当人物不是死亡的时候，layer改成“Player”，因为野猪（敌人）也是“Enemy”所以此时会被攻击
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }


    //攻击函数
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        //检测是否有启动攻击的布尔值
        isAttack = true;

        //启动PlayerAnimation的PalyerAttack，也就是攻击动画
        PlayerAnimation.PlayerAttack();

        //当按下，攻击为0
        rb.velocity=new Vector2(0,rb.velocity.y);
        
    }

    //材质的判定
    public void CheckMaterial()
    {
        cc2.sharedMaterial = physicsCheck.IsGround ? Normal : Rock;
    }


    //墙壁滑落
    public void PlayerClimb()
    {
        //检测是否触地面
        if (!physicsCheck.IsGround)
            isClimb = true;
        PlayerAnimation.PlayerClimb(); 
    }


    //双击检测滑铲
    public void CheckGlissade()
    {

        //滑铲需求
        #region
        //双击方向键来触发滑铲
        //当滑铲状态触发时候，如果此时触碰敌人，可以无敌通过来进行躲避

        //参考：https://blog.51cto.com/u_8378185/5990608
        #endregion

        //方法解析：
        #region

        //0、LeftTimer -= Time.deltaTime 一开始就从0开始递减，并且计算两帧之间的差值
        //1、从0开始递减，触发 Left = clickLeftCount.zeroLeftTime 这个枚举条件
        //1、当我按下按键（非抬起）的时候，执行LeftTimer = 0.2f（也就是Time.deltaTime从0.2开始递减）
        //2、当我按抬起按键的时候，执行标记Left = clickLeftCount.secondLefttime（根据枚举，Left数值为1）
        //3、如果需要触发双击的效果，需要满足以下三个条件
        //rb.velocity.x < 0
        //Left == clickLeftCount.secondLefttime
        //LeftTimer > 0f
        //由于Time.deltaTime会从0.2快速递减，当我在递减至0这个的时候再次点击按键，就可以满足双击的条件，触发滑铲效果

        #endregion



        //按左键的检验
        #region

        
        //Time.deltaTime是两帧之间的差值
        LeftTimer -= Time.deltaTime;

        if (LeftTimer < 0f)
        {
            Left = clickLeftCount.zeroLeftTime;
        }

        //当按下A键后
        if (Input.GetKeyDown(KeyCode.A) && Left == clickLeftCount.zeroLeftTime)
        {
            LeftTimer = 0.2f;
            Left = clickLeftCount.firstLefttime;
        }

        //当抬起A键后
        if (Input.GetKeyUp(KeyCode.A) && Left == clickLeftCount.firstLefttime)
        {
            Left = clickLeftCount.secondLefttime;
            isGlissade = false;
            //Vector2 gForce = new Vector2(glissadeForce, 0);
            //rb.AddRelativeForce(transform.localPosition * gForce);

            
        }
        
        //双击A键后
        if (Input.GetKey(KeyCode.A) && Left == clickLeftCount.secondLefttime && LeftTimer > 0f)
        {
            Left = clickLeftCount.zeroLeftTime;

            //打开滑铲检测开关
            isGlissade = true;

            //当滑铲开始的时候，隐藏野猪（Boar）Capsule Collider 2D组件
            Boar.GetComponentInChildren<CapsuleCollider2D>().enabled = false;

            //开启滑铲动画
            PlayerAnimation.PlayerGlissade();

            //人物滑铲的时候，关闭无敌动画，但是可以穿过敌人
            isHurt = false;

        }
        #endregion


        //按右键的检验
        #region

        RightTimer -= Time.deltaTime;

        if (RightTimer < 0f)
        {
            Right = clickRightCount.zeroRightTime;
        }

        //当按下D键后

        if (Input.GetKeyDown(KeyCode.D) && Right == clickRightCount.zeroRightTime)
        {
            RightTimer = 0.2f;
            Right = clickRightCount.firstRightTime;
        }

        //当抬起D键后
        if (Input.GetKeyUp(KeyCode.D) && Right == clickRightCount.firstRightTime)
        {
            Right = clickRightCount.secondRightTime;
        }

        //双击D键后
        if (Input.GetKey(KeyCode.D) && Right == clickRightCount.secondRightTime && RightTimer > 0f)
        {
            Right = clickRightCount.zeroRightTime;

            //打开滑铲检测开关
            isGlissade = true;

            //当滑铲开始的时候，隐藏野猪（Boar）Capsule Collider 2D组件
            Boar.GetComponentInChildren<CapsuleCollider2D>().enabled = false;

            //开启滑铲动画
            PlayerAnimation.PlayerGlissade();

            //人物滑铲的时候，关闭无敌动画，但是可以穿过敌人
            isHurt = false;
        }
        #endregion
    }

    
}