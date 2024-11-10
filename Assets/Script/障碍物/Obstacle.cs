using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isPassed;

    void Update()
    {
        if (!isPassed && transform.position.x < GameController.Instance.transform.position.x)
        {
            isPassed = true;
            GameController.Instance.IncrementScore(); // 增加分数
        }
    }
}