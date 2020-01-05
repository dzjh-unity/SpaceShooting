using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("GmaeScript/GameManager")]

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; // 静态实例
    public Transform m_canvas_main; // 分数界面
    public Transform m_canvas_gameover; // 游戏失败界面
    public Text m_text_score; // 分数文字
    public Text m_text_hp; // 生命值文字

    protected int m_score = 0; // 分数值
    protected Player m_player; // 主角实例

    public AudioClip m_musicClip; // 背景音乐
    protected AudioSource m_Audio; // 声音源

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_Audio = this.gameObject.AddComponent<AudioSource>(); // 使用代码添加声音源组件
        m_Audio.clip = m_musicClip; // 指定背景音乐
        m_Audio.loop = true; // 设置声音循环播放
        m_Audio.Play(); // 开始播放声音
        // 通过Tag查找主角
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // 获得UI控件
        m_text_score = m_canvas_main.transform.Find("Score").GetComponent<Text>();
        m_text_hp = m_canvas_main.transform.Find("HP").GetComponent<Text>();
        m_text_score.text = string.Format("分数 {0}", m_score);
        m_text_hp.text = string.Format("生命值 {0}", m_player.m_life);
        // 获取重新开始游戏按钮
        var restart_btn = m_canvas_gameover.transform.Find("RestartBtn").GetComponent<Button>();
        restart_btn.onClick.AddListener(delegate() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新开始当前关卡
        });
        m_canvas_gameover.gameObject.SetActive(false); // 默认隐藏游戏失败UI
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int point) {
        m_score += point;
        m_text_score.text = string.Format("分数 {0}", m_score);
    }

    public void ChangeHP(int hp) {
        m_text_hp.text = string.Format("生命值 {0}", hp); // 更新生命值
        if (hp <= 0) {
            m_canvas_gameover.gameObject.SetActive(true); // 生命值为0，显示游戏失败UI
        }
    }
}
