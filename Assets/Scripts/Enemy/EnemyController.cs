using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    protected Animator anim;

    protected Rigidbody2D rb;

    public GameObject player;

    public SpriteRenderer sp;


    [Header("基础信息")]
    public float normalSpeed;

    public float currentSpeed;

    

    public float chaseSpeed;

    public Vector3 faceDir;

    public bool isRun;

    public bool isWalk;

    //public bool isRetreat;

    private void Update()
    {
        //把野猪的朝向修正
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        ////野猪退后
        //if (player.GetComponent<PlayerController>()?.isHurt == true)
        //{
        //    boarAgainAttack();
        //}
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sp = GetComponent<SpriteRenderer>();

        currentSpeed = normalSpeed;
    }


    //方法注释
    #region
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
        boarMove();
        isWalk = true;
        transform.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        boarNoRun();
        
    }

    //方法注释
    #region
    //virtual是可以允许子类修改此方法
    //进入游戏后，野猪会一直处于移动状态
    //所以野猪的移动，需要野猪的RigidBody2D组件中的Info里面的Velocity的 X 为 1 的时候，野猪移动
    //人过需要让野猪移动速度快一点，需要在Velocity的x上添加一个自定义的floot参数，修改这个参数就可以增减速度
    //野猪的Velocity的y向速度为0（也就原来的Velocity的y向速度）
    #endregion
    public virtual void boarMove()
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, 0);
    }

    //方法注释
    #region
    //人物进入野猪视野范围的时候，野猪开始 “加速”冲向人物
    //由于野猪视野的检测机制也是沿用碰撞机制
    //所以如果野猪一旦进入奔跑模式的 “瞬间” 不关闭野猪视野组件
    //人物就会触发受伤机制
    //所以野猪开启蹦跑的一瞬间，视野检测组件就会关闭
    #endregion
    public virtual void boarRun()
    {
        isRun = true;
        currentSpeed = normalSpeed * 2;
        //人物进入野猪视野范围后，关闭碰撞组件
        transform.GetComponentInChildren<EdgeCollider2D>().enabled = false;
        //player.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    //方法注释
    #region
    //以下是野猪的视线范围的组件，true为打开，false为关闭
    //transform.GetComponentInChildren<EdgeCollider2D>()
    //当野猪的视野范围重新打开的时候，此时的人物已经离开野猪的视野范围，野猪停止蹦跑
    #endregion
    public virtual void boarNoRun()
    {
        if (transform.GetComponentInChildren<EdgeCollider2D>() == true)
        {
            currentSpeed = normalSpeed;
            isRun = false;
        }
    }



    ////野猪退后
    //public virtual void boarAgainAttack()
    //{
    //    isRetreat = true;
    //    currentSpeed = -normalSpeed;
    //}


}