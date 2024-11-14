using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [Header("Speed Settings")]
    public float levelSpeed = 7f; // 初始速度
    public float levelSpeedIncrement = 0.1f; // 每秒增加的速度值
    public float maxLevelSpeed = 20f; // 最大速度
    private float initialLevelSpeed; // 记录初始的 levelSpeed

    private float timeSinceLastIncrease = 0f; // 用于跟踪时间流逝

    void Awake()
    {
        // 保存初始的速度值
        initialLevelSpeed = levelSpeed;
    }

    void Update()
    {
        // 每秒增加一次 speed，直到达到最大值
        timeSinceLastIncrease += Time.deltaTime;

        if (timeSinceLastIncrease >= 1f) // 每秒增加
        {
            timeSinceLastIncrease = 0f;
            IncreaseSpeed();
        }
    }

    void IncreaseSpeed()
    {
        // 增加速度，但确保不超过最大值
        if (levelSpeed < maxLevelSpeed)
        {
            levelSpeed += levelSpeedIncrement;
        }
    }

    // 返回当前的 levelSpeed
    public float GetLevelSpeed()
    {
        return levelSpeed;
    }

    // 重置速度为初始值
    public void ResetSpeed()
    {
        levelSpeed = initialLevelSpeed; // 恢复为初始速度
        timeSinceLastIncrease = 0f; // 重置时间增量
    }
}