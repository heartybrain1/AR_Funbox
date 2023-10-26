using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuTheme : MonoBehaviour
{
    

    private void OnEnable()
    {
        GameManager.indexTheme = 0;
        GameManager.indexLevel = 0;
        for (int i = 0; i < 5; i++)
        {
            int index = i;
            transform.GetChild(1).GetChild(i).GetComponent<Button>().onClick.AddListener(() => OnSelectTheme(index));

        }


        for (int i = 0; i < 5; i++)
        {
            int index = i;
            Sprite sprite = getSprite(index);
            transform.GetChild(2).GetChild(index).GetComponent<Button>().image.sprite = sprite;
            transform.GetChild(2).GetChild(index).GetComponent<Button>().onClick.AddListener(() => OnSelectLevel(index));

        }

        //themeType indexTheme = (themeType)GameManager.indexTheme;
        // transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = indexTheme.ToString();


        transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => OnSelectBack());
        transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => OnSelectPlay());

    }
   


    void OnSelectTheme(int indexTheme)
    {
        GameManager.indexTheme = indexTheme;


        for (int i = 0; i < 5; i++)
        {
            int index = i;
            Sprite sprite = getSprite(index);
            transform.GetChild(2).GetChild(index).GetComponent<Button>().image.sprite = sprite;
            transform.GetChild(2).GetChild(index).GetComponent<Button>().onClick.AddListener(() => OnSelectLevel(index));

        }


    }
    void OnSelectLevel(int indexLevel)
    {
        GameManager.indexLevel = indexLevel;
    }

    void OnSelectBack()
    {
        transform.parent.GetChild(0).gameObject.SetActive(true);
        resetMenu();
        GameManager.indexTheme = -1;
        GameManager.indexLevel = -1;
        gameObject.SetActive(false);

    }
    void OnSelectPlay()
    {
        if (GameManager.Instance.Life > 0)
        {
            GameManager.Instance.Life = GameManager.Instance.Life - 1;
            SaveManager.playerData.life = SaveManager.playerData.life - 1;
            //wait until save finished
            SceneManager.LoadScene("2_Play");
        }
        else
        {
            //pop up " not enough life"
        }

       
    }

   

    void resetMenu()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(2).GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
        }
     
    }

    Sprite getSprite(int indexLevel)
    {
        string path1 = "Sprites/";
        Object[] objs = Resources.LoadAll(path1);

        int count = objs.Length;

        List<string> fileNames = new List<string>();
        for (int i = 0; i < count; i++)
        {

            string str = objs[i].name;
            fileNames.Add(str);
            Debug.Log(str);
        }


        themeType myType = (themeType)GameManager.indexTheme;
        string nameTheme = myType.ToString();
        int index = indexLevel;



        string str1 = nameTheme;
        string str2 = "theme";
        string result = str1.Replace(str2, "");

        string prefix = result + "_" + index.ToString();


        string namePrefab = "";
        string path2 = "";
        for (int i = 0; i < count; i++)
        {

            if (fileNames[i].Contains(prefix))
            {
                namePrefab = fileNames[i];

                break;


            }

        }


        path2 = "Sprites/" + namePrefab;
        Debug.Log(namePrefab);
        Debug.Log(path2);
        Sprite sprite = Resources.Load<Sprite>(path2);
        Debug.Log(sprite);
        return sprite;
    }

}
