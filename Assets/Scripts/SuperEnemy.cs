using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/SuperEnemy")]

public class SuperEnemy : Enemy
{

    public Transform m_rocket; // 子弹Prefab
    protected Transform m_player; // 主角
    protected float m_fireTimer = 1; // 射击定时器

    protected override void UpdateMove() {
        m_fireTimer -= Time.deltaTime;
        if (m_fireTimer <= 0) {
            m_fireTimer = 1;
            if (m_player != null) {
                // 使用向量减法获取朝向主角位置的方向（目标位置-自身位置）
                Vector3 relativePos = m_player.position - transform.position;
                Debug.Log(relativePos);
                Instantiate(m_rocket, transform.position, Quaternion.LookRotation(relativePos));
            } else {
                GameObject obj = GameObject.FindGameObjectWithTag("Player"); // 查找主角
                if (obj != null) {
                    m_player = obj.transform;
                }
            }
        }
        // 前进（-z方向）
        transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
