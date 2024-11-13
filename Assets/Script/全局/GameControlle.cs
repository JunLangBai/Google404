using UnityEngine;
using UnityEngine.UI;

//管理游戏状态

public class GameController : MonoBehaviour
{
    [Header("Score")] public static GameController Instance; //单例
    public Text scoreText; //显示在那哪个文本
    public int score; //分数统计
    public bool isPaused; //是否为暂停
    [Header("Speed")] public PlayerMove playerMove; // 引用PlayerMove组件
    public Text speedText; // UI文本对象，用于显示速度
    [Header("Coin")] private int _coinCount = 0; // 当前金币数量
    public Text coinText; // UI上的金币数显示
    [Header("GlobelData")] public float levelSpeed = 7f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
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
        isPaused = true;
        Time.timeScale = 0.2f; // 暂停游戏
    }

    public void RestartGame()
    {
        isPaused = false;
        score = 0;
        scoreText.text = "得分: " + score;
        Time.timeScale = 1;
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
}