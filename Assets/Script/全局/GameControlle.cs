using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//管理游戏状态

public class GameController : MonoBehaviour
{
    [Header("Score")]
    public static GameController Instance; //单例
    public Text scoreText; //显示在那哪个文本
    public int score; //分数统计
    public bool isPaused; //是否为暂停
    [Header("Speed")] 
    public PlayerMove playerMove; // 引用PlayerMove组件
    public Text speedText; // UI文本对象，用于显示速度
    [Header("Coin")]
    private int _coinCount = 0; // 当前金币数量
    public Text coinText; // UI上的金币数显示
    [Header("GlobelData")]
    public SpeedManager speedManager; // 引用 SpeedManager 脚本
    public float levelSpeed = 7f;
    [Header("SkillDash")]
    public Image skillIconImage;           // 引用技能图标的 Image 组件
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        levelSpeed = speedManager.GetLevelSpeed();
        DashCoolTime();

    }

    public void IncrementScore()
    {
        score++;
        UpdateScore();
    }
    
    public void UpdateScore()
    {
        scoreText.text = "得分: " + score;
    }

    public void PauseGame()
    {
        if (isPaused) return; // 如果已经暂停，直接返回

        float targetTimeScale = 0f; // 完全暂停
        float pauseDuration = 0.4f; // 渐变暂停的持续时间（秒）
        float pauseStartTime = Time.time; // 记录暂停开始的时间

        StartCoroutine(PauseCoroutine(pauseStartTime, targetTimeScale, pauseDuration));
    }

    public void RestartGame()
    {
        isPaused = false;
        score = 0;
        scoreText.text = "得分: " + score;
        Time.timeScale = 1;
        speedManager.ResetSpeed();
    }

    public void SpeedUp()
    {
        float speed = playerMove.GetCurrentSpeed();
        speedText.text = "速度: " + speed.ToString("F1"); // 显示速度，保留两位小数
    }

    // 当玩家碰撞到金币时
    public void CollectCoin()
    {
        _coinCount++;
        UpdateCoinText();
        // 打印调试信息，查看文本值
        Debug.Log("Updated Coin Text: " + coinText.text);
    }


    // 更新 UI 显示的金币数量
    public void UpdateCoinText()
    {
        coinText.text = "金币: " + _coinCount;
    }

    public void DashCoolTime()
    {
        // 获取冷却时间比例
        float cooldownRatio = playerMove.GetDashCooldownTime();

        // 更新技能图标的填充进度
        skillIconImage.fillAmount = 1 - cooldownRatio; // 冷却时间比例越大，技能图标的填充越小
    }



    private IEnumerator PauseCoroutine(float pauseStartTime, float targetTimeScale, float pauseDuration)
    {
        while (Time.time - pauseStartTime < pauseDuration)
        {
            float t = (Time.time - pauseStartTime) / pauseDuration;
            Time.timeScale = Mathf.Lerp(1f, targetTimeScale, t);
            yield return null;  // 等待一帧继续执行
        }

        Time.timeScale = targetTimeScale; // 确保最终到达目标时间缩放
        isPaused = true; // 设置暂停状态为 true
    }
}