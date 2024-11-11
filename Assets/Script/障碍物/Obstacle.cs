using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public PlayerMove player;
    private bool isPassed = false; // 初始化isPassed为false

    private void Start()
    {
        // 获取 PlayerMove 脚本挂载的对象
        player = FindObjectOfType<PlayerMove>();

        if (player != null)
        {
            // 输出 PlayerMove 脚本挂载对象的位置
            Vector3 playerPosition = player.transform.position;
            Debug.Log("Player Position: " + playerPosition);
        }
    }

    void Update()
    {   
        // 判断玩家是否已经通过障碍物，并且防止重复计分
        if (!isPassed && transform.position.x < player.transform.position.x)
        {
            isPassed = true;
            GameController.Instance.IncrementScore(); // 增加分数
        }
    }

    // 如果障碍物重新出现在场景中，需要将isPassed重置
    public void ResetObstacle()
    {
        isPassed = false;
    }
}