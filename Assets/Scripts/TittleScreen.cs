using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("GmaeScript/TittleScreen")]
public class TittleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonGameStart() {
        SceneManager.LoadScene("Level1"); // 读取关卡Level1
    }
}
