//Code by Luis Bazan
//Github user: luisquid11

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_Chicken : MonoBehaviour {

    public bool isInBasket = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Chicken") )
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if(collision.collider.GetComponent<CC_Chicken>().isInBasket && !isInBasket)
            {
                FindObjectOfType<CC_BasketManager>().AddChicken(transform);
            }
        }
    }
}
