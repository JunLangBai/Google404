using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // 障碍物预制体数组
    public Transform spawnPoint;          // 障碍物生成点
    public float spawnInterval = 1.5f;      // 生成障碍物的时间间隔
    public float obstacleSpeed = 7f;      // 障碍物移动速度
    public float destroyDistance = 27f;   // 超出视野的距离

    private float spawnTimer;

    void Update()
    {
        // 生成障碍物
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnRandomObstacle();
            spawnTimer = 0;
        }

        // 移动障碍物
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.left * obstacleSpeed * Time.deltaTime);
            if (child.position.x <= -destroyDistance)
            {
                Destroy(child.gameObject); // 回收障碍物或销毁
            }
        }
    }

    void SpawnRandomObstacle()
    {
        // 从数组中随机选择一个障碍物预制体
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedObstaclePrefab = obstaclePrefabs[randomIndex];

        // 实例化选定的障碍物，并设置为当前对象的子对象
        GameObject obstacle = Instantiate(selectedObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        obstacle.SetActive(true);
    }
}