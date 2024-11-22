using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f; //移动速度
    private Rigidbody2D rb; // 玩家刚体
    public float fastFallForce = 20f;   // 下落速度

    [Header("Jump")]
    public float jumpCooldown = 0.2f; //跳跃冷却
    public float jumpForce = 10f;      //跳跃力
    public Transform groundCheck;      //地面检查
    public float checkRadius = 0.2f;   //检测半径
    public LayerMask groundLayer;      //地面图层

    private float moveInput;
    private bool isGrounded;   //是否地面
    private bool readyToJump;  //是否允许跳跃
    
    [Header("Dash")]
    
    //冲刺速度
    public float dashSpeed = 10f;
    //判断冲刺
    private bool isDashing;
    //冲刺冷却
    public float dashCooldown = 2f;
    //冲刺持续时间
    public float dashDuration = 0.3f;
    private float lastDClickTime = -1f; // 上次按下D键的时间
    private float dashTime = 0f; // 当前冲刺时间
    private float dashCooldownTime = 0f; // 当前冲刺冷却时间
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //获取玩家刚体组件
        readyToJump = true;
    }

    void Update()
    {
        MyInput();


        //地面检查
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //跳跃逻辑
        if (isGrounded && readyToJump && Input.GetKey(KeyCode.Space))
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //下落逻辑
        if (!isGrounded && Input.GetKey(KeyCode.S))
        {
            UnderDown();
        }
    }

    void FixedUpdate()
    {
       MovePlayer();
    }

    public void MyInput()
    {
        //获取玩家输入
        moveInput = Input.GetAxisRaw("Horizontal");
        
        // 检测双击D键
        if (Input.GetKeyDown(KeyCode.D) && dashCooldownTime <= 0f)
        {
            if (Time.time - lastDClickTime <= 0.3f) // 判断双击时间间隔
            {
                isDashing = true;
                dashTime = dashDuration; // 设置冲刺时间
                dashCooldownTime = dashCooldown; // 重置冷却时间
            }
            lastDClickTime = Time.time;
        }
    }

    public void MovePlayer()
    {
        
        // 更新冲刺冷却时间
        if (dashCooldownTime > 0f)
        {
            dashCooldownTime -= Time.deltaTime; // 冷却倒计时
        }

       
        // 处理冲刺
        if (isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * Mathf.Sign(moveInput), rb.velocity.y);
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false; // 冲刺结束
            }
        }
        //玩家移动
        else if (moveInput != 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); //跳跃力的方向
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void UnderDown()
    {
         //下落逻辑
         rb.velocity = new Vector2(rb.velocity.x, -fastFallForce);
    }
    
    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude; // 返回当前速度的大小
    }
    
    // 提供一个公共方法让其他脚本访问当前冷却时间
    public float GetDashCooldownTime()
    {
        return dashCooldownTime / dashCooldown;
    }
    
    //障碍物碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 遇到障碍物失败，暂停游戏
            GameController.Instance.PauseGame();
        }
        else if (collision.gameObject.CompareTag("DeathWall"))
        {
            // 碰到死亡判定区，暂停游戏
            GameController.Instance.PauseGame();
        }
    }
}
