//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CC_Player : MonoBehaviour
{
    bool isGameOn = false;
    bool isGrounded = true;
    float speed = 3.5f;
    RaycastHit hit;
    Animator myAnim;

    public GameObject playerUI;
    public GameObject gameWonPanel;
    public GameObject gameLostPanel;
    public Text txtGemCounter;

    public GameObject gemPrefab;
    private int gemCounter = 0;

    public bool isOnLevelEditor = true;

    private void Start()
    {
        playerUI.SetActive(false);
        gameLostPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        if (!isOnLevelEditor)
            PlayGame();
    }

    public void PlayGame()
    {
        GetComponent<Rigidbody>().useGravity = true;
        myAnim = GetComponent<Animator>();
        playerUI.SetActive(true);
        isGameOn = true;
    }

    private void Update()
    {
        if(isGameOn)
        {
            if (Input.GetKey(KeyCode.A))
                MoveBackward();
            else if (Input.GetKey(KeyCode.D))
                MoveForward();
            if (Input.GetKeyDown(KeyCode.W))
                Jump();

            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                if (hit.collider.tag == "LE_Box")
                    isGrounded = true;
            }
        }
        
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        myAnim.SetFloat("Walk", 0.5f);
    }

    public void MoveBackward()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
        myAnim.SetFloat("Walk", 0.7f);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * speed * 70);
            myAnim.SetFloat("Walk", 0.0f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(isGameOn)
        {
            if(other.tag == "LE_Box")
            {
                if((other.name.Contains("Box_Gold") || other.name == "Box (Clone)") && other.GetComponent<SpriteRenderer>().color == Color.white)
                {
                    if (Physics.Raycast(transform.position, transform.up, out hit))
                    {
                        //Debug.Log("Physics succeded");
                        if (hit.collider.name == other.name)
                        {
                            other.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);
                            StartCoroutine(CreateGem(other.gameObject));
                        }
                    }
                }
            }
            else if(other.tag == "LE_Finish")
            {
                GameWon();
            }
            else if(other.tag == "LE_Void")
            {
                GameLost();
            }
            else if(other.tag == "LE_Enemy")
            {
                Debug.Log("Enemigos");
            }
            else if(other.tag == "Gem")
            {
                Destroy(other.gameObject);
                gemCounter++;
                txtGemCounter.text = gemCounter.ToString();
            }
        }
    }

    IEnumerator CreateGem(GameObject other)
    {
        yield return new WaitForSeconds(0.35f);
        //Instantiate Price
        GameObject temp = GameObject.Instantiate(gemPrefab);
        temp.tag = "Untagged";
        temp.transform.position = other.transform.position - new Vector3(0.2f, 1.0f, 0.0f);
        StartCoroutine(ChangeGemTag(temp));
    }

    IEnumerator ChangeGemTag(GameObject gem)
    {
        yield return new WaitForSeconds(0.15f);
        gem.tag = "Gem";
    }

    public void GameWon()
    {
        Debug.Log("Won Game");
        gameWonPanel.SetActive(true);
        gameWonPanel.transform.Find("txtGemCounter").GetComponent<Text>().text = gemCounter.ToString();
        StartCoroutine(WaittoLoadMenu());
    }

    public void GameLost()
    {
        Debug.Log("Lost Game");
        gameLostPanel.SetActive(true);
        StartCoroutine(WaittoLoadMenu());
    }

    IEnumerator WaittoLoadMenu()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<CC_SceneManager>().OpenMainMenu();
    }

}
