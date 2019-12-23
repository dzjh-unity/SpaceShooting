﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/Player")]

public class Player : MonoBehaviour
{
    public float m_speed = 1;

    public Transform m_rocket;

    public float m_rocketTimer = 0;

    public float m_life = 3;

    // Start is called before the first frame update
    void Start()
    {

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
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
                Instantiate(m_rocket, this.transform.position, this.transform.rotation);
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
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "PlayerRocket") {
            m_life -= 1;
            if (m_life <- 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
