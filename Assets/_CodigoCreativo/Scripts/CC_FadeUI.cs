//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_FadeUI : MonoBehaviour
{
    private float maxScale = 1.5f;
    private float minScale = 1.0f;
    private float speed = 80;
    bool makeSmall = false;

    private void Update()
    {
        if(makeSmall)
        {
            if (transform.localScale.x > minScale)
                transform.localScale -= Vector3.one / speed;
            else
                makeSmall = false;
        }
        else
        {
            if (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one / speed;
            }
            else
                makeSmall = true;
        }
        
    }
}
