//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Scr_LevelEditor : MonoBehaviour
{
    public SpriteRenderer background;
    public Camera mainCamera;

    [Tooltip("The parent of the instantiated prefabs")]
    public Transform spritesParent;

    public CC_Player player;
    public GameObject trashCan;

    [Header("UI")]
    public GameObject levelNamePanel;
    public GameObject levelNameDisplayPanel;
    public Transform levelNameHolder;
    public GameObject levelNamePrefab;


    private string fileName = "LevelLoader.txt";

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }


    public void ChangeBackground(Sprite sprt)
    {
        background.sprite = sprt;
        mainCamera.backgroundColor = Color.black;
    }

    public void BackgroundColor(Image img)
    {
        Color color = img.color;
        background.sprite = null;
        if (color != null)
        {
            mainCamera.backgroundColor = color;
        }
    }
    
    //Instantiates a sprite (block) into the world
    public void CreateSprite(GameObject prefab)
    {
        GameObject sprt = Instantiate(prefab);
        sprt.transform.SetParent(spritesParent, false);
        sprt.transform.SetAsLastSibling();
    }

    //Init Game
    public void PlayGame()
    {
        //encontrar player y prenderle la gravedad
        player.PlayGame();
        trashCan.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OpenSavingMenu()
    {
        levelNamePanel.SetActive(true);
    }

    public void OpenLoadLevels()
    {
        levelNameDisplayPanel.SetActive(true);
        TextReader textReader = new StreamReader(fileName);
        if(File.Exists(fileName))
        {
            foreach(string line in File.ReadAllLines(fileName))
            {
                if (line.Contains("Level:"))
                {
                    //Take name out of string
                    string[] res = line.Split('&');
                    if (res.Length > 0)
                    {
                        string name = res[0].Remove(0, 6); //Remove the "Level:" from name
                        //Make into UI
                        GameObject temp = Instantiate(levelNamePrefab, levelNameHolder);
                        temp.GetComponentInChildren<Text>().text = name;
                        temp.GetComponent<Button>().onClick.AddListener(() => OpenLevel(name));
                    }                    
                }
                else
                    break;
            }
        }
    }

    public void CloseLevelNameDisplay()
    {
        //delete current btns
        foreach (Transform child in levelNameHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //hide panel
        levelNameDisplayPanel.SetActive(false);

    }

    public void OpenLevel(string _name)
    {
        CloseLevelNameDisplay();

        //Read file with name
        TextReader textReader = new StreamReader(fileName);
        if (File.Exists(fileName))
        {
            foreach (string line in File.ReadAllLines(fileName))
            {
                //get all pieces
                if (line.Contains("Level:"))
                {
                    //Take name out of string
                    string[] res = line.Split('&');
                    if (res.Length > 0)
                    {
                        string name = res[0].Remove(0, 6); //Remove the "Level:" from name
                        //Si si es el mismo nombre, entonces lo encontramos
                        if(_name == name)
                        {
                            //Empieza en 1 porque el 0 es el nombre del nivel
                            for(int k = 1; k < res.Length; k++)
                            {
                                string[] component = res[k].Split(',');
                                //find prefab with that name
                                Debug.Log("Component " + component[0] + " " + component[1]);
                            }

                            break;
                        }
                    }
                }
            }
        }
    }

    public void SaveLevel()
    {
        if(!string.IsNullOrEmpty(levelNamePanel.transform.Find("InputField").GetComponent<InputField>().text))
            WriteOnFile(levelNamePanel.transform.Find("InputField").GetComponent<InputField>().text);
    }

    public void CancelSave()
    {
        levelNamePanel.transform.Find("InputField").GetComponent<InputField>().text = "";
        levelNamePanel.SetActive(false);
    }

    private bool CheckIfLevelNameExistsOnFile(string _levelName)
    {
        bool result = false;
        TextReader textReader = new StreamReader(fileName);
        if(File.Exists(fileName))
        {
            foreach (string line in File.ReadAllLines(fileName))
            {
                if (line.Contains(_levelName))
                {
                    result = true;
                    break;
                }
            }
        }

        textReader.Close();
        return result;
    }

    private void WriteOnFile(string _levelName)
    {
        //get the name of the level
        if(!CheckIfLevelNameExistsOnFile(_levelName))
        {
            string levelInfo = "Level:" + _levelName + "&";
            //Read all objects under sprite parents
            for (int i = 0; i < spritesParent.childCount; i++)
            {
                //save their names minus () and positions
                string name = spritesParent.GetChild(i).name;
                if(name.Contains("("))
                {
                    int index = name.Length - 1;
                    for(int n = 0; n < name.Length; n++)
                    {
                        if (name[n].Equals('('))
                        {
                            index = n;
                            break;
                        }

                    }
                    name = name.Remove(index);
                }
                levelInfo += "&" + name + ",pos:" + spritesParent.GetChild(i).position;
            }

            //look for player and save position: theyre also nested under sprites parent
            //look for finish line and save position
            //save background
            string background = "";
            if (GameObject.Find("Sprite_Background").GetComponent<SpriteRenderer>().sprite != null)
                background = "sprite:" + GameObject.Find("Sprite_Background").GetComponent<SpriteRenderer>().sprite.name;
            else
                background = "color:" + GameObject.Find("Sprite_Background").GetComponent<SpriteRenderer>().color.ToString();
            levelInfo += "*BG:" + background;

            TextWriter textWriter = new StreamWriter(fileName, true);
            textWriter.WriteLine(levelInfo);
            textWriter.Close();
        }

        CancelSave();

    }

    

}
