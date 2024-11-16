using UnityEngine;
using UnityEngine.UI; // 引入 UI 库

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;       // 金币预制体
    public float minSpawnTime = 1f;     // 最小生成时间间隔
    public float maxSpawnTime = 3f;     // 最大生成时间间隔
    public float maxSpawnHeight = 1f;      // 生成金币的高度范围
    public float minSpawnHeight = 1f;
    public Transform player;            // 玩家或摄像机

    private float _nextSpawnTime;       // 下次生成金币的时间
    public float moveSpeed = 7f;  // 金币的移动速度

    void Start()
    {
        moveSpeed = GameController.Instance.levelSpeed;
        ScheduleNextCoinSpawn();
    }

    void Update()
    {
        
        
        // 每帧检查是否到了生成金币的时间
        if (Time.time >= _nextSpawnTime)
        {
            SpawnCoin();
            ScheduleNextCoinSpawn();
        }
    }

    // 随机生成金币
    void SpawnCoin()
    {
        // 获取当前生成点的位置，假设是当前脚本对象的 transform.position
        float baseYPos = transform.position.y;  // 当前位置的 y 坐标

        // 随机生成一个 y 位置，范围是上下浮动
        float yPos = Random.Range(baseYPos - minSpawnHeight, baseYPos + maxSpawnHeight);  // 根据基准位置浮动

        // 创建金币实例
        GameObject coin = Instantiate(coinPrefab, new Vector3(transform.position.x, yPos, 0), Quaternion.identity);
        
        // 将金币命名为 Coin 以方便调试
        coin.name = "Coin";
    }


    // 调度下一次生成金币的时间
    void ScheduleNextCoinSpawn()
    {
        _nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
    
}