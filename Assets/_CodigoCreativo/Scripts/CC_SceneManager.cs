//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_SceneManager : MonoBehaviour
{
    public GameObject loadingPanel;
    private float secondsToWait = 1.7f;

    private void Start()
    {
        if (loadingPanel.activeSelf)
            loadingPanel.SetActive(false);
    }

    public void OpenGameScene()
    {
        OpenScene("ChickenGame");
    }

    public void OpenMainMenu()
    {
        OpenScene("ChickenMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OpenScene(string _name)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(WaitToOpenScene(_name));
    }

    IEnumerator WaitToOpenScene(string _name)
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(_name);
    }
}
