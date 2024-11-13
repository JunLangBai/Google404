using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool hasCollected = false;

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
        else if (other.CompareTag("DeathWall"))
        {
            // 销毁当前金币
            Destroy(gameObject);
        }
    }
}