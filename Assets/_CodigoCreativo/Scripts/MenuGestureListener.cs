//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    KinectManager manager;

    private void Start()
    {
        manager = KinectManager.Instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            FindObjectOfType<CC_SceneManager>().OpenGameScene();

        if(manager.IsUserDetected() && SceneManager.GetActiveScene().name == "ChickenMenu")
        {
            FindObjectOfType<CC_SceneManager>().OpenGameScene();
        }
    }

    public void UserDetected(uint userId, int userIndex)
    {
        FindObjectOfType<CC_SceneManager>().OpenGameScene();

        KinectManager manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        manager.DetectGesture(userId, KinectGestures.Gestures.Click);
        manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
    }

    public void UserLost(uint userId, int userIndex)
    {

    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        Debug.Log("Gesture " + gesture + " in progress");
        if (gesture == KinectGestures.Gestures.Jump)
        {
            FindObjectOfType<CC_SceneManager>().OpenGameScene();
            Debug.Log("JUMPING");
        }
    }
    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        Debug.Log("Gesture " + gesture + " completed");
        if (gesture == KinectGestures.Gestures.Jump)
        {
            FindObjectOfType<CC_SceneManager>().OpenGameScene();
            Debug.Log("JUMPING COMPLETED");
        }
        else if(gesture == KinectGestures.Gestures.Wave)
        {
            FindObjectOfType<CC_SceneManager>().QuitGame();
        }
            return true;
    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        return true;
    }
}
