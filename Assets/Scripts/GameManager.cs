using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using FluffyUnderware.Curvy.Controllers;
//using GeneratedNS;


//using UnityEngine.Rendering.Universal;

public enum playType { type_0_bra, type_1_pigeon, type_2_trex, type_3_stork, type_4_zombie, type_5_spider, type_6_bear, type_7_frog, type_8_elephant, type_9_crocodile, type_10_ski, type_11_raptor, type_12_cat }
public enum fruitTYPE { ApricotB, AvocadoB, BelunuB, BilimbiB, BisbulB, TamarinA };
public enum speechTYPE{ speech_eat, speech_steak, speech_surprise };
public enum entryTYPE { fromLOAD, fromHOME, fromDIE, fromWIN, fromCANCEL, fromRESTART, fromLevel, fromBonus, fromTree };
public class GameManager : MonoBehaviour
{
   // public static E_level levelData;
   // public static E_ability abilityData;

  //  public weatherTYPE myweatherTYPE;s
   // public treeskinTYPE mytreeskinTYPE;

    public entryTYPE myentryTYPE = entryTYPE.fromLOAD;
    public static GameManager Instance;

    public static bool isPlaying = false;
  
    public List<GameObject> prefabTrees;
    public List<GameObject> prefabBranches;
    public List<GameObject> prefabSBranches;
    public List<GameObject> prefabAnimals;
    public List<GameObject> prefabItems;
    public List<GameObject> prefabWords;
    public List<GameObject> prefabPickup;
    public List<GameObject> prefabItemVFX;
    public List<GameObject> prefabVFX;
    public List<GameObject> prefabSpeeches;
    public List<GameObject> prefabUIm;
    public List<GameObject> prefabUIb;
    public List<GameObject> prefabEnvir;
    public List<AudioClip> prefabAudioClips;
    public List<GameObject> prefabCheckers;
    public List<GameObject> prefabStructures;

   
    public static int indexPlaymode;
    public static int indexTheme;
    public static int indexLevel;


    public float tmp_delayTimeOffset;
    public float tmp_geckoSpeed;


    float m_Time;

    float m_Life;
    public float Life { get { return m_Life; } set { m_Life = value; } }

    private int m_Seq;
    public int Seq { get { return m_Seq; } set { m_Seq = value; } }

    private int m_tSeq;
    public int tSeq { get { return m_tSeq; } set { m_tSeq = value; } }

    private int m_Score;
    public int Score { get { return m_Score; } set { m_Score = value; } }

    private int m_Item;
    public int Item { get { return m_Item; } set { m_Item = value; } }

    private int m_Jump;
    public int Jump { get { return m_Jump; } set { m_Jump = value; } }

    float m_Distance;
    public float Distance { get { return m_Distance; } set { m_Distance = value; } }

    float m_TotalDistance;
    public float TotalDistance { get { return m_TotalDistance; } set { m_TotalDistance = value; } }

    int m_Coin;
    public int Coin { get { return m_Coin; } set { m_Coin = value; } }





    float m_HP;
    public float HP { get { return m_HP; } set { m_HP = value; } }

    float m_TotalHP;
    public float TotalHP { get { return m_TotalHP; } set { m_TotalHP = value; } }

    float m_Mana;
    public float Mana { get { return m_Mana; } set { m_Mana = value; } }

    float m_TotalMana;
    public float TotalMana { get { return m_TotalMana; } set { m_TotalMana = value; } }

    float m_ManaRatio;
    public float ManaRatio { get { return m_ManaRatio; } set { m_ManaRatio = value; } }



    bool[] m_OwendGECKO = new bool[5];
    public bool[] OwendGECKO { get { return m_OwendGECKO; } set { m_OwendGECKO = value; } }


    private bool m_Cleared;
    public bool Cleared { get { return m_Cleared; } set { m_Cleared = value; } }


    private Vector3 m_DPposition;
    public Vector3 DPposition { get { return m_DPposition; } set { m_DPposition = value; } }

    private Quaternion m_DProtation;
    public Quaternion DProtation { get { return m_DProtation; } set { m_DProtation = value; } }



    private void Awake()
    {

        Instance = this;
        DontDestroyOnLoad(gameObject);
       // loadPrefabs();

     
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        //QualitySettings.SetQualityLevel(1,true);
      
    }
    
    public void initializeLevel()
    {
        Debug.Log("playmode is" + indexPlaymode);
        Debug.Log("theme is" + indexTheme);
        Debug.Log("level is" + indexLevel);

      //  E_Theme entity0 = E_Theme.GetEntity(indexTheme-1);
     //   List<E_level> listLevelData = entity0.f_level;
      //  levelData = listLevelData[indexLevel];

    }
   

    public void initializeSingleRunThrough()
    {
       
  //      E_Theme entity0 = E_Theme.GetEntity(0);

    }




    public void initializeWeather()
    {
     //   myweatherTYPE = (weatherTYPE)levelData.f_weatherTYPE;
       

      //  string path = "Prefabs/skybox/" + myweatherTYPE;
        //Material prefab = Resources.Load<Material>(path);
       // RenderSettings.skybox = prefab;

      //  string path2 = "Prefabs/skybox/skybox_" + myweatherTYPE;
       // GameObject prefab2 = Resources.Load<GameObject>(path2);
       // GameObject skybox = Instantiate(prefab2);

    }

    public void initializeTreeSkin()
    {
       /*
        mytreeskinTYPE = (treeskinTYPE)levelData.f_treeskinTYPE;

        string path1M = "material/tree_common_bark";
        string path1T = "textures/" + mytreeskinTYPE.ToString() + "_bark";
        Material mat_bark = Resources.Load<Material>(path1M);
        Texture texture_bark = Resources.Load<Texture>(path1T);
        Debug.Log(path1M);
        Debug.Log(path1T);
        Debug.Log(mat_bark);
        Debug.Log(texture_bark);
        mat_bark.SetTexture("_BaseMap", texture_bark);
      
        string path2M = "material/tree_common_branch";
        string path2T = "textures/" + mytreeskinTYPE.ToString() + "_branch";
        Material mat_branch = Resources.Load<Material>(path2M);
        Texture texture_branch = Resources.Load<Texture>(path2T);
        mat_branch.SetTexture("Texture2D_6FF0EC81", texture_branch);

        string path3M = "material/tree_common_log";
        string path3T = "textures/" + mytreeskinTYPE.ToString() + "_log";
        Material mat_log = Resources.Load<Material>(path3M);
        Texture texture_log = Resources.Load<Texture>(path3T);
        mat_log.SetTexture("_BaseMap", texture_log);

        string path4M = "material/tree_common_leaf";
        string path4T = "textures/" + mytreeskinTYPE.ToString() + "_leaf";
        Material mat_leaf = Resources.Load<Material>(path4M);
        Texture texture_leaf = Resources.Load<Texture>(path4T);
        mat_leaf.SetTexture("_BaseMap", texture_leaf);

        */
    }


    public void initializeGeckoData()
    {
        int indexGecko = SaveManager.playerData.currentGecko;
       // abilityData = E_ability.GetEntity(indexGecko);

        isPlaying = false;
        m_Seq = 0;
        m_tSeq = 0;
        m_Score = 0;
        m_Distance = 0;
       // m_TotalDistance = levelData.f_distance;
        m_Coin = 0;
        m_Item = 0;
        m_Jump = 3;



      //  m_HP = SaveManager.playerData.hp + abilityData.f_plusHP;
      //  m_TotalHP= SaveManager.playerData.hp + abilityData.f_plusHP;
        m_Mana = 0;
        m_TotalMana = SaveManager.playerData.mana;
        m_ManaRatio = SaveManager.playerData.manaRatio;
        m_Cleared = SaveManager.playerData.levelCleared[indexTheme, indexLevel];
        m_OwendGECKO[0] = SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 0];
        m_OwendGECKO[1] = SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 1];
        m_OwendGECKO[2] = SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 2];
        m_OwendGECKO[3] = SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 3];
        m_OwendGECKO[4] = SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 4];

        Debug.Log(m_OwendGECKO[0]);
        Debug.Log(m_OwendGECKO[1]);
        Debug.Log(m_OwendGECKO[2]);
        Debug.Log(m_OwendGECKO[3]);
        Debug.Log(m_OwendGECKO[4]);


        float poX= SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].posX;
        float poY = SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].posY;
        float poZ = SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].posZ;
        m_DPposition = new Vector3(poX, poY, poZ);

        float rotX = SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotX;
        float rotY = SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotY;
        float rotZ = SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotZ;
        float rotW = SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotW;
        m_DProtation = new Quaternion(rotX, rotY, rotZ, rotW);
    }

    /*
    public IEnumerator spawnGecko()
    {/*
        if(myentryTYPE==entryTYPE.fromLevel)
        {
            yield return new WaitUntil(() => BonusGenerator.Instance.initialized == true);
        }
        else
        {
            Debug.Log("spawn");
            yield return new WaitUntil(() => TreeGenerator.Instance.initialized == true);
        }

        int indexGecko = SaveManager.playerData.currentGecko;
        string path = "Prefabs/player/Player" + indexGecko.ToString();

        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject Player = Instantiate(prefab);
        Player.transform.GetChild(0).GetChild(0).GetComponent<GeckoController>().setInitialGeckoController();
        Player.transform.GetChild(0).GetChild(0).GetComponent<GeckoController>().mygeckoSTATE = GeckoController.geckoSTATE.geckoWALK;

        Camera cam = Player.transform.GetChild(1).GetChild(0).GetComponent<Camera>();
        var cameraData = cam.GetUniversalAdditionalCameraData();
        Camera overycam = GameObject.FindGameObjectWithTag("overlay").GetComponent<Camera>();
        cameraData.cameraStack.Add(overycam);
     
}
*/
/*
public void loadPrefabs()
 {
     prefabTrees = new List<GameObject>();
     int countTree = System.Enum.GetValues(typeof(treeTYPE)).Length;
     for(int i=0; i< countTree; i++)
     {
         int index = i;
         treeTYPE mytreeTYPE = (treeTYPE)index;
         string path = "Prefabs/tree/" + mytreeTYPE.ToString();
         prefabTrees.Add(Resources.Load<GameObject>(path));


     }

     prefabBranches = new List<GameObject>();
     int countBranches = System.Enum.GetValues(typeof(branchTYPE)).Length;
     for (int i = 0; i < countBranches; i++)
     {
         int index = i;
         branchTYPE mybranchTYPE = (branchTYPE)index;
         string path = "Prefabs/tmp/branch_tmp/" + mybranchTYPE.ToString();
         prefabBranches.Add(Resources.Load<GameObject>(path));
     }

     prefabSBranches = new List<GameObject>();
     int countSBranches = System.Enum.GetValues(typeof(sidebranchTYPE)).Length;
     for (int i = 0; i < countSBranches; i++)
     {
         int index = i;
         sidebranchTYPE mysbranchTYPE = (sidebranchTYPE)index;
         string path = "Prefabs/sbranch/" + mysbranchTYPE.ToString();
         prefabSBranches.Add(Resources.Load<GameObject>(path));
     }

     prefabAnimals = new List<GameObject>();
     int countAnimals = System.Enum.GetValues(typeof(animalTYPE)).Length;
     for (int i = 0; i < countAnimals; i++)
     {
         int index = i;
         animalTYPE myanimalTYPE = (animalTYPE)index;
         string path = "Prefabs/animal/" + myanimalTYPE.ToString();
         prefabAnimals.Add(Resources.Load<GameObject>(path));
     }

     prefabItems = new List<GameObject>();
     int countItems = System.Enum.GetValues(typeof(itemTYPE)).Length;
     for (int i = 0; i < countItems; i++)
     {
         int index = i;
         itemTYPE myitemTYPE = (itemTYPE)index;
         string path = "Prefabs/pickup/" + myitemTYPE.ToString();
         prefabItems.Add(Resources.Load<GameObject>(path));
     }

     prefabWords = new List<GameObject>();
     int countWORD = System.Enum.GetValues(typeof(wordTYPE)).Length;
     for (int i = 0; i < countWORD; i++)
     {
         int index = i;
         wordTYPE mywordTYPE = (wordTYPE)index;
         string path = "Prefabs/pickup/" + mywordTYPE.ToString();
         prefabWords.Add(Resources.Load<GameObject>(path));
     }

     prefabItemVFX = new List<GameObject>();
     int countItemVFX = System.Enum.GetValues(typeof(itemTYPE)).Length;
     for (int i = 0; i < countItemVFX; i++)
     {
         int index = i;
         itemTYPE myitemTYPE = (itemTYPE)index;
         string path = "Prefabs/vfx/" +"vfx_"+ myitemTYPE.ToString();
         prefabItemVFX.Add(Resources.Load<GameObject>(path));
     }

     prefabVFX = new List<GameObject>();
     int countVFX = System.Enum.GetValues(typeof(vfxTYPE)).Length;
     for (int i = 0; i < countVFX; i++)
     {
         int index = i;
         vfxTYPE myvfxTYPE = (vfxTYPE)index;
         string path = "Prefabs/vfx/" +  myvfxTYPE.ToString();
         prefabVFX.Add(Resources.Load<GameObject>(path));
     }

     prefabSpeeches = new List<GameObject>();
     int countSpeech = System.Enum.GetValues(typeof(speechTYPE)).Length;
     for (int i = 0; i < countSpeech; i++)
     {
         int index = i;
         speechTYPE myspeechTYPE = (speechTYPE)index;
         string path = "Prefabs/UI/" + myspeechTYPE;
         prefabSpeeches.Add(Resources.Load<GameObject>(path));
     }



     prefabUIm = new List<GameObject>();
     int countUIm = System.Enum.GetValues(typeof(uimTYPE)).Length;
     for (int i = 0; i < countUIm; i++)
     {
         int index = i;
         uimTYPE myuimTYPE = (uimTYPE)index;
         string path = "Prefabs/UI/" + myuimTYPE.ToString();
         prefabUIm.Add(Resources.Load<GameObject>(path));


     }
     prefabUIb = new List<GameObject>();
     int countUIb = System.Enum.GetValues(typeof(uibTYPE)).Length;
     for (int i = 0; i < countUIb; i++)
     {
         int index = i;
         uibTYPE myuibTYPE = (uibTYPE)index;
         string path = "Prefabs/UI/" + myuibTYPE.ToString();
         prefabUIb.Add(Resources.Load<GameObject>(path));


     }

     prefabPickup = new List<GameObject>();
     int countPickup = System.Enum.GetValues(typeof(pickupTYPE)).Length;
     for (int i = 0; i < countPickup; i++)
     {
         int index = i;
         pickupTYPE mypickupTYPE = (pickupTYPE)index;
         string path = "Prefabs/pickup/" + mypickupTYPE.ToString();
         prefabPickup.Add(Resources.Load<GameObject>(path));
     }


     prefabEnvir = new List<GameObject>();
     int countEnvir = System.Enum.GetValues(typeof(envirTYPE)).Length;
     for (int i = 0; i < countEnvir; i++)
     {
         int index = i;
         envirTYPE myenvirTYPE = (envirTYPE)index;
         string path = "Prefabs/envir/" + myenvirTYPE.ToString();
         prefabEnvir.Add(Resources.Load<GameObject>(path));
     }


     prefabAudioClips = new List<AudioClip>();
     int countAudioClips = System.Enum.GetValues(typeof(soundTYPE)).Length;
     for (int i = 0; i < countAudioClips; i++)
     {
         int index = i;
         soundTYPE myaudioclipTYPE = (soundTYPE)index;

         string path = "audio/" + myaudioclipTYPE.ToString();


         var clip = Resources.Load(path) as AudioClip;


         prefabAudioClips.Add(clip);
     }


     prefabCheckers = new List<GameObject>();
     int countChecker = System.Enum.GetValues(typeof(checkerTYPE)).Length;
     for (int i = 0; i < countChecker; i++)
     {
         int index = i;
         checkerTYPE mycheckerTYPE = (checkerTYPE)index;
         string path = "Prefabs/checker/" + mycheckerTYPE.ToString();
         prefabCheckers.Add(Resources.Load<GameObject>(path));
     }


     prefabStructures = new List<GameObject>();
     int countStructure = System.Enum.GetValues(typeof(structureTYPE)).Length;
     for (int i = 0; i < countStructure; i++)
     {
         int index = i;
         structureTYPE mystructureTYPE = (structureTYPE)index;
         string path = "Prefabs/structure/" + mystructureTYPE.ToString();
         prefabStructures.Add(Resources.Load<GameObject>(path));
     }

 }

 */
public void updatePlayerData()
    {
        SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal + m_Coin;

       
        if(m_Distance>SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].Distance)
        {
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].Distance = m_Distance;
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].seq = m_Seq;

            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].posX = DPposition.x;
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].posY = DPposition.y;
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].posZ = DPposition.z;

            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotX = DProtation.x;
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotY = DProtation.y;
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotZ = DProtation.z;
            SaveManager.playerData.deathPoints[indexPlaymode, indexTheme, indexLevel].rotW = DProtation.w;

        }
      
        SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 0] = m_OwendGECKO[0];
        SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 1] = m_OwendGECKO[1];
        SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 2] = m_OwendGECKO[2];
        SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 3] = m_OwendGECKO[3];
        SaveManager.playerData.ownedGECKO[indexTheme-1, indexLevel, 4] = m_OwendGECKO[4];

        if(m_Cleared)
        {
            Debug.Log(indexLevel + "level cleared");
            SaveManager.playerData.levelCleared[indexTheme-1, indexLevel] = true;
        }
        /*
        if (levelData.f_mapIndexGecko == 0)
        {

        }
        else
        {
            SaveManager.playerData.activatedGECKO[levelData.f_mapIndexGecko] = true;
        }
        */
       
        SaveManager.Instance.Save();


    }

    public float addHP(float amount)
    {
        if(m_HP + amount > TotalHP)
        {
            m_HP = TotalHP;
        }
        else
        {
            m_HP = m_HP + amount;
        }
      
      /*
        if (m_HP > m_TotalHP * 1 / 3 && GeckoBehaviour.Instance.Warning)
        {
            GeckoBehaviour.Instance.Warning = false;
        }
      */
        return m_HP;
    }


    public float addMana(float amount)
    {
        m_Mana = m_Mana + amount;
        return m_Mana;
    }

    public float addScore(int amount)
    {
        m_Score = m_Score + amount;
        return m_Score;
    }

    public void addDistance(float subtract, float add)
    {
        TotalDistance = TotalDistance - subtract + add;
    }
    public int addJump(int amount)
    {
        m_Jump = m_Jump + amount;
        return m_Jump;
    }
    public void addItem(int index)
    {
        m_Item = index;
    }
    public int addCoin(int amount)
    {
        /*
        if (GeckoBehaviour.Instance.myitemSTATE == itemSTATE.itemCoin2X)
        {
            m_Coin = m_Coin + 2 * amount;
        }
        else
        {
            m_Coin = m_Coin + amount;
        }   */
        return m_Coin;
       
    }


    public void addWord(int index)
    {
        m_OwendGECKO[index] = true;
    }

   


    public IEnumerator updateHP()
    {
        float ratio = 1;
        bool canfill = true;
     
        while (canfill)
        {
            m_HP = m_HP - ratio * Time.deltaTime;
   
            if (myentryTYPE != entryTYPE.fromBonus)
            {
                if (m_HP < 0)
                {
                    canfill = false;
                    /*
                    if (GeckoController.Instance.mygeckoSTATE != GeckoController.geckoSTATE.geckoDIE)
                    {
                        StartCoroutine(GeckoBehaviour.Instance.geckoDie());
                    }
                    */

                }
                else if ((m_HP < m_TotalHP * 0.5f) )
                {
                  
                   // GeckoBehaviour.Instance.Warning = true;
                   // StartCoroutine(GeckoBehaviour.Instance.geckoHeartBeat());
                }
            }
            //Debug.Log("hp is" + m_HP);
            UIManager.Instance.updateUIsliderHP(m_HP);
            yield return null;
        }
    }
    public IEnumerator updateMana()
    {
        float ratio = 2f;
        bool canfill = true;
        while (canfill)
        {
            if (myentryTYPE != entryTYPE.fromBonus)
            {
                m_Mana = m_Mana + ratio * Time.deltaTime;
                if (m_Mana > m_TotalMana)
                {
                    canfill = false;
                }
            }
            //Debug.Log("mana is" + m_Mana);
            UIManager.Instance.updateUIsliderMana(m_Mana);
            yield return null;
        }
    }
    public IEnumerator updateDistance()
    {
        while (isPlaying)
        {
            if(myentryTYPE != entryTYPE.fromBonus)
            {
                m_Time = m_Time + Time.deltaTime;
               // m_Distance = m_Time * GeckoController.Instance.SpeedWalk;
                UIManager.Instance.updateUIDistance(m_Distance);
                yield return null;
            }
           
        }
    }

}
