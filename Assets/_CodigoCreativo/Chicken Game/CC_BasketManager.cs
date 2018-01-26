//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_BasketManager : MonoBehaviour
{
    private int chickenCounter = 0;
    private float speed = 3;

    [Header("UI")]
    public Text txtChickenCounter;

    private void Update()
    {
        SetBasketPos();
    }

    void SetBasketPos()
    {
        KinectManager manager = KinectManager.Instance;
        //checar si no hubi problemas de inicializacion
        if (manager != null && manager.IsUserDetected())
        {
            uint userID = manager.GetPlayer1ID();
            Vector3 posUser = manager.GetUserPosition(userID);
            transform.position = new Vector3(posUser.x * speed, transform.position.y, posUser.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chicken")
        {
            Debug.Log("WE'VE CAUGHT A CHICKEN!");
            chickenCounter++;
            txtChickenCounter.text = chickenCounter.ToString();
            //acumular
           // other.GetComponent<Rigidbody>().
            //Destroy(other.gameObject);
        }
    }
}
