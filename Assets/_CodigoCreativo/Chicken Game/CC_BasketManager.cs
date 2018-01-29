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

    KinectManager manager;
    private void Start()
    {
        manager = KinectManager.Instance;
    }

    private void Update()
    {
        SetBasketPos();
    }

    void SetBasketPos()
    {
        //checar si no hubi problemas de inicializacion
        if (manager != null && manager.IsUserDetected())
        {
            uint userID = manager.GetPlayer1ID();
            Vector3 posUser = manager.GetUserPosition(userID);
            transform.position = new Vector3(posUser.x * speed, transform.position.y, posUser.z);
        }

        else
        {
            StartCoroutine(CheckIfPlayerDetected());
        }
    }

    IEnumerator CheckIfPlayerDetected()
    {
        yield return new WaitForSeconds(5.0f);
        if(!manager.IsUserDetected())
        {
            FindObjectOfType<CC_SceneManager>().OpenMainMenu();
        }
    }


    public void AddChicken(Transform other)
    {
        chickenCounter++;
        txtChickenCounter.text = chickenCounter.ToString();
        other.GetComponent<CC_Chicken>().isInBasket = true;
        other.parent = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chicken")
        {
            Debug.Log("WE'VE CAUGHT A CHICKEN!");

            AddChicken(other.transform);
            //acumular

           // other.GetComponent<Rigidbody>().
            //Destroy(other.gameObject);
        }
    }
}
