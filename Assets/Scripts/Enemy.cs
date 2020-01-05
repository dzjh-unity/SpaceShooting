using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/Enemy")]

public class Enemy : MonoBehaviour
{

    public float m_speed = 1; // 速度

    public float m_life = 10; // 生命值

    protected float m_rotSpeed = 30; // 旋转速度

    internal Renderer m_renderer; // 模型渲染组件

    internal bool m_isActive = false; // 是否激活

    protected AudioSource m_audio; // 声音源
    public Transform m_explosionFX; // 爆炸特效

    public int m_point = 10; // 攻击力

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>();
        m_audio = this.GetComponent<AudioSource>(); // 获取声音源组件
    }

    void OnBecameVisible() { // 当模型进入可视范围时触发
        m_isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        if (m_isActive && !this.m_renderer.isVisible) {
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateMove() {
        // 左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime * 2;
        // 前进（-z方向）
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerRocket") {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null) {
                m_life -= rocket.m_power;
                if (m_life <= 0) {
                    GameManager.Instance.AddScore(m_point); // 更新分数值UI
                    // 播放爆炸特效
                    Instantiate(m_explosionFX, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        } else if (other.tag == "Player") {
            m_life = 0;
            // 播放爆炸特效
            Instantiate(m_explosionFX, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
