//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_EnemyBase : MonoBehaviour
{
	float health = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            if (health > 0)
            {
                //Kill Player

            }
            else
                KillSelf();
        }
    }

    void KillSelf()
    {
        health = 0;
        Destroy(gameObject);
    }
}
