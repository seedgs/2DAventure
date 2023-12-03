using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

using UnityEditor.Experimental.GraphView;

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

        inputDirection = inputControl.GamePlayer.Move.ReadValue<Vector2>();
    }

    //获取 PlayerInputControl(输入设备)，存进inputControl
    //PlayerInputControl在Seetings文件夹里面的InputSystem文件里面
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;

    //需要获得别的组件，就创建这个组件对应的类型，一个变量
    //需要获得PhysicsCheckl里面的东西，组件名+变量（命名随意）
    private PhysicsCheck physicsCheck;

    public Rigidbody2D rb;

    public SpriteRenderer sp;

    private CapsuleCollider2D coll;

    //获取动画组件
    private PlayerAnimation playerAnimation;

    //title命名
    [Header("基本参数")]
  
    private float runSpeed;

    private float walkSpeed;

    public float speed;

    public float jumpForce;

    public float hurtForce;

    private Vector2 originalOffset;

    private Vector2 originalSize;

    [Header("状态")]
    public bool IsHurt;

    public bool IsCrouch;

    public bool IsDead;

    public bool isAttack;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();

        //获取physicsCheck组件
        physicsCheck = GetComponent<PhysicsCheck>();
        //started那就按下那一刻
        //把Jump这个函数方法添加到你按键按下的按键按下的那一刻（started）里面执行
        inputControl.GamePlayer.Jump.started += Jump;

        coll = GetComponent<CapsuleCollider2D>();

        playerAnimation = GetComponent<PlayerAnimation>();

        originalOffset = coll.offset;

        originalSize = coll.size;

        //攻击
        inputControl.GamePlayer.Attack.started += Attack;

        //跑步与走路切换
        #region

        //在Awake函数里面写是因为，Awake函数在游戏对象开启后就会执行，如果游戏对象关闭后再执行，Awake也会再次开启
        runSpeed = speed;

        walkSpeed = speed / 2.5f;
        //ctx为回调函数
        //performed为按下按键
        inputControl.GamePlayer.Walkbutton.performed += ctx =>
        {
            if (physicsCheck.isGround)
            {
                speed = walkSpeed;
            }
        };

        //canceled为松开按键
        inputControl.GamePlayer.Walkbutton.canceled += ctx =>
        {
            if (physicsCheck.isGround)
            {
                speed = runSpeed;
            }
        };
        #endregion


    }



    /*//碰撞测试
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }*/


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
        if(!IsHurt)
        Move();
    }

    //移动方法
    public void Move()
    {
        //当不下蹲的时候才可以移动
        if (IsCrouch != true)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //方法1
        //这个方法是用了Sprite Renderer里面的Flip的X轴是被点选（也就是布尔值的True和Flase）来判断
        if (inputDirection.x > 0)
        {
            sp.flipX = false;
        }
        else if (inputDirection.x < 0)
        {
            sp.flipX = true;
        }

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


        //inputDirection为操作杆的变量，x方向为左右移动，y方向为跳或者下蹲
        //y为负数代表下蹲，y为整数代表跳跃

        IsCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;

        if (IsCrouch)
        {
            //修改碰撞体大小和位移
            coll.offset = new Vector2(-0.09440199f, 0.939046f);
            coll.size = new Vector2(0.4867882f, 1.6f);
        }
        else
        {
            //还原之前碰撞参数
            coll.size = originalSize;
            coll.offset = originalOffset;
        }

    }



    //给人物跳跃，但是不是一直不停往上跳，需要在上的方向上施加一个力
    private void Jump(InputAction.CallbackContext context)
    {

        //physicsCheck.isGround：检测地面碰撞
        if (physicsCheck.isGround)

            //在面上施加一个力
            //Debug.Log("Jump");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //受伤后，施加一个反弹力
    public void GetHurt(Transform attacker)
    {
        IsHurt = true;

        //停止运动
        rb.velocity = Vector2.zero;

        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }


    public void PlayerDead()
    {
        IsDead = true;
        Debug.Log("1");
        inputControl.GamePlayer.Disable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;

    }
}