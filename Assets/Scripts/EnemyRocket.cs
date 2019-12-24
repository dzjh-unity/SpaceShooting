using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/EnemyRocket")]

public class EnemyRocket : Rocket
{

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
