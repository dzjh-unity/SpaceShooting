using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/EnemyRenderer")]

public class EnemyRenderer : MonoBehaviour
{

    protected Enemy m_enemy;

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = this.GetComponentInParent<Enemy>(); // 从父物体获得Enemy脚本
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameVisible() { // 当模型进入可视范围时触发
        m_enemy.m_isActive = true;
        m_enemy.m_renderer = this.GetComponent<Renderer>();
    }
}
