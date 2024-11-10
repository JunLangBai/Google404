using UnityEngine;
using UnityEngine.UI;

//管理游戏状态

public class GameController : MonoBehaviour
{
    public static GameController Instance;//单例
    public Text scoreText;//显示在那哪个文本
    private int score;    //分数统计
    private bool isPaused;//是否为暂停

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // 暂停游戏
    }

    public void RestartGame()
    {
        isPaused = false;
        score = 0;
        scoreText.text = "Score: " + score;
        Time.timeScale = 1;
    }
}