using System;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float speed = 7f;            // 背景移动速度
    public float backgroundWidth = 26f; // 背景片段的宽度
    public Transform player;            // 玩家或摄像机
    public Rigidbody2D playerRigidbody; // 玩家 Rigidbody2D
    public float destroyDistance = 12f; // 离开摄像机时销毁的距离

    private GameObject[] _backgrounds;   // 用于存储激活的背景片段
    private int _currentIndex = 0;       // 当前背景片段索引

    public Transform backgroundTransform;  // 背景的初始Transform
    private float _bufferZone = 1f; // 背景回收的缓冲区，避免回收过早

    void Start()
    {
        // 初始化背景片段数组并激活四个背景片段
        _backgrounds = new GameObject[4];

        for (int i = 0; i < _backgrounds.Length; i++)
        {
            // 从对象池获取背景片段
            _backgrounds[i] = ObjectPool.Instance.GetObject();
            _backgrounds[i].SetActive(true);

            // 设置背景片段的初始位置
            float xPos = backgroundTransform.position.x + (i * backgroundWidth);
            float yPos = backgroundTransform.position.y; 
            _backgrounds[i].transform.position = new Vector3(xPos, yPos, 0);
        }
    }

    void Update()
    {
        // 背景片段向左移动
        MoveBackground();

        // 检查背景片段是否离开视野并回收
        CheckBackgroundPosition();

    }

    private void FixedUpdate()
    {
        //玩家也随着背景移动
        MovePlayer();
    }

    void MoveBackground()
    {
        // 持续移动所有背景片段
        foreach (var bg in _backgrounds)
        {
            bg.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void CheckBackgroundPosition()
    {
        // 检查当前索引背景片段是否离开视野
        if (_backgrounds[_currentIndex].transform.position.x <= -backgroundWidth - _bufferZone)
        {
            // 将当前背景片段回收到池中
            ObjectPool.Instance.ReturnObject(_backgrounds[_currentIndex]);

            // 重新设置当前背景片段的位置为最右侧
            int nextIndex = (_currentIndex + 1) % _backgrounds.Length;
            float newPosX = _backgrounds[(_currentIndex + 3) % _backgrounds.Length].transform.position.x + backgroundWidth; // 获取最后一个背景的位置并延伸
            float newPosY = _backgrounds[(_currentIndex + 1) % _backgrounds.Length].transform.position.y;
            _backgrounds[_currentIndex].transform.position = new Vector3(newPosX, newPosY, 0);
            
            // 激活当前背景片段，并更新索引
            _backgrounds[_currentIndex].SetActive(true);
            _currentIndex = nextIndex;
        }
    }
    
    void MovePlayer()
    { 
        // 如果玩家没有按下左右键（即没有主动控制水平移动），则由背景控制
        if (playerRigidbody != null && Mathf.Approximately(playerRigidbody.velocity.x, 0f))
        {
            // 角色的目标移动速度与背景的速度一致
            float targetSpeed = -speed;  // 角色应该朝左侧移动，与背景一致
            playerRigidbody.velocity = new Vector2(targetSpeed, playerRigidbody.velocity.y);
        }
        else
        {
            // 让玩家自己控制水平移动（如果玩家有输入）
            float targetSpeed = playerRigidbody.velocity.x;  // 玩家自己控制的水平速度
            playerRigidbody.velocity = new Vector2(targetSpeed, playerRigidbody.velocity.y);
        }
    }
}
