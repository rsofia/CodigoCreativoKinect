//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_ChickenMadness : MonoBehaviour
{
    public Transform chickenPrefab;

    private float timeToWait = 1.0f;    //tiempo que se tarda en instanciar 
    private uint userID = 0;             // el id del usuario en el kinect

    private void Start()
    {
        //Empezar a instanciar desde el inicio
        StartCoroutine(WaitToSpawn());
    }
    

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(timeToWait);
        SpawnChicken();
        //volver a llamar para que se haga un loop
        StartCoroutine(WaitToSpawn());
    }

    void SpawnChicken()
    {
        KinectManager manager = KinectManager.Instance;

        //Si el kinect fue inicializado, el pollo no es nulo y hay algun usuario dete
        if(manager != null && chickenPrefab != null && manager.IsUserDetected())
        {
            userID = manager.GetPlayer1ID();
        }

        if (chickenPrefab && manager && manager.IsInitialized() && manager.IsUserDetected())
        {
            uint userId = manager.GetPlayer1ID();
            Vector3 posUser = manager.GetUserPosition(userId);

            float addXPos = Random.Range(-10f, 10f);
            Vector3 spawnPos = new Vector3(addXPos, 10f, posUser.z);

            Transform eggTransform = Instantiate(chickenPrefab, spawnPos, Quaternion.identity) as Transform;
            eggTransform.parent = transform;
        }
    }
}
