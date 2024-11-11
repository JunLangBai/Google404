using UnityEngine;
using UnityEngine.UI;

//管理游戏状态

public class GameController : MonoBehaviour
{
    [Header("Score")]
    public static GameController Instance;//单例
    public Text scoreText;//显示在那哪个文本
    public int score;    //分数统计
    public bool isPaused;//是否为暂停
    [Header("Speed")]
    public PlayerMove playerMove; // 引用PlayerMove组件
    public Text speedText;        // UI文本对象，用于显示速度

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void IncrementScore()
    {
        score++;
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
}