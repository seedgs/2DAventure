using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemyController : MonoBehaviour
{
    //[HideInInspector]是在unity窗口隐藏
    [HideInInspector] public Animator anim;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public GameObject player;

    [HideInInspector] public SpriteRenderer sp;

    [HideInInspector] public Character ch;

    [HideInInspector] private EnemyLeftPhysicsCheck elpc;

    [HideInInspector] private EnemyRightPhysicsCheck erpc;

    [HideInInspector] private EnemyRunPhysicsCheck eupc;



    [Header("正常巡逻速度")]
    public float normalSpeed;

    [Header("奔跑速度")]
    public float chaseSpeed;

    [Header("当前速度")]
    public float currentSpeed;

    [Header("进入等待动画时速度")]
    public float waitSpeed;

    [Header("受伤时反向力")]
    public float hurtForce;

    [HideInInspector] public bool collisionLeft;

    [HideInInspector] public bool collisionRight;




    [Header("奔跑时间")]
    public float continueRunTime;

    [Header("当前设置时间")]
    public float CurrentWaitTime;

    [Header("正常等待时间")]
    public float NormalWaitTime;

    public float lostTime;

    public float lostTimeCounter;

    public float totalCollisionWallTime = 2;



    //当野猪进入某一个状态，且丢失了玩家的
    //attacker就是用来检测是否丢失玩家的
    public Transform attacker;

    [HideInInspector] public Vector3 facdir;

    //[Header("计数器")]


    [HideInInspector] public Vector3 faceDir;

    [Header("状态")]

    //追击状态（敌人）
    protected BaseState chaseState;

    //当前状态（敌人）
    protected BaseState currentState;

    //巡逻状态（敌人）
    protected BaseState patrolState;

    //奔跑状态（敌人）
    protected BaseState runState;


    [Header("当前奔跑状态")]
    public bool isRun;

    [Header("当前巡逻状态")]
    public bool isWalk;

    [Header("当前受伤状态")]
    public bool isHurt;

    [Header("当前死亡状态")]
    public bool isDead;

    [Header("当前不是左等待状态")]
    public bool isNotLeftWait;

    [Header("当前不是右等待状态")]
    public bool isNotRightWait;

    public bool isWall;


    [Header("CheckPlayerBoxCast检测数值")]
    public Vector2 checkPlayerCenterOffset;

    public Vector2 checkPlayerCheckSize;

    public float checkPlayerCheckDistance;

    public LayerMask checkPlayerAttackLayer;


    [Header("CheckWallBoxCast检测数值")]
    public Vector2 checkWallCenterOffset;

    public Vector2 checkWallCheckSize;

    public float checkWallCheckDistance;

    public LayerMask checkWallAttackLayer;


    //public bool isRetreat;

    public void Update()
    {
        //把野猪的朝向修正
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        ////野猪退后
        //if (player.GetComponent<PlayerController>()?.isHurt == true)
        //{
        //    boarAgainAttack();
        //}



        lostPlayer();

        currentState.LogicUpdate();


    }


    //一进入游戏就能执行代码 
    public void OnEnable()
    {

        //当进入游戏的时候，敌人“当前”的状态就“巡逻”的状态
        currentState =  patrolState;

        //patrolState = new BoarPatrolState();

        //所有在这个状态一开始的时候就执行“当前”状态
        //当绑定状态机传参后，这里需要“this”来传参，否则会报错
        //currentState.OnEnter(this);


        //lostPlayer();

        patrolState.OnEnter(this);


    }



    //这里的Awake是在EnemyController脚本的Awake执行之下，再执行Boar脚本的Awake
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sp = GetComponent<SpriteRenderer>();

        ch = GetComponent<Character>();

        elpc = GetComponent<EnemyLeftPhysicsCheck>();

        erpc = GetComponent<EnemyRightPhysicsCheck>();

        eupc = GetComponent<EnemyRunPhysicsCheck>();



        currentSpeed = normalSpeed;

        
    }


    //固定帧率执行
    #region
    //方法注释

    //FixedUpdat为固定帧率就会进行调用
    //问答：固定到什么程度才会调用里面的方法呢？
    //猜测：以transform.GetComponentInChildren<EdgeCollider2D>().enabled
    //以上方法例子，这个视野的方法是在野猪奔跑的瞬间就关闭，也是人物与野猪的视野范围碰撞的时候，就关闭这个组件
    //按照“FixedUpdat为固定帧率就会进行调用”这个去猜测，当碰撞瞬间就调用，也就是下一次固定帧率调用就是这个碰撞再次发生的时候
    //这个野猪视野范围与人物再次碰撞，一般就到了野猪后背的视野范围与人物发生碰撞这个帧率点
    //所以当人物与野猪后背的视野范围碰撞的时候，就触发FixedUpdat，野猪视野范围被再次开启
    #endregion

    public void FixedUpdate()
    {


        //patrolState.LogicUpdate();

        //chaseState.LogicUpdate();

        //transform.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        //NoRun();

        patrolState.LogicUpdate();

        isWall = false;
        
        //chaseState.LogicUpdate();

    }



    public void OnDsiable()
    {
        currentState.OnExit();
    }


    //移动
    #region
    //方法注释

    //virtual是可以允许子类修改此方法
    //进入游戏后，野猪会一直处于移动状态
    //所以野猪的移动，需要野猪的RigidBody2D组件中的Info里面的Velocity的 X 为 1 的时候，野猪移动
    //人过需要让野猪移动速度快一点，需要在Velocity的x上添加一个自定义的floot参数，修改这个参数就可以增减速度
    //野猪的Velocity的y向速度为0（也就原来的Velocity的y向速度）
   
    public virtual void move()
    {

        if (!isHurt && !isDead && elpc.isLeftGround == true && erpc.isRightGround == true) {

            rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, 0);
            
        }

    }
    #endregion


    //站立（无其他动作）
     #region
    public virtual void wait()
    {

        //左边监测点
        if (elpc.isLeftGround == true)
        {
            isNotLeftWait = true;

            //CurrentWaitTime = NormalWaitTime;
            //Debug.Log("LeftMove");
        }
        if (elpc.isLeftGround == false) //当野猪与地面的碰撞检测的isGround值为false时，野猪速度的waitSpeed为0
        {
            isNotLeftWait = false;

            CurrentWaitTime -= Time.deltaTime;

            rb.velocity = new Vector2(faceDir.x * waitSpeed * Time.deltaTime * CurrentWaitTime, 0);

            if (CurrentWaitTime <= 0)
            {

                transform.localScale = new Vector3(-1, 1, 1);


                rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, 0);
                
            }

            
            //Debug.Log("NotLeftMove");
        }



        //右边监测点
        if (erpc.isRightGround == true)
        {
            isNotRightWait = true;

            //CurrentWaitTime = NormalWaitTime;
            //Debug.Log("RightMove");
        }
        if (erpc.isRightGround == false) //当野猪与地面的碰撞检测的isGround值为false时，野猪速度的waitSpeed为0
        {
            isNotRightWait = false;

            CurrentWaitTime -= Time.deltaTime;

            rb.velocity = new Vector2(faceDir.x * waitSpeed * Time.deltaTime * CurrentWaitTime, 0);

            
            if (CurrentWaitTime <= 0)
            {

                transform.localScale = new Vector3(1, 1, 1);


                rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, 0);
                
            }

            //Debug.Log("NotRightMove");
        }
        if(isNotLeftWait && isNotRightWait)
        {
            CurrentWaitTime = NormalWaitTime;
        }
    }
    #endregion




    //受伤
    #region

    //方法注释
    //以下是野猪的视线范围的组件，true为打开，false为关闭
    //transform.GetComponentInChildren<EdgeCollider2D>()
    //当野猪的视野范围重新打开的时候，此时的人物已经离开野猪的视野范围，野猪停止蹦跑
    

    public void onTakeTruma(Transform attacktrans)
    {
        attacker = attacktrans;


        //转身
        #region

        //人物在野猪的左侧
        //人物x坐标数值永远比野猪x坐标数值要小，所以他们的差值永远小于0
        //此时需要野猪朝着人物（左侧）方面转过来
        if (attacktrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //人物在野猪的右侧
        //人物x坐标数值永远比野猪x坐标数值要大，所以他们的差值永远大于0
        //此时需要野猪朝着人物（右侧）方面转过来
        if (attacktrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        #endregion



        //受伤被击退
        #region
        isHurt = true;

        anim.SetTrigger("Hurt");

        Vector2 Dir = new Vector2(transform.position.x - attacktrans.position.x, 0).normalized;


        //当玩家在敌人任何状态下攻击时，敌人都会受到这个后退的力
        rb.velocity = new Vector2(0, rb.velocity.y);

        //固定写法，执行下面的协同器S  tartCoroutine(协同器函数名以及传参)
        StartCoroutine(onHurt(Dir));

        #endregion
    }
    #endregion



    //协程
    #region

    //使用协程是因为代码加载是快速的，不会等敌人受伤被击退这个过程结束isHurt才 等于 false
    //此时需要协程来延迟代码的加载时间，然后再执行下一行
    //IEnumerator为协同程序


    IEnumerator onHurt(Vector2 Dir)
    {
        //下面括号内的Dir是上一个函数里面的数值，这里如果要使用Dir参数，上面函数需要调用Dir
        rb.AddForce(Dir * hurtForce, ForceMode2D.Impulse);


        //以下固定顺序yield return null;  ruturn后面必须有值，可以是null，null的意思是执行完上一帧就直接执行下一帧
        //new waitForSecond(0.45f)意思是执行括号内的固定秒数后代码再往下执行，相当于延迟器
        yield return new WaitForSeconds(0.4f);

        isHurt = false;
    }
    #endregion



    //执行死亡动画
    #region
    public void onDead()
    {
        
        if (ch.currentHealth == 0)
        {
            gameObject.layer = 2;
            //当执行死亡动画的时候，野猪马上切换碰撞图层，该图层已经设置为不与玩家（角色）残生碰撞
            //layer的设置，(寻找路径)edit --> project setting --> physics 2D --> Layerer Collision Matrix

            isDead = true;

            anim.SetBool("Dead",isDead);


        }
    }



    //死亡后执行隐藏
    public void hiddenGameobject()
    {
        gameObject.SetActive(false);
    }



    /*    //死亡后执行销毁
    public void destoryGameobject()
    {
        Destroy(this.gameObject);
    }*/
    #endregion



    //遇到空气墙时的各种状态
    #region
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bg_Rock_Left" || collision.name == "Bg_Rock_Right")
        {
            isWall = true;
            currentSpeed = waitSpeed;

            CurrentWaitTime = 0;
        }

        if (collision.name == "Bg_Rock_Left")
        {
            collisionLeft = true;
        }
        else
        {
            collisionLeft = false;
        }

        if (collision.name == "Bg_Rock_Right")
        {
            collisionRight = true;
        }
        else
        {
            collisionRight = false;
        }

    }
    #endregion


    //敌人丢失玩家后，多久恢复以前状态
    #region
    public void lostPlayer()
    {
        
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            //Debug.Log("lostTimeCounter_star_reduce");

            lostTimeCounter -= Time.deltaTime;

        }

    }

    #endregion



    //检测玩家
    #region
    //BoxCast方法检测敌人
    //BoxCast就是朝特定方向拖动一个 “盒体” 穿过场景，可以检测并报告与“盒体”接触的对象
    //例子：在敌人身上用boxcast，敌人身上就会有一个“盒体”。敌人移动，“盒体”也会跟随移动。当其他物体碰撞到这个“盒体”的时候，就会报告碰撞的是什么。

    //Boxcast里面的参数
    //origin： 盒体在2D空间的起点
    //size：盒体大小
    //angle：盒体的角度
    //direction：盒体方向的矢量（朝哪个方向）
    //distance：盒体最大的投射距离
    //layerMask：过滤器，用于尽在特定层上检测碰撞体
    //minDepth：Z坐标大于或等于该值的对象
    //maxDepth：Z坐标小于或等于该值的对象
    public bool FoundPlayer() //返回布尔值，是否发现敌人
    {
        //当你不知道“return”返回的是什么值的时候，可以在前面改成“var...”
        //例子：
        //var what = Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, facdir, checkDistance, attackLayer);
        //鼠标移进“var”就会有提示;



        var checkPlayerCollision = Physics2D.BoxCast(transform.position + (Vector3)checkPlayerCenterOffset, checkPlayerCheckSize, 0, facdir, checkPlayerCheckDistance, checkPlayerAttackLayer);

        return checkPlayerCollision;
    }

    #endregion


    //敌人的状态转换
    #region
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState, //当判断为patrol的时候，切换到patrol状态

            NPCState.Chase => chaseState, //当判断为chase的时候，切换到chase状态

            _ => null //没有的话返回一个null
        };

        currentState.OnExit();  // 结束上一个状态

        currentState = newState; // 切换状态

        currentState.OnEnter(this); // 执行新的状态

        
    }
    public void OnDrawGizmosSelected()
    {
        //经过测试上面的checkSize的x数值 与 checkDistance的x数值是“一半”的关系
        //例：当设置checkDistance的x数值为3，此时屏幕的点就会移动，如果需要检测的点也是这里的话，checkSize的x数值设置成checkDistance的x的数值的一倍，也就是6
        Gizmos.DrawWireSphere(transform.position + (Vector3)checkPlayerCenterOffset + new Vector3(checkPlayerCheckDistance * faceDir.x, 0), 0.2f);

       
    }

    internal bool OnTriggerEnter2D()
    {
        throw new NotImplementedException();
    }
    #endregion
}