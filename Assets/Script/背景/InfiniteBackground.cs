using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float speed = 7f;            // 背景移动速度
    public float backgroundWidth = 26f; // 背景片段的宽度
    public Transform player;            // 玩家或摄像机
    public float destroyDistance = 12f; // 离开摄像机时销毁的距离

    private GameObject[] _backgrounds;   // 用于存储激活的背景片段
    private int _currentIndex = 0;       // 当前背景片段索引

    public Transform backgroundTransform;  // 背景的初始Transform
    private float _bufferZone = 1f; // 背景回收的缓冲区，避免回收过早

    void Start()
    {
        // 初始化背景片段数组并激活四个背景片段
        _backgrounds = new GameObject[4];

        for (int i = 0; i < _backgrounds.Length; i++)
        {
            // 从对象池获取背景片段
            _backgrounds[i] = ObjectPool.Instance.GetObject();
            _backgrounds[i].SetActive(true);

            // 设置背景片段的初始位置
            float xPos = backgroundTransform.position.x + (i * backgroundWidth);
            float yPos = backgroundTransform.position.y; 
            _backgrounds[i].transform.position = new Vector3(xPos, yPos, 0);
        }
    }

    void Update()
    {
        // 背景片段向左移动
        MoveBackground();

        // 检查背景片段是否离开视野并回收
        CheckBackgroundPosition();
    }

    void MoveBackground()
    {
        // 持续移动所有背景片段
        foreach (var bg in _backgrounds)
        {
            bg.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void CheckBackgroundPosition()
    {
        // 检查当前索引背景片段是否离开视野
        if (_backgrounds[_currentIndex].transform.position.x <= -backgroundWidth - _bufferZone)
        {
            // 将当前背景片段回收到池中
            ObjectPool.Instance.ReturnObject(_backgrounds[_currentIndex]);

            // 重新设置当前背景片段的位置为最右侧
            int nextIndex = (_currentIndex + 1) % _backgrounds.Length;
            float newPosX = _backgrounds[(_currentIndex + 3) % _backgrounds.Length].transform.position.x + backgroundWidth; // 获取最后一个背景的位置并延伸
            float newPosY = _backgrounds[(_currentIndex + 1) % _backgrounds.Length].transform.position.y;
            _backgrounds[_currentIndex].transform.position = new Vector3(newPosX, newPosY, 0);
            
            // 激活当前背景片段，并更新索引
            _backgrounds[_currentIndex].SetActive(true);
            _currentIndex = nextIndex;
        }
    }
}
