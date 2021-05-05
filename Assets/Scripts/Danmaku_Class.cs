using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku_Class : MonoBehaviour
{
    [Header("基本信息")]
    public int damage;
    //所有弹幕的基类
    [Header("速度相关")]
    public float speed;
    public float acceleration;
    [HideInInspector]
    public delegate float SpeedChange(float speed,float acceleration);
    public SpeedChange speedChange = new SpeedChange(SpeedChange_Default);
    
    [Header("插值移动相关")]
    [Tooltip("是否插值移动")]
    public bool ifInterpolationMove = false;
    [Tooltip("插值移动目标位置")]
    public Vector2 InterpolationMove_target = Vector2.zero;
    [Tooltip("插值比例")]
    public float InterpolationMove_lerpT = 0.5f;

    [Header("属性相关")]
    [Tooltip("子弹是否穿墙")]
    public bool can_gothrough_wall = false;
    [Tooltip("是否无视DeathLine")]
    public bool ignore_deathline = false;
    [Tooltip("子弹存活时间")]
    public float will_destroy_after = 10;

    public Vector2 speed_angle = Vector2.right;

    //依赖组件
    protected Rigidbody2D rb2d;

    //本次移动的增量
    protected Vector2 lastMoveDelta;

    [Header("当前速度与加速度")]
    public float current_speed;
    public float current_acceleration;
    private static float SpeedChange_Default(float speed, float acceleration) { return speed + acceleration; }
    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    protected void OnEnable()
    {
        //重置速度和加速度
        current_speed = speed;
        current_acceleration = acceleration;
        //自毁倒计时
        Invoke(nameof(TimeOut), will_destroy_after);
    }
    protected void FixedUpdate()
    {
        if (ifInterpolationMove)
        {
            rb2d.position = Vector2.Lerp(rb2d.position, InterpolationMove_target, InterpolationMove_lerpT);
        }
        else
        {
            //速度计算
            current_speed = speedChange(current_speed, current_acceleration);
            //移动子弹
            lastMoveDelta = speed_angle * current_speed * Time.fixedDeltaTime;
            rb2d.MovePosition(rb2d.position + lastMoveDelta);
        }
    }
    protected virtual void TimeOut()
    {
        //存活超过生命周期自动回收
        Recycle();
    }
    public virtual void Recycle()
    {
        //将子弹回收到对象池
        CancelInvoke();
        Destroy(gameObject);
    }

    //碰撞事件相关
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞事件总控制器
        if(collision.CompareTag("Monster_Enemy"))
        {
            collision.GetComponent<Monster_Class>().life -= damage;
            Recycle();
        }
    }

    public virtual void HitWall(Collider2D collision)
    {
        Recycle();
    }
}
