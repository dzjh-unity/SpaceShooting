using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/Player")]

public class Player : MonoBehaviour
{
    public float m_speed = 1;

    public Transform m_rocket;

    public float m_rocketTimer = 0;

    public int m_life = 3;

    protected AudioSource m_audio; // 声音源
    public AudioClip m_shootClip; // 射击声音文件
    public Transform m_explosionFX; // 爆炸特效

    protected Vector3 m_targetPos; // 目标位置

    public LayerMask m_inputMask; // 鼠标射线碰撞层

    // Start is called before the first frame update
    void Start()
    {
        m_audio = this.GetComponent<AudioSource>(); // 获取声音源组件
        m_targetPos = this.transform.position; // 初始化当前位置
    }

    // Update is called once per frame
    void Update()
    {
        // 纵向移动距离
        float movev = 0;
        // 水平移动距离
        float moveh = 0;

        m_rocketTimer -= Time.deltaTime;
        if (m_rocketTimer <= 0) {
            m_rocketTimer = 0.1f;
            // 按空格键或鼠标左键发射子弹
            if (Input.GetKey(KeyCode.Space)) {
                Instantiate(m_rocket, this.transform.position, this.transform.rotation);
                // 播放射击音效
                m_audio.PlayOneShot(m_shootClip);
            }
        }

        // 按上键Z方向递增
        if (Input.GetKey(KeyCode.UpArrow)) {
            movev += m_speed * Time.deltaTime;
        }
        // 按上键Z方向递减
        if (Input.GetKey(KeyCode.DownArrow)) {
            movev -= m_speed * Time.deltaTime;
        }
        
        // 按上键X方向递增
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveh += m_speed * Time.deltaTime;
        }
        // 按上键X方向递减
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveh -= m_speed * Time.deltaTime;
        }
        // 移动
        this.transform.Translate(new Vector3(moveh, 0, movev));
        // // 相当于
        // this.transform.position += new Vector3(moveh, 0, movev);

        MoveTo();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "PlayerRocket") {
            m_life -= 1;
            if (other.tag == "Enemy") {
                m_life = 0;
            }
            GameManager.Instance.ChangeHP(m_life); // 更新生命值UI
            if (m_life <= 0) {
                // 播放爆炸特效
                Instantiate(m_explosionFX, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    void MoveTo() {
        if (Input.GetMouseButton(0)) {
            Vector3 ms = Input.mousePosition; // 鼠标相对于屏幕的位置
            Ray ray = Camera.main.ScreenPointToRay(ms); // 将屏幕位置转为射线
            RaycastHit hitInfo; // 射线碰撞信息
            bool iscast = Physics.Raycast(ray, out hitInfo, 1000, m_inputMask); // 其中m_inputMask可替换为LayerMask.GetMask("plane")
            if (iscast) {
                m_targetPos = hitInfo.point; // 射击碰撞点
            }
        }
        // 更新当前位置
        Vector3 pos = Vector3.MoveTowards(this.transform.position, m_targetPos, m_speed * Time.deltaTime);
        this.transform.position = pos;
    }
}
