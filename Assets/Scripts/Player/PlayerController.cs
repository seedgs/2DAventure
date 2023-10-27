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

        inputDirection = inputControl.GamePlayer.Move.ReadValue<Vector2>();
    }


    public PhysicsCheck physicsCheck;

    //获取 PlayerInputControl(输入设备)，存进inputControl
    //PlayerInputControl在Seetings文件夹里面的InputSystem文件里面
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;
    
    public Rigidbody2D rb;

    public SpriteRenderer sp;
    //title命名
    [Header("基本参数")]
    public float speed;

    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
        //started那就按下那一刻
        //把Jump这个函数方法添加到你按键按下的按键按下的那一刻（started）里面执行
        inputControl.GamePlayer.Jump.started += Jump;

        physicsCheck = GetComponent<PhysicsCheck>();
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
        Move();
    }

    //移动方法
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //方法1
        //这个方法是用了Sprite Renderer里面的Flip的X轴是被点选（也就是布尔值的True和Flase）来判断
        if(inputDirection.x > 0)
        {
            sp.flipX = false;
        }
        else if(inputDirection.x < 0)
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
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if(physicsCheck.IsGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


}