using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//游戏ui显示
public class PlayerDisplay : MonoBehaviour
{
    void Update()
    {
        if (GameController.Instance.playerMove != null && GameController.Instance.speedText != null)
        {
            GameController.Instance.SpeedUp();
        }
    }
}
