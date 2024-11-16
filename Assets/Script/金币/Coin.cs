using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool hasCollected = false;
    public float maxDistance = 10f; // 最大距离阈值
    private Transform player;

    // 当脚本启动时初始化玩家引用
    private void Start()
    {
        // 查找场景中的玩家对象
        player = GameObject.FindWithTag("Player").transform;
    }

    // 每帧检查金币与玩家的距离
    private void Update()
    {
        DestroyCoin();
    }

    // 当玩家进入金币的碰撞体时触发
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保触发的是玩家，而不是其他物体（如地面检测碰撞器）
        if (other.CompareTag("Player") && !hasCollected)
        {
            // 标记为已收集
            hasCollected = true;

            // 禁用金币的碰撞体，防止后续触发
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false; // 禁用碰撞体
            }

            // 调用 GameController 收集金币
            GameController.Instance.CollectCoin();

            // 销毁金币
            Destroy(gameObject);
        }
    }
    
    private void DestroyCoin()
    {
        if (player != null && !hasCollected)
        {
            // 计算金币与玩家之间的距离
            float distance = Vector2.Distance(transform.position, player.position);
            
            // 如果距离超过最大值，则销毁金币
            if (distance > maxDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}