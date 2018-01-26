//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_EndlessVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Si es un pollo, entonces significa que ya cayo demasiado y hay que destruirlo
        if (other.tag == "Chicken")
            Destroy(other.gameObject);
    }
}
