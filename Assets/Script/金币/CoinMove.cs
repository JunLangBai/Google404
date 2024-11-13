using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float moveSpeed = 5f;  // 金币的移动速度

    private void Start()
    {
        moveSpeed = GameController.Instance.levelSpeed;
    }

    private void Update()
    {
        TakeCoinMove();
    }

    //金币移动
    public void TakeCoinMove()
    {
        // 每帧向左移动，改变 x 坐标
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
