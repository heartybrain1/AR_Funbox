using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using FluffyUnderware.Curvy.Controllers;
//using GeneratedNS;


//using UnityEngine.Rendering.Universal;
public enum themeType { themeDinosaur, themeAnimal, themeMovie, themeInsect, themeSports, themeSimulatio, themeGame}

public enum playType { type_0_bra, type_1_pigeon, type_2_trex, type_3_stork, type_4_zombie, type_5_spider, type_6_bear, type_7_frog, type_8_elephant, type_9_crocodile, type_10_ski, type_11_raptor, type_12_cat }

public class GameManager : MonoBehaviour
{


  
    public static GameManager Instance;

    public static bool isPlaying = false;
  
   
    public static int indexPlaymode;
    public static int indexTheme;
    public static int indexLevel;



  

    private int m_Life;
    public int Life { get { return m_Life; } set { m_Life = value; } }




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
      
      
    }
    
  

}
