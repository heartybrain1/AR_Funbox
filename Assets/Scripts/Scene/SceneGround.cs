using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGround : SceneLoader
{
    public int indexTheme;
    public int indexLevel;

    GameObject gamePrefab;
    protected override void sceneLoad()
    { /*
        string path = "Prefabs/" + "Animal_5_Crocodile";
        GameObject gamePrefab = Resources.Load(path) as GameObject;
        GameObject obj = Instantiate(gamePrefab);
       */

        string path1 = "Prefabs/";
        Object[] objs = Resources.LoadAll(path1);

        int count = objs.Length;

        List<string> fileNames = new List<string>();
        for (int i = 0; i < count; i++)
        {
           
            string str = objs[i].name;
            fileNames.Add(str);
            Debug.Log(str);
        }


        themeType myType = (themeType)indexTheme;
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


        path2 = "Prefabs/" + namePrefab;
        Debug.Log(namePrefab);
        Debug.Log(path2);
        gamePrefab = Resources.Load(path2) as GameObject;
      

  
    }

    public void spawnObj()
    {
        Instantiate(gamePrefab);
    }

    public void Home()
    {

    }

}
