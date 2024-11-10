using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public PlayerMove playerMove; // 引用PlayerMove组件
    public Text speedText;        // UI文本对象，用于显示速度

    void Update()
    {
        if (playerMove != null && speedText != null)
        {
            float speed = playerMove.GetCurrentSpeed();
            speedText.text = "速度: " + speed.ToString("F1"); // 显示速度，保留两位小数
        }
    }
}
