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
    }

    public void MovePlayer()
    {
        //玩家移动
        if (moveInput != 0)
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
}
