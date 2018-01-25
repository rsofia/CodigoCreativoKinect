//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_DeleteSprite : MonoBehaviour
{
    bool canErase = false;

	void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Contains("Player") && !other.tag.Contains("Finish"))
        {
            canErase = true;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            StartCoroutine(WaitToErase(other));
        }        
    }

    IEnumerator WaitToErase(Collider other)
    {
        yield return new WaitForSeconds(2.0f);
        if (canErase)
        {
            Destroy(other.gameObject);
        }
        ResetDelete();
    }

    private void OnTriggerExit(Collider other)
    {
        ResetDelete();
    }

    void ResetDelete()
    {
        canErase = false;
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
}
