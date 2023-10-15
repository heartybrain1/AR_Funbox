using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public static PlayerData playerData;

  
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }
    
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath +"/playerData.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/playerData.dat";
        Debug.Log(path);
        if(File.Exists(path))
        {
            Debug.Log("load");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            playerData= formatter.Deserialize(stream) as PlayerData;
            stream.Close();
        }
        else
        {
            Debug.Log("save");
            createPlayerData();
            Save();
        }
    }
    public void createPlayerData()
    {
       
        playerData = new PlayerData();

        playerData.hp = 100;
        playerData.mana = 100;
        playerData.manaRatio = 0.02f;
        playerData.levelCleared = new bool[4, 30];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 30; j++)
            {

                playerData.levelCleared[i, j] = false;

            }
        }
        playerData.levelCleared[0, 0] = true;


        playerData.ownedGECKO = new bool[4, 30, 5];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    playerData.ownedGECKO[i, j, k] = false;
                }
            }
        }

        playerData.deathPoints = new PlayerData.DeathPoint[3, 4, 30];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 30; k++)
                {
                    playerData.deathPoints[i, j, k].Distance = 0;


                    playerData.deathPoints[i, j, k].seq = 0;
                    playerData.deathPoints[i, j, k].posX = 0;
                    playerData.deathPoints[i, j, k].posY = 0;
                    playerData.deathPoints[i, j, k].posZ = 0;

                    playerData.deathPoints[i, j, k].rotX = 0;
                    playerData.deathPoints[i, j, k].rotY = 0;
                    playerData.deathPoints[i, j, k].rotZ = 0;
                    playerData.deathPoints[i, j, k].rotW = 0;

                }
            }
        }

        playerData.coinTotal = 0;
        playerData.totalScore1 = 0;
        playerData.totalScore2 = 0;

       
       

        playerData.activatedGECKO = new bool[20];
        playerData.activatedGECKO[0] = true;
        for (int i = 1; i < 20; i++)
        {
            playerData.activatedGECKO[i] = false;
        }



        playerData.itemStep = new int[12];
        for (int i = 0; i < 12; i++)
        {
            playerData.itemStep[i] = 0;
        }

        playerData.itemTime = new float[12];
        for (int i = 0; i < 12; i++)
        {
            playerData.itemTime[i] = 3;
        }

        playerData.ownedGecko = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            playerData.ownedGecko[i] = false;
        }
        playerData.ownedGecko[0] = true;


        playerData.ownedTree = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            playerData.ownedTree[i] = false;
        }
        playerData.ownedTree[0] = true;


        playerData.ownedAnimal = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            playerData.ownedAnimal[i] = false;
        }


        
    }
}
