using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //测试测试测试测试测试
    void Start()
    {
        
    }

    // Update is called once per frame

    //每一帧都坚持按键的输入
    void Update()
    {
        //获取inputControl里面的 GamePalyer 里面的 Move 的 Vector2 存进inputDirection，但是这个Vector2需要ReadValue

        inputDirection =inputControl.GamePlayer.Move.ReadValue<Vector2>();
    }


    //方法
    #region
    public PhysicsCheck physicsCheck;

    //获取 PlayerInputControl(输入设备)，存进inputControl
    //PlayerInputControl在Seetings文件夹里面的InputSystem文件里面
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;

    //获取PlayerAnimation脚本组件
    public PlayerAnimation PlayerAnimation;

    //加载刚体
    public Rigidbody2D rb;

    //加载“灵巧”渲染器
    public SpriteRenderer sp;


    //title命名
    [Header("基本参数")]

    //速度
    public float speed;

    //奔跑速度
    private float runSpeed;

    //行走速度
    private float walkSpeed;

    //当人物起跳时，添加一个瞬时向下的力
    public float jumpForce;

    //当人物受伤时，添加一个瞬时横向的力
    public float hurtForce;

    //检测受伤的布尔值
    public bool isHurt;

    //检测死亡的布尔值
    public bool isDeath;

    //创建攻击布尔值
    public bool isAttack;


    #endregion



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        physicsCheck = GetComponent<PhysicsCheck>();

        PlayerAnimation = GetComponent<PlayerAnimation>();

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

        //获取控制中控制走路的组件，在“按下”(performed)的时候,调用回调函数(+= ctx =>)
        //回调函数检测人物碰撞地面后开始执行
        //“按下”后为走路模式
        inputControl.GamePlayer.Walkbutton.performed += ctx =>
        {
            if (physicsCheck.IsGround)
            {
                speed = walkSpeed;
            }
        };

        
        //获取控制中控制走路的组件，在“松开”(canceled)的时候,调用回调函数(+= ctx =>)
        //回调函数检测人物碰撞地面后开始执行
        //“松开”后为跑步模式
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


    private void FixedUpdate()
    {
        if (!isHurt)
        Move();
    }


    //测试
    //OnTriggerStay2D为当触发器接触的时候，进行碰撞检测
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
    //}


    //移动方法
    #region
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //方法1
        //这个方法是用了Sprite Renderer里面的Flip的X轴是被点选（也就是布尔值的True和Flase）来判断
        //Sprite Renderer里面的Flip是，沿着裁剪图片时设定的焦点来进行镜像翻转
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


    //向跳跃施加一个向上的力
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
        isHurt = true;
        //受伤的时候，停止一切操控，所以velocity的x和y轴方向速度都为0
        rb.velocity = Vector2.zero;
        
        //人物受伤后反弹的距离是当前人物的X轴数值 减去 被碰撞（野猪）的当前的X轴的数值
        //例子：人物在x轴数值为1，野猪x轴数值为0，相减为1，人物反弹1的距离
        //但是如果人物距离野猪很远，人物x轴数值为100的话，野猪不变，相减为100，人物反弹力为100
        //添加.normalized就是无论相减为多少，都在0-1之间，就是说把相减的数值归1化
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        PlayerAnimation.playerHurt();

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

    }

}