using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 支持 UI 图片
using TMPro; // 支持 TextMeshPro 文本

/// <summary>
/// UI 图片和文本自适应缩放
/// </summary>
public class UIScale : MonoBehaviour
{
    // 自定义结构体，记录初始缩放数据
    struct ScaleData
    {
        public RectTransform rectTransform;
        public Vector2 beginSize; // 初始尺寸
        public Vector3 beginScale; // 初始缩放
    }

    // 开发时分辨率
    const float DESIGN_WIDTH = 1920f;
    const float DESIGN_HEIGHT = 1080f;

    private Dictionary<RectTransform, ScaleData> m_ScaleData = new Dictionary<RectTransform, ScaleData>();

    void Start()
    {
        Refresh();
    }

    void Refresh()
    {
        // 开发时和当前屏幕的宽高比
        float designScale = DESIGN_WIDTH / DESIGN_HEIGHT;
        float screenScale = (float)Screen.width / (float)Screen.height;

        // 遍历当前对象及其所有子对象中的 UI 图片和文本
        foreach (RectTransform rectTransform in GetComponentsInChildren<RectTransform>(true))
        {
            // 判断是否是图片或文本
            if (rectTransform.GetComponent<Image>() || rectTransform.GetComponent<TextMeshProUGUI>())
            {
                // 如果尚未记录初始数据，则存入字典
                if (!m_ScaleData.ContainsKey(rectTransform))
                {
                    m_ScaleData[rectTransform] = new ScaleData()
                    {
                        rectTransform = rectTransform,
                        beginSize = rectTransform.sizeDelta, // 记录初始尺寸
                        beginScale = rectTransform.localScale // 记录初始缩放
                    };
                }
            }
        }

        // 根据屏幕宽高比调整缩放
        foreach (var item in m_ScaleData)
        {
            if (screenScale < designScale)
            {
                // 屏幕宽高比小于设计比，按比例缩放
                float scaleFactor = screenScale / designScale;
                item.Value.rectTransform.localScale = item.Value.beginScale * scaleFactor;
            }
            else
            {
                // 宽高比正常，保持原始比例
                item.Value.rectTransform.localScale = item.Value.beginScale;
            }
        }
    }

    // 监听子对象变化
    private void OnTransformChildrenChanged()
    {
        Refresh();
    }

#if UNITY_EDITOR
    // 编辑器模式下，分辨率调整时自动刷新
    private void Update()
    {
        Refresh();
    }
#endif
}
