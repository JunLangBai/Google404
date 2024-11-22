using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 粒子效果自适应
/// </summary>
public class UIParticleScale : MonoBehaviour
{
    //自定义结构体
    struct ScaleData
    {
        public Transform transform;
        public Vector3 beginScale;
    }
    
    //开发时分辨率的高度和宽度，定义为常量
    const float DESIGN_WIDTH = 1920f;       //宽度
    const float DESIGN_HEIGHT = 1080f;      //高度
    
    private Dictionary<Transform, ScaleData> m_ScaleData = new Dictionary<Transform, ScaleData>();

    void Start()
    {
        Refresh();
    }

    void Refresh()
    {
            float designScale = DESIGN_WIDTH / DESIGN_HEIGHT;
            float scaleRate = (float)Screen.width/(float)Screen.height;

            foreach (ParticleSystem p in Transform.GetComponentsInChildren<ParticleSystem>(true))
            {
                if (!m_ScaleData.ContainsKey(p.transform))
                {
                    m_ScaleData [p.transform] = new ScaleData(){transform = p.transform, beginScale = p.transform.localScale};
                }
            }

            foreach (var item in m_ScaleData)
            {
                if (scaleRate < designScale)
                {
                    float scaleFactor = scaleRate / designScale;
                    item.Value.transform.localScale = item.Value.beginScale * scaleFactor;
                }
                else
                {
                    item.Value.transform.localScale = item.Value.beginScale;
                }
            }
    }
    
    //子节点发生变化时重新刷新深度
    private void OnTransformChildrenChanged()
    {
        Refresh();
    }
    
    #if UNITY_EDITOR
    //编辑模式下修改分辨率后在UpDate（）中刷新
    private void Update()
    {
        Refresh();
    }
    #endif
}
