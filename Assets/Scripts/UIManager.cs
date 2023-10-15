using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
//using GeneratedNS;
using UnityEngine.Audio;
//using BansheeGz.BGDatabase;

public enum abilityTYPE { plusHP, plusRatioMana, plusItemCoin2X, plusItemMagnet, plusItemTimesleep, plusItemEcho, plusItemScaleup, plusItemHeart, plusItemEdible, plusItemJump, plusItemShield, plusItemOffpoison, plusItemOffwind }
public enum geckoSkinTYPE { gecko0, gecko1, gecko2, gecko3, gecko4}
public enum animalSkinTYPE { animal0, animal1, animal2, animal3, animal4, animal5, animal6, animal7, animal8, animal9 }
public enum treeSkinTYPE { tree0, tree1, tree2, tree3, tree4 }
public enum shopcoinTYPE {coin500, coin2000, coin5000, coin20000, coin100000 }
public enum uibTYPE { uiBtnMenu, uiBtnCharacter,uiBtnAnimal, uiBtnUpgrade, uiBtnGallery, uiBtnLevel, uiBtnCoin, uiBtnTree, uiBtnPlaymode,uiBtnCampaign, uiBtnSinglerun, uiBtnHome}
public enum uimTYPE { ui_mHome, ui_mPlaymode, ui_mShop, ui_mCharacter, ui_mUpgrade, ui_mTree,ui_mAnimal, ui_mCoin, ui_mT1Level, ui_mT2Level, ui_mT3Level, ui_mT4Level, ui_mCampaign, ui_mSinglerun, ui_mSetting, ui_mGallery }
public enum uipTYPE {ui_pBuy, ui_pTutorial, ui_pCredit, ui_pReset, ui_pEtc, ui_pNotEnoughMoney }
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    Slider sliderHP;
    Slider sliderMana;
    Slider sliderDistance;
    public Button btnPowerRun;

    Text textCoin;
    Text textDistance;
    Text textScore;
    public GameObject character;

    public Coroutine corHPslider;
    public AudioMixer audioMixer;

    int indexPage = 1;

   
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
       
    }


    public void setCurrentGeckoCharacter(int index)
    {
        character = GameObject.FindGameObjectWithTag("character");
        for (int i = 0; i < 5; i++)
        {
            character.transform.GetChild(i).gameObject.SetActive(false);
        }
        character.transform.GetChild(index).gameObject.SetActive(true);

    }
    /*
    public string getProductName(int indexShop, int indexProduct)
    {
        E_Menu entity0 = E_Menu.GetEntity(0);
        List<E_shopMenu> listShopmenuData = entity0.f_shopMenu;
        E_shopMenu shopMenuData = listShopmenuData[indexShop];

        List<E_product> listProductData = shopMenuData.f_product;
        E_product productData = listProductData[indexProduct];

        return productData.f_name;
     
    }
    public int getProductPrice(int indexShop, int indexProduct)
    {
        E_Menu entity0 = E_Menu.GetEntity(0);
        List<E_shopMenu> listShopmenuData = entity0.f_shopMenu;
        E_shopMenu shopMenuData = listShopmenuData[indexShop];

        List<E_product> listProductData = shopMenuData.f_product;
        E_product productData = listProductData[indexProduct];

        return productData.f_price;

    }
    public string getProductDescription(int indexShop, int indexProduct)
    {
        E_Menu entity0 = E_Menu.GetEntity(0);
        List<E_shopMenu> listShopmenuData = entity0.f_shopMenu;
        E_shopMenu shopMenuData = listShopmenuData[indexShop];

        List<E_product> listProductData = shopMenuData.f_product;
        E_product productData = listProductData[indexProduct];

        return productData.f_description;

    }
    */
    public Transform instantiateUImenu(int index)
    {
        GameObject UIm = Instantiate(GameManager.Instance.prefabUIm[index]);
        UIm.name = ((uimTYPE)index).ToString();
        Transform canv = GameObject.Find("Canvas").transform;
        UIm.transform.SetParent(canv);
        UIm.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIm.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIm.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UIm.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        return UIm.transform;
    }


    public void initializeUIonHome(uimTYPE index)
    {
       
        setCurrentGeckoCharacter(SaveManager.playerData.currentGecko);

        setMenuHome();
        setMenuPlaymode();
        setMenuCampaign();
        for (int i = 1; i < 5; i++)
        {
            setMenuLevel(i);
        }
       
        setMenuSinglerun();

        setMenuShop();
        setMenuUpgrade();
        setMenuCharacter();
        setMenuAnimal();
        setMenuTree();
        setMenuCoin();
        //setMenuTreeGallery();

        setMenuSetting();

        activeUIm(index);
    }

    public void activeUIm(uimTYPE ui)
    {
        character.SetActive(false);
        if (ui==uimTYPE.ui_mHome)
        {
            GameManager.indexPlaymode = 0;
            GameManager.indexTheme = 0;
            GameManager.indexLevel = 0;

            character.SetActive(true);
        } 
        else if(ui==uimTYPE.ui_mPlaymode)
        {
            GameManager.indexPlaymode = 0;
            GameManager.indexTheme = 0;
            GameManager.indexLevel = 0;
        }
        else if (ui == uimTYPE.ui_mCampaign)
        {
            GameManager.indexTheme = 0;
            GameManager.indexLevel = 0;
        }
        else if (ui == uimTYPE.ui_mT1Level)
        {
            GameManager.indexLevel = 0;
        }
        else if (ui == uimTYPE.ui_mT2Level)
        {
            GameManager.indexLevel = 0;
        }
        else if (ui == uimTYPE.ui_mT3Level)
        {
            GameManager.indexLevel = 0;
        }
        else if (ui == uimTYPE.ui_mT4Level)
        {
            GameManager.indexLevel = 0;
        }
        else
        {
          
        }
      
        Transform canv = GameObject.Find("Canvas").transform;
        int count = canv.childCount;
        for (int i = 0; i < count; i++)
        {
            canv.GetChild(i).gameObject.SetActive(false);
        }
        canv.Find(ui.ToString()).gameObject.SetActive(true);
    }



    #region set menu
    public void setMenuHome()
    {
        int index = (int)uimTYPE.ui_mHome;
        Transform mHome = instantiateUImenu(index);

        mHome.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mHome.Find("spacer").GetComponent<RectTransform>();

        GameObject btn = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn.transform.SetParent(spacer);
        btn.transform.localScale = new Vector3(1, 1, 1);
        btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn.GetComponentInChildren<Text>().text = "play";
        btn.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mPlaymode));

        GameObject btn2 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn2.transform.SetParent(spacer);
        btn2.transform.localScale = new Vector3(1, 1, 1);
        btn2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn2.GetComponentInChildren<Text>().text = "shop";
        btn2.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mShop));

        GameObject btn3 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn3.transform.SetParent(spacer);
        btn3.transform.localScale = new Vector3(1, 1, 1);
        btn3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn3.GetComponentInChildren<Text>().text = "gallery";
        //btn3.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mGallery));


        GameObject btn4 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn4.transform.SetParent(spacer);
        btn4.transform.localScale = new Vector3(1, 1, 1);
        btn4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn4.GetComponentInChildren<Text>().text = "setting";
        btn4.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mSetting));

    }
    public void setMenuPlaymode()
    {
        int index = (int)uimTYPE.ui_mPlaymode;
        Transform mPlaymode = instantiateUImenu(index);

        mPlaymode.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mPlaymode.Find("spacer").GetComponent<RectTransform>();

        GameObject btn1 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnPlaymode]);
        btn1.transform.SetParent(spacer);
        btn1.transform.localScale = new Vector3(1, 1, 1);
        btn1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite1 = Resources.Load<Sprite>("Sprites/campaign");
        btn1.transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
        btn1.transform.GetChild(1).GetComponent<Text>().text = "Campaign";
        btn1.transform.GetChild(2).GetComponent<Text>().text = "score is";
        btn1.transform.GetChild(3).GetComponent<Text>().text = "";
        btn1.transform.GetChild(4).GetComponent<Text>().text = "junior, explore the four theme of the tiny world, your health must not be zero";
        btn1.GetComponent<Button>().onClick.AddListener(() => OnSelectPlaymode(0));
      
        GameObject btn2 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnPlaymode]);
        btn2.transform.SetParent(spacer);
        btn2.transform.localScale = new Vector3(1, 1, 1);
        btn2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite2 = Resources.Load<Sprite>("Sprites/singlerun");
        btn2.transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
        btn2.transform.GetChild(1).GetComponent<Text>().text = "singlerun";
        btn2.transform.GetChild(2).GetComponent<Text>().text = "score is" + SaveManager.playerData.totalScore2.ToString();
        btn2.transform.GetChild(3).GetComponent<Text>().text = "max distance is";
        btn2.transform.GetChild(4).GetComponent<Text>().text = "senior, are you experienced? you have just one chance to run through entire tree level, gecko can punch the beetle";
        btn2.GetComponent<Button>().onClick.AddListener(() => OnSelectPlaymode(1));

        GameObject btn3 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnPlaymode]);
        btn3.transform.SetParent(spacer);
        btn3.transform.localScale = new Vector3(1, 1, 1);
        btn3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite3 = Resources.Load<Sprite>("Sprites/yourtree");
        btn3.transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
        btn3.transform.GetChild(1).GetComponent<Text>().text = "your tree";
        btn3.transform.GetChild(2).GetComponent<Text>().text = "the number of animal is";
        btn3.transform.GetChild(3).GetComponent<Text>().text = "tree is";
        btn3.transform.GetChild(4).GetComponent<Text>().text = "get the five GECKO character in each level and collect animals on your tree";
        //btn3.GetComponent<Button>().onClick.AddListener(() => OnSelectPlaymode(1));
        btn3.GetComponent<Button>().onClick.AddListener(() => OnSelectYourTree());
      

        mPlaymode.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mHome));
    }
    public void setMenuCampaign()
    {
        int index = (int)uimTYPE.ui_mCampaign;
        Transform mCampaign = instantiateUImenu(index);

        mCampaign.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mCampaign.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();

        GameObject btn = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnCampaign]);
        btn.transform.SetParent(spacer);
        btn.transform.localScale = new Vector3(1, 1, 1);
        btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite1 = Resources.Load<Sprite>("Sprites/theme1");
        btn.transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
        btn.transform.GetChild(1).GetComponent<Text>().text = "day";
        btn.transform.GetChild(2).GetComponent<Text>().text = "%";
        btn.GetComponent<Button>().onClick.AddListener(() => OnSelectCampaign(1));

        GameObject btn2 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnCampaign]);
        btn2.transform.SetParent(spacer);
        btn2.transform.localScale = new Vector3(1, 1, 1);
        btn2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite2 = Resources.Load<Sprite>("Sprites/theme2");
        btn2.transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
        btn2.transform.GetChild(1).GetComponent<Text>().text = "night";
        btn2.transform.GetChild(2).GetComponent<Text>().text = "%";
        btn2.GetComponent<Button>().onClick.AddListener(() => OnSelectCampaign(2));
        
        GameObject btn3 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnCampaign]);
        btn3.transform.SetParent(spacer);
        btn3.transform.localScale = new Vector3(1, 1, 1);
        btn3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite3 = Resources.Load<Sprite>("Sprites/theme3");
        btn3.transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
        btn3.transform.GetChild(1).GetComponent<Text>().text = "human";
        btn3.transform.GetChild(2).GetComponent<Text>().text = "%";
        btn3.GetComponent<Button>().onClick.AddListener(() => OnSelectCampaign(3));
       
        GameObject btn4 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnCampaign]);
        btn4.transform.SetParent(spacer);
        btn4.transform.localScale = new Vector3(1, 1, 1);
        btn4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Sprite sprite4 = Resources.Load<Sprite>("Sprites/theme4");
        btn4.transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
        btn4.transform.GetChild(1).GetComponent<Text>().text = "fire";
        btn4.transform.GetChild(2).GetComponent<Text>().text = "%";
        btn4.GetComponent<Button>().onClick.AddListener(() => OnSelectCampaign(4));

        mCampaign.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mPlaymode));
    }
    public void setMenuLevel(int theme)
    {
        int index;
        if (theme == 1)
        {
            index = (int)uimTYPE.ui_mT1Level;
        }
        else if (theme == 2)
        {
            index = (int)uimTYPE.ui_mT2Level;
        }
        else if (theme == 3)
        {
            index = (int)uimTYPE.ui_mT3Level;
        }
        else
        {
            index = (int)uimTYPE.ui_mT4Level;
        }

        Transform mLevel = instantiateUImenu(index);
        mLevel.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mLevel.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>();

        int count = 30;
        for (int i = 0; i < count; i++)
        {

            GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnLevel]);
            Transform btn = obj.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            int level = i;
            Text tBtn = btn.GetChild(0).GetComponent<Text>();
            tBtn.text = level.ToString();
            btn.GetComponent<Button>().onClick.AddListener(() => OnSelectLevel(level));

            if(theme==1)
            {
                if (i < 1)
                {
                    btn.GetChild(2).gameObject.SetActive(false);
                    btn.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btn.GetChild(2).gameObject.SetActive(true);
                    btn.GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                btn.GetChild(2).gameObject.SetActive(true);
                btn.GetComponent<Button>().interactable = false;

            }

            /*
            if(level==0)
            {
                btn.GetChild(2).gameObject.SetActive(false);
                btn.GetComponent<Button>().interactable = true;
            }
            else
            {
                if (SaveManager.playerData.levelCleared[theme - 1, level-1])
                {
                    btn.GetChild(2).gameObject.SetActive(false);
                    btn.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btn.GetChild(2).gameObject.SetActive(true);
                    btn.GetComponent<Button>().interactable = false;
                }

            }

            */

          


            int owendGECKO = 0;
            for (int j = 0; j < 5; j++)
            {
                if (SaveManager.playerData.ownedGECKO[theme-1,level, j] == true)
                {
                    owendGECKO++;
                }
            }
            Text tGECKO = btn.GetChild(1).GetComponent<Text>();
            tGECKO.text = owendGECKO.ToString() + "/5";


        }
        mLevel.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mCampaign));
    }
    public void setMenuSinglerun()
    {
        int index = (int)uimTYPE.ui_mSinglerun;
        Transform mSinglerun = instantiateUImenu(index);
        RectTransform spacer = mSinglerun.Find("spacer").GetComponent<RectTransform>();

        GameObject btn = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnSinglerun]);
        btn.transform.SetParent(spacer);
        btn.transform.localScale = new Vector3(1, 1, 1);
        btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn.GetComponent<Button>().onClick.AddListener(() => OnSelectSinglerun());

        mSinglerun.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mPlaymode));
    }
    public void setMenuShop()
    {
        int index = (int)uimTYPE.ui_mShop;
        Transform mShop = instantiateUImenu(index);
        mShop.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mShop.Find("spacer").GetComponent<RectTransform>();

        GameObject btn = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnMenu]);
        btn.transform.SetParent(spacer);
        btn.transform.localScale = new Vector3(1, 1, 1);
        btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn.GetComponentInChildren<Text>().text = "upgrade";
        btn.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mUpgrade));

        GameObject btn2 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnMenu]);
        btn2.transform.SetParent(spacer);
        btn2.transform.localScale = new Vector3(1, 1, 1);
        btn2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn2.GetComponentInChildren<Text>().text = "character";
        btn2.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mCharacter));

        GameObject btn3 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnMenu]);
        btn3.transform.SetParent(spacer);
        btn3.transform.localScale = new Vector3(1, 1, 1);
        btn3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn3.GetComponentInChildren<Text>().text = "animal";
        btn3.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mAnimal));

        GameObject btn4 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnMenu]);
        btn4.transform.SetParent(spacer);
        btn4.transform.localScale = new Vector3(1, 1, 1);
        btn4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn4.GetComponentInChildren<Text>().text = "tree";
        btn4.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mTree));

        GameObject btn5 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnMenu]);
        btn5.transform.SetParent(spacer);
        btn5.transform.localScale = new Vector3(1, 1, 1);
        btn5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn5.GetComponentInChildren<Text>().text = "coin";
        btn5.GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mCoin));

        mShop.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mHome));
    }
    public void setMenuUpgrade()
    {/*
        int indexMenu = (int)uimTYPE.ui_mUpgrade;
        Transform mUpgrade = instantiateUImenu(indexMenu);
        mUpgrade.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mUpgrade.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();

        int count = System.Enum.GetValues(typeof(itemTYPE)).Length;

        for (int i = 0; i < count; i++)
        {
            int indexItem = i;
            GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnUpgrade]);
            Transform btn = obj.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            string path = "Sprites/" + ((itemTYPE)indexItem).ToString();
            Sprite sprite = Resources.Load<Sprite>(path);
            btn.GetChild(3).GetComponent<Image>().sprite = sprite;

            int step = SaveManager.playerData.itemStep[indexItem];

            Text tName = btn.GetChild(0).GetComponent<Text>();
            tName.text = getProductName(0, indexItem);

            Text tPrice = btn.GetChild(2).GetComponent<Text>();
            tPrice.text = getProductPrice(0, indexItem).ToString();

            Text tDescription = btn.GetChild(4).GetComponent<Text>();
            tDescription.text = getProductDescription(0, indexItem);
          
            Text tBuy = btn.GetChild(6).GetChild(0).GetComponent<Text>();
            if (step == 5)
            {
                tBuy.text = "full";
                btn.GetChild(6).GetComponent<Button>().interactable = false;
            }
            else
            {
                tBuy.text = "buy";
            }

            Transform steps = btn.transform.GetChild(5);
            for (int j = 0; j < step; j++)
            {
                steps.GetChild(j).gameObject.SetActive(true);
            }
          

        btn.GetChild(6).GetComponent<Button>().onClick.AddListener(() => OnSelectItem(indexItem));
        }

        RectTransform spacer2 = mUpgrade.GetChild(5).GetComponent<RectTransform>();
        spacer2.GetChild(0).GetChild(2).GetComponent<Text>().text = SaveManager.playerData.hp.ToString();
        spacer2.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnSelectHP());

        spacer2.GetChild(1).GetChild(2).GetComponent<Text>().text = SaveManager.playerData.manaRatio.ToString();
        spacer2.GetChild(1).GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnSelectManaRatio());
      
        mUpgrade.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mShop));
        */
    }


    public void setMenuCharacter()
    {
        /*
        int indexMenu = (int)uimTYPE.ui_mCharacter;
        Transform mCharacter = instantiateUImenu(indexMenu);
        mCharacter.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mCharacter.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        
        int count = System.Enum.GetValues(typeof(geckoSkinTYPE)).Length;
        for (int i = 0; i < count; i++)
        {

            int indexSkin = i;
         
            GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnCharacter]);
            Transform btn = obj.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            string path = "Sprites/gecko" + indexSkin.ToString();
            Sprite sprite = Resources.Load<Sprite>(path);
            btn.GetChild(3).GetComponent<Image>().sprite = sprite;

            Text tName = btn.GetChild(0).GetComponent<Text>();
            tName.text = getProductName(1, indexSkin);

            Text tPrice = btn.GetChild(2).GetComponent<Text>();
            tPrice.text = getProductPrice(1, indexSkin).ToString();

            Text tDescription = btn.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>();
            tDescription.text = getProductDescription(1, indexSkin);
            Image imgGecko= btn.GetChild(4).GetChild(0).GetComponent<Image>();
            string path1 = "Sprites/imgGecko" + indexSkin.ToString();
            Sprite sprite1 = Resources.Load<Sprite>(path);
            imgGecko.sprite = sprite1;

            Button btnPopup = btn.GetChild(4).GetChild(1).GetComponent<Button>();
            btnPopup.GetComponent<Button>().onClick.AddListener(() => OnSelectPopup(btn.GetChild(4).gameObject));




            Text tBuy = btn.GetChild(6).GetChild(0).GetComponent<Text>();
          
            
            E_ability abilityData = E_ability.GetEntity(indexSkin);
            RectTransform pAbilities = btn.GetChild(5).GetComponent<RectTransform>();

            for(int j=0; j< pAbilities.childCount; j++)
            {
                int indexAbility = j;
                string str = ((abilityTYPE)indexAbility).ToString();
                pAbilities.GetChild(j).GetChild(0).GetComponent<Text>().text = abilityData.Get<float>(str).ToString();
                pAbilities.GetChild(j).GetChild(1).GetComponent<Text>().text = str;
                if (abilityData.Get<float>(str) == 0)
                {
                    pAbilities.GetChild(j).gameObject.SetActive(false);

                }
            }

           
            pAbilities.GetChild(0).GetChild(0).GetComponent<Text>().text = abilityData.f_plusHP.ToString();
            pAbilities.GetChild(0).GetChild(1).GetComponent<Text>().text = "HP";
            if(abilityData.f_plusHP== 0)
            {
                pAbilities.GetChild(0).gameObject.SetActive(false);

            }

            pAbilities.GetChild(1).GetChild(0).GetComponent<Text>().text = abilityData.f_plusRatioMana.ToString();
            pAbilities.GetChild(1).GetChild(1).GetComponent<Text>().text = "ratioMana";
            if (abilityData.f_plusRatioMana == 0)
            {
                pAbilities.GetChild(0).gameObject.SetActive(false);

            }

            pAbilities.GetChild(2).GetChild(0).GetComponent<Text>().text = abilityData.f_plusItemTimesleep.ToString();
            pAbilities.GetChild(2).GetChild(1).GetComponent<Text>().text = "itemTimesleep";
            if (abilityData.f_plusItemTimesleep == 0)
            {
                pAbilities.GetChild(0).gameObject.SetActive(false);

            }

            pAbilities.GetChild(3).GetChild(0).GetComponent<Text>().text = abilityData.f_plusItemTimesleep.ToString();
            pAbilities.GetChild(3).GetChild(1).GetComponent<Text>().text = "itemTimesleep";
            if (abilityData.f_plusItemTimesleep == 0)
            {
                pAbilities.GetChild(0).gameObject.SetActive(false);

            }

            pAbilities.GetChild(4).GetChild(0).GetComponent<Text>().text = abilityData.f_plusItemTimesleep.ToString();
            pAbilities.GetChild(4).GetChild(1).GetComponent<Text>().text = "itemTimesleep";
            if (abilityData.f_plusItemTimesleep == 0)
            {
                pAbilities.GetChild(0).gameObject.SetActive(false);

            }

            pAbilities.GetChild(5).GetChild(0).GetComponent<Text>().text = abilityData.f_plusItemTimesleep.ToString();
            pAbilities.GetChild(5).GetChild(1).GetComponent<Text>().text = "itemTimesleep";
            if (abilityData.f_plusItemTimesleep == 0)
            {
                pAbilities.GetChild(0).gameObject.SetActive(false);

            }

            pAbilities.GetChild(6).GetChild(0).GetComponent<Text>().text = abilityData.f_plusItemTimesleep.ToString();
            pAbilities.GetChild(6).GetChild(1).GetComponent<Text>().text = "itemTimesleep";


        


            if (SaveManager.playerData.ownedGecko[indexSkin])
            {
                if (SaveManager.playerData.currentGecko == indexSkin)
                {
                    tBuy.text = "selected";
                    btn.GetChild(6).GetComponent<Button>().interactable = false;
                }
                else
                {
                    tBuy.text = "select";
                }
            }
            else
            {
                tBuy.text = "buy";
            }
            btn.GetChild(6).GetComponent<Button>().onClick.AddListener(() => OnSelectSkinGecko(indexSkin));




            if(SaveManager.playerData.activatedGECKO[indexSkin])
            {
                btn.GetChild(7).gameObject.SetActive(false);
            }
            else
            {
                btn.GetChild(7).gameObject.SetActive(true);
            }

         



        }
        mCharacter.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mShop));
        */
    }
    public void setMenuAnimal()
    {
        /*
        int indexMenu = (int)uimTYPE.ui_mAnimal;
        Transform mAnimal = instantiateUImenu(indexMenu);

        mAnimal.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mAnimal.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        int count = System.Enum.GetValues(typeof(animalSkinTYPE)).Length;

        for (int i = 0; i < count; i++)
        {

            int indexSkin = i;

            GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnAnimal]);
            Transform btn = obj.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            string path = "Sprites/animal" + indexSkin.ToString();
            Sprite sprite = Resources.Load<Sprite>(path);
            btn.GetChild(3).GetComponent<Image>().sprite = sprite;

            Text tName = btn.GetChild(0).GetComponent<Text>();
            tName.text = getProductName(2, indexSkin);

            Text tPrice = btn.GetChild(2).GetComponent<Text>();
            tPrice.text = getProductPrice(2, indexSkin).ToString();

            Text tDescription = btn.GetChild(4).GetComponent<Text>();
            tDescription.text = getProductDescription(2, indexSkin);

            Text tBuy = btn.GetChild(6).GetChild(0).GetComponent<Text>();

            if (SaveManager.playerData.ownedAnimal[indexSkin])
            {

                tBuy.text = "owned";

            }
            else
            {
                tBuy.text = "buy";
            }

            btn.GetChild(6).GetComponent<Button>().onClick.AddListener(() => OnSelectSkinAnimal(indexSkin));
        }
        mAnimal.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mShop));
        */
    }
    public void setMenuTree()
    {
        int indexMenu = (int)uimTYPE.ui_mTree;
        Transform mTree = instantiateUImenu(indexMenu);

        mTree.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mTree.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        int count = System.Enum.GetValues(typeof(treeSkinTYPE)).Length;

        for (int i = 0; i < count; i++)
        {

            int indexSkin = i;

            GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnTree]);
            Transform btn = obj.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            string path = "Sprites/tree" + indexSkin.ToString();
            Sprite sprite = Resources.Load<Sprite>(path);
            btn.GetChild(3).GetComponent<Image>().sprite = sprite;

            Text tName = btn.GetChild(0).GetComponent<Text>();
            //tName.text = getProductName(3, indexSkin);

            Text tPrice = btn.GetChild(2).GetComponent<Text>();
           // tPrice.text = getProductPrice(3, indexSkin).ToString();

            Text tDescription = btn.GetChild(4).GetComponent<Text>();
          //  tDescription.text = getProductDescription(3, indexSkin);

            Text tBuy = btn.GetChild(6).GetChild(0).GetComponent<Text>();

            if (SaveManager.playerData.ownedTree[indexSkin])
            {
                if (SaveManager.playerData.currentTree == indexSkin)
                {
                    tBuy.text = "selected";
                }
                else
                {
                    tBuy.text = "select";
                }
            }
            else
            {
                tBuy.text = "buy";
            }
            btn.GetChild(6).GetComponent<Button>().onClick.AddListener(() => OnSelectSkinTree(indexSkin));

        }
        mTree.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mShop));
    }
    public void setMenuCoin()
    {
        int indexMenu = (int)uimTYPE.ui_mCoin;
        Transform mCoin = instantiateUImenu(indexMenu);

        mCoin.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mCoin.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        int count = System.Enum.GetValues(typeof(shopcoinTYPE)).Length;

        for (int i = 0; i < count; i++)
        {

            int indexCoin = i;

            GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnCoin]);
            Transform btn = obj.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            string path = "Sprites/shop_gold" + indexCoin.ToString();
            Sprite sprite = Resources.Load<Sprite>(path);
            btn.GetChild(3).GetComponent<Image>().sprite = sprite;

            Text tName = btn.GetChild(0).GetComponent<Text>();
           // tName.text = getProductName(4, indexCoin);

            Text tPrice = btn.GetChild(2).GetComponent<Text>();
          //  tPrice.text = getProductPrice(4, indexCoin).ToString();

            Text tDescription = btn.GetChild(4).GetComponent<Text>();
          //  tDescription.text = getProductDescription(4, indexCoin);

            Text tBuy = btn.GetChild(5).GetChild(0).GetComponent<Text>();
            tBuy.text = "buy";

            btn.GetChild(5).GetComponent<Button>().onClick.AddListener(() => OnSelectShopCoin(indexCoin));

        }
        mCoin.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mShop));
    }
    public void setMenuSetting()
    {
        int indexMenu = (int)uimTYPE.ui_mSetting;
        Transform mSetting = instantiateUImenu(indexMenu);

        mSetting.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mSetting.Find("spacer").GetComponent<RectTransform>();

        GameObject btn = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn.transform.SetParent(spacer);
        btn.transform.localScale = new Vector3(1, 1, 1);
        btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn.GetComponentInChildren<Text>().text = "tutorial";
        btn.GetComponent<Button>().onClick.AddListener(() => OnSelectTutorial());

        GameObject btn2 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn2.transform.SetParent(spacer);
        btn2.transform.localScale = new Vector3(1, 1, 1);
        btn2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn2.GetComponentInChildren<Text>().text = "credit";
        btn2.GetComponent<Button>().onClick.AddListener(() => OnSelectCredit());

        GameObject btn3 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn3.transform.SetParent(spacer);
        btn3.transform.localScale = new Vector3(1, 1, 1);
        btn3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn3.GetComponentInChildren<Text>().text = "reset games";
        btn3.GetComponent<Button>().onClick.AddListener(() => OnSelectReset());


        GameObject btn4 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnHome]);
        btn4.transform.SetParent(spacer);
        btn4.transform.localScale = new Vector3(1, 1, 1);
        btn4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btn4.GetComponentInChildren<Text>().text = "etc";
        btn4.GetComponent<Button>().onClick.AddListener(() => OnSelectEtc());

        mSetting.Find("grp_slider").GetChild(1).GetComponent<Slider>().onValueChanged.AddListener(setVolume);

        bool value = true;
        mSetting.Find("grp_toggle").GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener(delegate { toggleBGM(value); });
        mSetting.Find("grp_toggle").GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener(delegate { toggleSFX(value); });

      

        mSetting.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => activeUIm(uimTYPE.ui_mHome));
    }
    #endregion



    #region on select btn
    private void OnSelectPlaymode(int playmode)
    {
        GameManager.indexPlaymode = playmode;
        if (playmode == 0)
        {
            activeUIm(uimTYPE.ui_mCampaign);
        }
        else if (playmode == 1)
        {
            activeUIm(uimTYPE.ui_mSinglerun);
        }

    }
    private void OnSelectCampaign(int theme)
    {
        GameManager.indexTheme = theme;
        if (theme == 1)
        {
            activeUIm(uimTYPE.ui_mT1Level);
        }
        else if (theme == 2)
        {
            activeUIm(uimTYPE.ui_mT2Level);
        }
        else if (theme == 3)
        {
            activeUIm(uimTYPE.ui_mT3Level);
        }
        else
        {
            activeUIm(uimTYPE.ui_mT4Level);
        }
    }
    private void OnSelectLevel(int level)
    {
        GameManager.indexLevel = level;
        GameManager.Instance.myentryTYPE = entryTYPE.fromHOME;
        SceneManager.LoadScene("2_Play");
    }


    private void OnSelectSinglerun()
    {
        Debug.Log("start singlerun");
    }
    private void OnSelectYourTree()
    {
        GameManager.Instance.myentryTYPE = entryTYPE.fromHOME;
        SceneManager.LoadScene("4_Tree");
    }
    private void OnSelectItem(int index)
    {
        /*
        List<E_upgrade> upgrades = E_Player.GetEntity(0).f_upgrade;
        List<E_shopMenu> shopMenu = E_Menu.GetEntity(0).f_shopMenu;
        List<E_product> products = shopMenu[0].f_product;

        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pBuy");
        GameObject UIbuy = Instantiate(obj);
        UIbuy.name = "ui_pBuy";

        UIbuy.transform.SetParent(GameObject.Find("Canvas").transform);
        UIbuy.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIbuy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UIbuy.transform.localScale = new Vector3(1, 1, 1);
        UIbuy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


        string name = products[index].Name;
        UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = name.ToString();

        int price = products[index].f_price;
        UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = price.ToString();

        UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this item?";

        int step = upgrades[index].f_step;
        UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBuyItem(index, price));

        UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pBuy));
        */

    }
    private void OnSelectHP()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pBuy");
        GameObject UIbuy = Instantiate(obj);
        UIbuy.name = "ui_pBuy";

        UIbuy.transform.SetParent(GameObject.Find("Canvas").transform);
        UIbuy.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIbuy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UIbuy.transform.localScale = new Vector3(1, 1, 1);
        UIbuy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


    
        UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "HP".ToString();
        UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "200".ToString();
        UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this item?";

      
        UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBuyHP());
        UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pBuy));

     
    }

    private void OnSelectManaRatio()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pBuy");
        GameObject UIbuy = Instantiate(obj);
        UIbuy.name = "ui_pBuy";

        UIbuy.transform.SetParent(GameObject.Find("Canvas").transform);
        UIbuy.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIbuy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UIbuy.transform.localScale = new Vector3(1, 1, 1);
        UIbuy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);



        UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ManaRatio".ToString();
        UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "100".ToString();
        UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this item?";


        UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBuyManaRatio());
        UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pBuy));

      
    }
    private void OnBuyHP()
    {
        if (SaveManager.playerData.coinTotal >= 200)
        {
            SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal - 200;
            SaveManager.playerData.hp = SaveManager.playerData.hp + 20;
            SaveManager.Instance.Save();


            removeUI((int)uipTYPE.ui_pBuy);
            updateShopHP();
        }
        else
        {
            removeUI((int)uipTYPE.ui_pBuy);
            OnNotEnoughMoney();
        }
    }
 
        private void OnBuyManaRatio()
    {
        if (SaveManager.playerData.coinTotal >= 100)
        {
            SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal - 100;
            SaveManager.playerData.manaRatio = SaveManager.playerData.manaRatio + 0.05f;
            SaveManager.Instance.Save();


            removeUI((int)uipTYPE.ui_pBuy);
            updateShopManaRatio();
        }
        else
        {
            removeUI((int)uipTYPE.ui_pBuy);
            OnNotEnoughMoney();
        }
    }

    private void OnSelectSkinGecko(int index)
    {
      
        if (SaveManager.playerData.ownedGecko[index] && SaveManager.playerData.currentGecko == index)
        {
           
        }
        else if (SaveManager.playerData.ownedGecko[index] && SaveManager.playerData.currentGecko != index)
        {

            int previous = SaveManager.playerData.currentGecko;
            SaveManager.playerData.currentGecko = index;
            SaveManager.Instance.Save();

            Transform canv = GameObject.Find("Canvas").transform;
            Transform mCharacter = canv.Find("ui_mCharacter");

            RectTransform spacer = mCharacter.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            spacer.GetChild(index).GetChild(6).GetChild(0).GetComponent<Text>().text = "selected";
            spacer.GetChild(previous).GetChild(6).GetChild(0).GetComponent<Text>().text = "select";
            spacer.GetChild(previous).GetChild(6).GetComponent<Button>().interactable = true;

            setCurrentGeckoCharacter(SaveManager.playerData.currentGecko);

        }
        else
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pBuy");
            GameObject UIbuy = Instantiate(obj);
            UIbuy.name = "ui_pBuy";
            UIbuy.transform.SetParent(GameObject.Find("Canvas").transform);
            UIbuy.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            UIbuy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            UIbuy.transform.localScale = new Vector3(1, 1, 1);
            UIbuy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

           // string name = getProductName(1, index).ToString();
            UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = name.ToString();

           // int price = getProductPrice(1, index);
           // UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = price.ToString();

            UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this skin?";
           // UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBuyGecko(index, price));

        
            UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pBuy));
        }
    }
    private void OnSelectPopup(GameObject obj)
    {


        if(obj.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            string path = "Sprites/popup_inactive";
            Sprite sprite1 = Resources.Load<Sprite>(path);
            obj.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = sprite1;
        }
        else
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            string path = "Sprites/popup_active";
            Sprite sprite1 = Resources.Load<Sprite>(path);
            obj.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = sprite1;

        }
       

    }

        private void OnSelectSkinAnimal(int index)
    {

        if (SaveManager.playerData.ownedAnimal[index])
        {

        }
 
        else
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pBuy");
            GameObject UIbuy = Instantiate(obj);
            UIbuy.name = "ui_pBuy";
            UIbuy.transform.SetParent(GameObject.Find("Canvas").transform);
            UIbuy.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            UIbuy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            UIbuy.transform.localScale = new Vector3(1, 1, 1);
            UIbuy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

           // string name = getProductName(2, index).ToString();
            UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = name.ToString();

            //int price = getProductPrice(2, index);
          //  UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = price.ToString();

            UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this animal?";
          //  UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBuyAnimal(index, price));


            UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pBuy));
        }
    }
    private void OnSelectSkinTree(int index)
    {

        if (SaveManager.playerData.ownedTree[index] && SaveManager.playerData.currentTree == index)
        {

        }
        else if (SaveManager.playerData.ownedTree[index] && SaveManager.playerData.currentTree != index)
        {

            int previous = SaveManager.playerData.currentTree;
            SaveManager.playerData.currentTree = index;
            SaveManager.Instance.Save();

            Transform canv = GameObject.Find("Canvas").transform;
            Transform mTree = canv.Find("ui_mTree");

            RectTransform spacer = mTree.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            spacer.GetChild(index).GetChild(6).GetChild(0).GetComponent<Text>().text = "selected";
            spacer.GetChild(previous).GetChild(6).GetChild(0).GetComponent<Text>().text = "select";
        }
        else
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pBuy");
            GameObject UIbuy = Instantiate(obj);
            UIbuy.name = "ui_pBuy";
            UIbuy.transform.SetParent(GameObject.Find("Canvas").transform);
            UIbuy.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            UIbuy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            UIbuy.transform.localScale = new Vector3(1, 1, 1);
            UIbuy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

           // string name = getProductName(3, index).ToString();
            UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = name.ToString();

           // int price = getProductPrice(3, index);
           // UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = price.ToString();

            UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this tree?";
          //  UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBuyTree(index, price));


            UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pBuy));
        }
    }
    private void OnSelectShopCoin(int index)
    {

    }
    private void OnSelectTutorial()
    {
        indexPage = 1;
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pTutorial");
        GameObject UITutorial = Instantiate(obj);
        UITutorial.name = "ui_pTutorial";
        UITutorial.transform.SetParent(GameObject.Find("Canvas").transform);
        UITutorial.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UITutorial.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UITutorial.transform.localScale = new Vector3(1, 1, 1);
        UITutorial.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

      
        string path = "Sprites/tutorial" + indexPage.ToString();
        Sprite sprite = Resources.Load<Sprite>(path);
        UITutorial.transform.GetChild(0).GetComponent<Image>().sprite = sprite;

        UITutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => previousPage());
        UITutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = false;
        UITutorial.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => nextPage());
      
        UITutorial.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = indexPage.ToString();

        UITutorial.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pTutorial));
    }
    public void OnSelectCredit()
    {
        Debug.Log("!!!");
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pCredit");
        GameObject UICredit = Instantiate(obj);
        UICredit.name = "ui_pCredit";
        UICredit.transform.SetParent(GameObject.Find("Canvas").transform);
        UICredit.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UICredit.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UICredit.transform.localScale = new Vector3(1, 1, 1);
        UICredit.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        UICredit.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pCredit));
    }


    public void OnSelectReset()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pReset");
        GameObject UIReset = Instantiate(obj);
        UIReset.name = "ui_pReset";
        UIReset.transform.SetParent(GameObject.Find("Canvas").transform);
        UIReset.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIReset.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UIReset.transform.localScale = new Vector3(1, 1, 1);
        UIReset.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        UIReset.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => resetGames());
        UIReset.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pReset));
    }
    public void OnSelectEtc()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pEtc");
        GameObject UIEtc = Instantiate(obj);
        UIEtc.name = "ui_pEtc";
        UIEtc.transform.SetParent(GameObject.Find("Canvas").transform);
        UIEtc.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIEtc.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UIEtc.transform.localScale = new Vector3(1, 1, 1);
        UIEtc.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


        UIEtc.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pEtc));
    }


    public void OnBuyItem(int index, int cost)
    {

        if (SaveManager.playerData.coinTotal >= cost)
        {
            SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal - cost;
            SaveManager.playerData.itemStep[index]++;
            SaveManager.Instance.Save();
          
            removeUI((int)uipTYPE.ui_pBuy);
            updateShopUpgrade(index);
        }
        else
        {
            removeUI((int)uipTYPE.ui_pBuy);
            OnNotEnoughMoney();
        }
    }
    public void OnBuyGecko(int index, int cost)
    {
        if (SaveManager.playerData.coinTotal >= cost)
        {
            SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal - cost;
            SaveManager.playerData.ownedGecko[index] = true;
            //SaveManager.playerData.currentGecko = index;
            SaveManager.Instance.Save();

            removeUI((int)uipTYPE.ui_pBuy);
            updateShopGecko(index);
            setCurrentGeckoCharacter(index);
        }
        else
        {
            removeUI((int)uipTYPE.ui_pBuy);
            OnNotEnoughMoney();
        }
    }
    public void OnBuyAnimal(int index, int cost)
    {
        if (SaveManager.playerData.coinTotal >= cost)
        {
            SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal - cost;
            SaveManager.playerData.ownedAnimal[index] = true;
           
            SaveManager.Instance.Save();
            removeUI((int)uipTYPE.ui_pBuy);

            updateShopAnimal(index);
        }
        else
        {
            removeUI((int)uipTYPE.ui_pBuy);
            OnNotEnoughMoney();
        }
    }
    public void OnBuyTree(int index, int cost)
    {
        if (SaveManager.playerData.coinTotal >= cost)
        {
            SaveManager.playerData.coinTotal = SaveManager.playerData.coinTotal - cost;
            SaveManager.playerData.ownedTree[index] = true;
            SaveManager.playerData.currentTree = index;
            SaveManager.Instance.Save();
            removeUI((int)uipTYPE.ui_pBuy);

            Transform canv = GameObject.Find("Canvas").transform;
            Transform mCharacter = canv.Find("ui_mTree");
            mCharacter.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
            RectTransform spacer = GameObject.Find("Canvas").transform.Find("spacer").GetComponent<RectTransform>();
            spacer.GetChild(index).GetChild(0).GetChild(0).GetComponent<Text>().text = "selected";
        }
        else
        {
            removeUI((int)uipTYPE.ui_pBuy);
            OnNotEnoughMoney();
        }
    }

    void OnNotEnoughMoney()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pNotEnoughMoney");
        GameObject pUI = Instantiate(obj);
        pUI.name = "ui_pNotEnoughMoney";
        pUI.transform.SetParent(GameObject.Find("Canvas").transform);
        pUI.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        pUI.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        pUI.transform.localScale = new Vector3(1, 1, 1);
        pUI.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

      
        pUI.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => removeUI((int)uipTYPE.ui_pNotEnoughMoney));

    }
    #endregion


    private void updateShopUpgrade(int index)
    {
        Transform canv = GameObject.Find("Canvas").transform;
        Transform mUpgrade = canv.Find("ui_mUpgrade");
        mUpgrade.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        int step = SaveManager.playerData.itemStep[index];
        RectTransform spacer = mUpgrade.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        for (int i = 0; i < step; i++)
        {
            spacer.GetChild(index).GetChild(5).GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }

    private void updateShopHP()
    {
        Transform canv = GameObject.Find("Canvas").transform;
        Transform mUpgrade = canv.Find("ui_mUpgrade");
        mUpgrade.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        float hp = SaveManager.playerData.hp;
        mUpgrade.GetChild(5).GetChild(0).GetChild(2).GetComponent<Text>().text = hp.ToString();
    }

    private void updateShopManaRatio()
    {
        Transform canv = GameObject.Find("Canvas").transform;
        Transform mUpgrade = canv.Find("ui_mUpgrade");
        mUpgrade.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        float manaRatio = SaveManager.playerData.manaRatio;
        mUpgrade.GetChild(5).GetChild(1).GetChild(2).GetComponent<Text>().text = manaRatio.ToString();


    }
    private void updateShopGecko(int index)
    {
        Transform canv = GameObject.Find("Canvas").transform;
        Transform mCharacter = canv.Find("ui_mCharacter");
        mCharacter.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = GameObject.Find("Canvas").transform.Find("spacer").GetComponent<RectTransform>();

        spacer.GetChild(index).GetChild(6).GetChild(0).GetComponent<Text>().text = "select";
      
    }
    private void updateShopAnimal(int index)
    {
        Transform canv = GameObject.Find("Canvas").transform;
        Transform mCharacter = canv.Find("ui_mAnimal");
        mCharacter.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = GameObject.Find("Canvas").transform.Find("spacer").GetComponent<RectTransform>();

        spacer.GetChild(index).GetChild(0).GetChild(0).GetComponent<Text>().text = "owned";
        spacer.GetChild(index).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
    }
    private void updateShopTree()
    {

    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void toggleBGM(bool value)
    {

    }
    public void toggleSFX(bool value)
    {

    }
    void resetGames()
    {



    }
    public void setCameraTree(int index)
    {
        //camera.transform.position = index camera position;
    }


    public void previousPage()
    {
        indexPage = indexPage - 1;
        GameObject UITutorial = GameObject.Find("Canvas").transform.Find("ui_pTutorial").gameObject;


        string path = "Sprites/tutorial" + indexPage.ToString();
        Sprite sprite = Resources.Load<Sprite>(path);
        UITutorial.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        UITutorial.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = indexPage.ToString();

        if (indexPage <2)
        {
            UITutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = false;
            UITutorial.transform.GetChild(1).GetChild(1).GetComponent<Button>().interactable = true;
        }
        else
        {
            UITutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = true;
        }

    }
    public void nextPage()
    {
        indexPage = indexPage + 1;
        GameObject UITutorial = GameObject.Find("Canvas").transform.Find("ui_pTutorial").gameObject;


        string path = "Sprites/tutorial" + indexPage.ToString();
        Sprite sprite = Resources.Load<Sprite>(path);
        UITutorial.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        UITutorial.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = indexPage.ToString();

        if(indexPage>9)
        {
            UITutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = true;
            UITutorial.transform.GetChild(1).GetChild(1).GetComponent<Button>().interactable = false;
        }
        else
        {
            UITutorial.transform.GetChild(1).GetChild(1).GetComponent<Button>().interactable = true;
        }
      
    }
   
    public void removeUI(int index)
    {
        string ui = ((uipTYPE)index).ToString();
        Destroy(GameObject.Find("Canvas").transform.Find(ui).gameObject);
    }
    
    public void initializeUIOnTree()
    {
        setMenuTreeGallery();
    }
    
  public void setCameraTree()
    {

    }

    public void setMenuTreeGallery()
    {
        int indexMenu = (int)uimTYPE.ui_mGallery;
        Transform mGallery = instantiateUImenu(indexMenu);
        mGallery.Find("total_coin").GetComponent<Text>().text = SaveManager.playerData.coinTotal.ToString();
        RectTransform spacer = mGallery.Find("spacer").GetComponent<RectTransform>();

        //add btn tree
        GameObject obj = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnGallery]);
        Transform btnTree = obj.transform;
        btnTree.SetParent(spacer);
        btnTree.localScale = new Vector3(1, 1, 1);
        btnTree.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        btnTree.GetChild(0).GetComponent<Text>().text = "tree".ToString();
        btnTree.GetComponent<Button>().onClick.AddListener(() => setCameraTree());


        int count = System.Enum.GetValues(typeof(animalSkinTYPE)).Length;

        for (int i = 1; i < count; i++)
        {
            int indexAnial = i;
            GameObject obj2 = Instantiate(GameManager.Instance.prefabUIb[(int)uibTYPE.uiBtnGallery]);
            Transform btn = obj2.transform;
            btn.SetParent(spacer);
            btn.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


            if(SaveManager.playerData.ownedAnimal[indexAnial])
            {
                string path = "Sprites/animal" + indexAnial.ToString();
                Sprite sprite = Resources.Load<Sprite>(path);
                btn.GetChild(3).GetComponent<Image>().sprite = sprite;

                Text tBtn = btn.GetChild(0).GetComponent<Text>();
              //  tBtn.text = getProductName(2, indexAnial);

                btn.GetComponent<Button>().onClick.AddListener(() => OnSelectCard(indexAnial));
            }
            else
            {
                string path = "Sprites/question";
                Sprite sprite = Resources.Load<Sprite>(path);
                btn.GetChild(3).GetComponent<Image>().sprite = sprite;

                Text tBtn = btn.GetChild(0).GetComponent<Text>();
                tBtn.text = "?";
                btn.GetComponent<Button>().interactable = false;
            }
        }
        mGallery.Find("ui_btnBack").GetComponent<Button>().onClick.AddListener(() => OnSelectHome());
    }

   void OnSelectHome()
    {
        GameManager.Instance.myentryTYPE = entryTYPE.fromTree;
        SceneManager.LoadScene("1_Home");
    }

    void OnSelectCard(int index)
    {

        GameObject uiGallery = GameObject.Find("Canvas").transform.Find("ui_Gallery").gameObject;


        //overlay frame image on btn, for i count false; index true
        int count = uiGallery.transform.childCount;
        for(int i=0; i<count; i++)
        {
            uiGallery.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

        }

        uiGallery.transform.GetChild(index).GetChild(1).gameObject.SetActive(true);


        //reset tree camera position and rotation
        Transform axisCamera = Camera.main.transform.parent;
        axisCamera.localPosition = new Vector3(0, 0, 10);
        axisCamera.localRotation = Quaternion.identity;

        //move camera  to index animal position
        //Camera.main.GetComponent<Navigation>().setCameraTo(index);
       
        //show  description of this animal
    }

    /*

    private void OnSelectAnimal(int index)
    {

        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "UICard");
        GameObject UICard = Instantiate(obj);
        UICard.name = "UICard";
        UICard.transform.SetParent(GameObject.Find("Canvas").transform);
        UICard.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UICard.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        UICard.transform.localScale = new Vector3(1, 1, 1);

        UICard.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
      
            UIbuy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = nameSkin[index].ToString();
            UIbuy.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = 2000.ToString();
            UIbuy.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "buy this skin?";
            UIbuy.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnSkinBuy(index, 2000));
            UIbuy.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => removeUIBuy());
      
        UICard.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => removeUICard());
        UICard.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => flipCard());
    }

    private void flipCard()
    {
        Debug.Log("flip");
        Transform UIcard = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform;
        UIcard.GetChild(0).gameObject.SetActive(UIcard.GetChild(1).gameObject.activeInHierarchy);
        UIcard.GetChild(1).gameObject.SetActive(!UIcard.GetChild(0).gameObject.activeInHierarchy);
    }
    */


    #region ingameUI

    public void initializeUIonCampaign()
    {
        int level = SceneManager.GetActiveScene().buildIndex;

        GameObject UIPlayPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIPlayCampaign");
        GameObject UIPlay = Instantiate(UIPlayPrefab);
        Transform canv = GameObject.Find("Canvas").transform;
        UIPlay.transform.SetParent(canv);
        UIPlay.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIPlay.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIPlay.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

     
        sliderHP= UIPlay.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>();
        sliderHP.maxValue = GameManager.Instance.HP;
        sliderHP.value = GameManager.Instance.HP;

        sliderMana = UIPlay.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Slider>();
        sliderMana.maxValue = GameManager.Instance.TotalMana;
        sliderMana.value = 0;

        btnPowerRun = UIPlay.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
        btnPowerRun.onClick.AddListener(() => OnBtnPowerUP());

        textScore = UIPlay.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        textScore.text = GameManager.Instance.Score.ToString();

        textDistance = UIPlay.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>();
        textDistance.text = GameManager.Instance.Distance.ToString();

        sliderDistance = UIPlay.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Slider>();
        sliderDistance.maxValue = GameManager.Instance.TotalDistance;
        sliderDistance.value = 0;

        textCoin = UIPlay.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Text>();
        textCoin.text = GameManager.Instance.Coin.ToString();

        for (int i = 0; i < 5; i++)
        {
            UIPlay.transform.GetChild(5).GetChild(0).GetChild(i).gameObject.SetActive(GameManager.Instance.OwendGECKO[i]);
        }

        UIPlay.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(() => OnBtnPaused());

        if(GameManager.Instance.myentryTYPE==entryTYPE.fromHOME|| GameManager.Instance.myentryTYPE == entryTYPE.fromRESTART)
        {
            UIPlay.transform.GetChild(8).gameObject.SetActive(false);
            UIPlay.transform.GetChild(9).gameObject.SetActive(true);
            UIPlay.transform.GetChild(9).GetComponent<Button>().onClick.AddListener(() => OnBtnStartCampaign());
        }
        else if (GameManager.Instance.myentryTYPE == entryTYPE.fromBonus )
        {

            UIPlay.transform.GetChild(9).gameObject.SetActive(false);
        }
        else if (GameManager.Instance.myentryTYPE == entryTYPE.fromLevel)
        {
            UIPlay.transform.GetChild(8).gameObject.SetActive(false);
            UIPlay.transform.GetChild(9).gameObject.SetActive(false);
        }

    }
    public void initializeUIonSinglerun()
    {
        int level = SceneManager.GetActiveScene().buildIndex;

        GameObject UIPlayPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIPlaySinglerun");
        GameObject UIPlay = Instantiate(UIPlayPrefab);
        Transform canv = GameObject.Find("Canvas").transform;
        UIPlay.transform.SetParent(canv);
        UIPlay.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIPlay.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIPlay.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);


        Image powerup = UIPlay.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        powerup.fillAmount = 0;

        textScore = UIPlay.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        textScore.text = GameManager.Instance.Score.ToString();


        //textDistance = UIPlay.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
        //textDistance.text = GameManager.Instance.Distance.ToString();


        textCoin = UIPlay.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Text>();
        textCoin.text = GameManager.Instance.Coin.ToString();

        UIPlay.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(() => OnBtnPaused());

        if (GameManager.Instance.myentryTYPE == entryTYPE.fromHOME)
        {
            UIPlay.transform.GetChild(7).gameObject.SetActive(false);
            UIPlay.transform.GetChild(8).gameObject.SetActive(true);
            UIPlay.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(() => OnBtnStartSinglerun());
        }

    }


    public void OnBtnStartCampaign()
    {
        StartCoroutine(countDownStart());
      
    }
    public void OnBtnStartSinglerun()
    {
       // GeckoController.Instance.startGecko();
        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);
        uiPlay.GetChild(7).gameObject.SetActive(true);
        uiPlay.GetChild(8).gameObject.SetActive(false);
      
    }

    IEnumerator countDownStart()
    {
        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);
        uiPlay.GetChild(9).gameObject.SetActive(false);
        uiPlay.GetChild(10).gameObject.SetActive(true);
        Text count = uiPlay.GetChild(10).GetChild(0).GetComponent<Text>();
        for (int i = 0; i < 3; i++)
        {
            count.gameObject.GetComponent<Animation>().Play();
            count.text = (3 - i).ToString();
            yield return new WaitForSecondsRealtime(1);
          
        }
        uiPlay.GetChild(8).gameObject.SetActive(true);
        uiPlay.GetChild(10).gameObject.SetActive(false);

      //  GeckoController.Instance.startGecko();
        StartCoroutine(GameManager.Instance.updateHP());
        StartCoroutine(GameManager.Instance.updateMana());
        StartCoroutine(GameManager.Instance.updateDistance());

    }

   
    public IEnumerator unfillUIPowerUPSlider()
    {
        float mana = GameManager.Instance.Mana;
        btnPowerRun.interactable = false;
        float time = 5;
        float t = 0.0f;
        while (t < time)
        {
            t += Time.deltaTime;
            sliderMana.value = Mathf.Lerp(mana, 0, t / time);
            yield return null;
        }
        GameManager.Instance.Mana = 0;
        StartCoroutine(GameManager.Instance.updateMana());

    }

    public void OnBtnPowerUP()
    {
        StartCoroutine(unfillUIPowerUPSlider());
        float powerupTime = SaveManager.playerData.powerupTime;
      //  StartCoroutine(GeckoBehaviour.Instance.statePowerup(powerupTime));
    }

   
    public void spawnUIscore(Vector3 pos, int score)
    {
        string path = "Prefabs/UI/UIscore";
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject uiScore = Instantiate(prefab);

        uiScore.GetComponentInChildren<Text>().text = score.ToString();
        if(score>0)
        {
            //Debug.Log("+");
           // uiScore.GetComponentInChildren<Text>().color = Color.white;
        }
        else
        {
            //Debug.Log("-");
           // uiScore.GetComponentInChildren<Text>().color = Color.red;
        }
        Transform spacer = GameObject.Find("Canvas").transform.GetChild(0).GetChild(10);
        uiScore.transform.SetParent(spacer);
        uiScore.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        uiScore.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        uiScore.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(pos);
        uiScore.GetComponent<RectTransform>().position = screenPos;
        Destroy(uiScore, 2);

    }

    public void spawnUIheart(Vector3 pos, int index)
    {
        string path = "Prefabs/UI/UIheart";
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject uiHeart = Instantiate(prefab);


        Transform spacer = GameObject.Find("Canvas").transform.GetChild(0).GetChild(11);
        uiHeart.transform.SetParent(spacer);
        uiHeart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        uiHeart.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        uiHeart.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(pos);
        uiHeart.GetComponent<RectTransform>().position = screenPos;

        //- heart
        if (index==0)
        {
            uiHeart.GetComponentInChildren<Animation>().clip = uiHeart.GetComponentInChildren<Animation>().GetClip("heart_minus");
            uiHeart.GetComponentInChildren<Animation>().Play();
        }
        //+heart
        else
        {
            uiHeart.GetComponentInChildren<Animation>().clip = uiHeart.GetComponentInChildren<Animation>().GetClip("heart_plus");
            uiHeart.GetComponentInChildren<Animation>().Play();
         //   StartCoroutine(uiHeart.GetComponent<Heart>().flow());
        }
        Destroy(uiHeart, 5);
    }

    public void updateUIsliderHP(float amount)
    {
        sliderHP.value = amount;
    }
    public void updateUIsliderMana(float amount)
    {
        sliderMana.value = amount;
        if (amount > 1)
        {
            btnPowerRun.interactable = true;
        }
    }

    public void updateUIScore(float score)
    {
        textScore.text = score.ToString();

    }
    public void updateUIDistance(float distance)
    {
        textDistance.text = distance.ToString() + "m";
        sliderDistance.value = distance;
    }

    public void extendUIDistance()
    {
        sliderDistance.maxValue = GameManager.Instance.TotalDistance;

    }
    public void updateUICoin(int coin)
    {
        textCoin.text = coin.ToString();
        textCoin.GetComponent<Animation>().Play();
    }
    public void updateUIWord(int index)
    {
        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);
        uiPlay.transform.GetChild(5).GetChild(0).GetChild(index).gameObject.SetActive(true);
    }
    

    
    public void updateUIJump(int amount)
    {

        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);

        for (int i = 0; i < 3; i++)
        {

            if (uiPlay.GetChild(14).GetChild(0).GetChild(i).GetComponent<Image>().color.a == 1)
            {
                Destroy(uiPlay.GetChild(14).GetChild(0).GetChild(i).gameObject);

                break;

            }


        }



    }

    public IEnumerator updateUIJumpSlider(float jumpFillTime)
    {
        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);
        Transform pJump = uiPlay.GetChild(14);
     
        bool canfill = true;
      
        while(canfill)
        {

            int indexJump = 0;

            GameObject uiJumpPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIJump");
            GameObject uiJump = Instantiate(uiJumpPrefab);
            uiJump.transform.SetParent(pJump.GetChild(0));
            uiJump.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            uiJump.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            uiJump.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

            Image imgJump = uiJump.GetComponent<Image>();

            float t = 0.0f;
            while (t < jumpFillTime)
            {
                t += Time.deltaTime;
                imgJump.fillAmount = Mathf.Lerp(0, 1, t / jumpFillTime);

                yield return null;
            }

            Color color = imgJump.color;
            color.a = 1.0f;
            imgJump.color = color;

            int amount = GameManager.Instance.addJump(1);
            if (amount == 3)
            {
                canfill = false;
            }
        }
            
      
      
    }
    
    public void updateUIItem(int index)
    {
        int indexItem = index;


       // string file = ((itemTYPE)indexItem).ToString();
       // Sprite sprite = Resources.Load<Sprite>("Sprites/" + file);

        Transform tItem = GameObject.Find("Canvas").transform.GetChild(0).GetChild(6).GetChild(0).GetChild(0);
        Image image = tItem.GetComponent<Image>();
       // image.sprite = sprite;

        //GameObject uiItemPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIItem");
        //GameObject uiItem = Instantiate(uiItemPrefab);
        //Transform spacer = GameObject.Find("Canvas").transform.GetChild(0).GetChild(6).GetChild(0).GetChild(0);
        //uiItem.transform.SetParent(spacer);
        //uiItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //uiItem.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        //uiItem.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);


        //string file = ((itemTYPE)indexItem).ToString();
        //Sprite sprite = Resources.Load<Sprite>("Sprites/" + file);
        //Image image = uiItem.transform.GetChild(0).GetComponent<Image>();
        //image.sprite = sprite;
        tItem.GetComponent<Button>().interactable = true;
        tItem.GetComponent<Button>().onClick.RemoveAllListeners();
      //  tItem.GetComponent<Button>().onClick.AddListener(() => GeckoBehaviour.Instance.stateItem(indexItem));


    }

    public IEnumerator updateUIItemSlider(int index)
    {
        int indexItem = index;

        GameObject uiItemGeckoPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIItemGecko");
        GameObject uiItemGecko = Instantiate(uiItemGeckoPrefab);

        Transform spacer = GameObject.Find("Canvas").transform.GetChild(0).GetChild(7);
        uiItemGecko.transform.SetParent(spacer);
        uiItemGecko.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        uiItemGecko.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        uiItemGecko.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        uiItemGecko.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

      //  string file = ((itemTYPE)indexItem).ToString();
       // Sprite sprite = Resources.Load<Sprite>("Sprites/" + file);
        Image image = uiItemGecko.transform.GetChild(0).GetComponent<Image>();
       // image.sprite = sprite;

        float itemTime = SaveManager.playerData.itemTime[indexItem];

        Image slider = uiItemGecko.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        float t = 0.0f;
        Vector3 offset = new Vector3(0,50f, 0);
        while (t < itemTime)
        {
            t += Time.deltaTime;
            slider.fillAmount = Mathf.Lerp(1, 0, t / itemTime);

          //  Vector3 screenPos = Camera.main.WorldToScreenPoint(GeckoController.Instance.gameObject.transform.position);
           // uiItemGecko.GetComponent<RectTransform>().position = screenPos+ offset;
            yield return null;
        }
        Debug.Log("destroy");
        Destroy(uiItemGecko);
    }
    






    public void OnBtnPaused()
    {
        Time.timeScale = 0;
        GameManager.isPlaying = false;

        GameObject uiPausedPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIPaused");
        GameObject UIPaused = Instantiate(uiPausedPrefab);
        Transform spacer = GameObject.Find("Canvas").transform;
        UIPaused.transform.SetParent(spacer);
        UIPaused.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIPaused.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -200);
        UIPaused.GetComponent<RectTransform>().offsetMin = new Vector2(100, 200);

        GameManager.Instance.myentryTYPE = entryTYPE.fromCANCEL;
        UIPaused.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnBtnResume(UIPaused));
        UIPaused.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBtnHome());
        UIPaused.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnBtnRestart());
    }


    public void OnBtnResume(GameObject UIPaused)
    {
        Time.timeScale = 1;
        GameManager.isPlaying = true;
        Destroy(UIPaused);
       
    }
    public void OnBtnOptain(GameObject UIActivatedGecko)
    {
        Destroy(UIActivatedGecko);

    }


    public void OnBtnHome()
    {
        GameManager.Instance.myentryTYPE = entryTYPE.fromCANCEL;
        Time.timeScale = 1;
        //GameManager.Instance.updatePlayerData();
        SceneManager.LoadScene("1_Home");
    }

    public void OnBtnRestart()
    {
        Time.timeScale = 1;
        GameManager.Instance.myentryTYPE = entryTYPE.fromRESTART;
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }

    


    public void updateRevive()
    {
        GameObject UIRevivePrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIRevive");
        GameObject UIRevive = Instantiate(UIRevivePrefab);
        Transform spacer = GameObject.Find("Canvas").transform;
        UIRevive.transform.SetParent(spacer);
        UIRevive.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIRevive.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        UIRevive.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

        UIRevive.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnBtnRevive(0));
        UIRevive.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnBtnRevive(1));

        UIRevive.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => updateDEAD());

    }



    public void OnBtnRevive(int index)
    {
        if(index==0)
        {
            //revive by watchingvideo
        }
        else if (index == 1)
        {
            //revive by coin
        }



    }
    public void updateDEAD()
    {
        

        GameManager.Instance.myentryTYPE = entryTYPE.fromDIE;
        GameObject UIDeadPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIDead");
        GameObject UIDead = Instantiate(UIDeadPrefab);
        Transform spacer = GameObject.Find("Canvas").transform;
        UIDead.transform.SetParent(spacer);
        UIDead.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIDead.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -200);
        UIDead.GetComponent<RectTransform>().offsetMin = new Vector2(100, 200);

       

        UIDead.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnBtnHome());
        UIDead.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = GameManager.Instance.Coin.ToString();
        UIDead.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = GameManager.Instance.Distance.ToString();

        int count = 0;
        for (int i = 0; i < 5; i++)
        {
          
            if(GameManager.Instance.OwendGECKO[i])
            {
                UIDead.transform.GetChild(2).GetChild(5).GetChild(i).gameObject.SetActive(true);
                count++;
            }
            else
            {
                UIDead.transform.GetChild(2).GetChild(5).GetChild(i).gameObject.SetActive(false);
            }
          
        }
        if(count==5)
        {
            //popup card ui menu and sest icon, name animal 
        }

        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);
        uiPlay.transform.GetChild(7).gameObject.SetActive(false);

    }

    public void updateWIN()
    {
        GameManager.Instance.myentryTYPE = entryTYPE.fromWIN;
        GameObject UIWinPrefab = Resources.Load<GameObject>("Prefabs/UI/" + "UIWin");
        GameObject UIWin = Instantiate(UIWinPrefab);
        Transform spacer = GameObject.Find("Canvas").transform;
        UIWin.transform.SetParent(spacer);
        UIWin.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIWin.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -200);
        UIWin.GetComponent<RectTransform>().offsetMin = new Vector2(100, 200);

        //save player data

        UIWin.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnBtnHome());


        Transform pCoin = UIWin.transform.GetChild(2).GetChild(0);
        pCoin.GetChild(1).GetComponent<Text>().text = GameManager.Instance.Coin.ToString();

        Transform pDistance = UIWin.transform.GetChild(2).GetChild(1);
        float distance = GameManager.Instance.Distance;
        pDistance.GetChild(1).GetChild(1).GetComponent<Text>().text = distance.ToString() ;

        StartCoroutine(fillDistance(distance));


        Transform pGECKO = UIWin.transform.GetChild(2).GetChild(2);
        int count = 0;
        for (int i = 0; i < 5; i++)
        {

            if (GameManager.Instance.OwendGECKO[i])
            {
                pGECKO.GetChild(1).GetChild(i).GetComponent<Text>().enabled = true;
                count++;
            }
            else
            {
                pGECKO.GetChild(1).GetChild(i).GetComponent<Text>().enabled = false;
            }

        }
        if (count == 5)
        {
            //popup card ui menu and sest icon, name animal 
        }

        Transform uiPlay = GameObject.Find("Canvas").transform.GetChild(0);
        uiPlay.transform.GetChild(7).gameObject.SetActive(false);

        /*
        int indexGecko = GameManager.levelData.f_mapIndexGecko;
        if (indexGecko != 0)
        {
            if(!SaveManager.playerData.activatedGECKO[indexGecko])
            {
                popupActivatedGecko(indexGecko);
            }
        }
       */
    }

    public void popupActivatedGecko(int index)
    {
     
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + "ui_pActivatedGecko");
        GameObject UIActivatedGecko = Instantiate(obj);
        Transform spacer = GameObject.Find("Canvas").transform;
        UIActivatedGecko.transform.SetParent(spacer);
        UIActivatedGecko.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        UIActivatedGecko.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -200);
        UIActivatedGecko.GetComponent<RectTransform>().offsetMin = new Vector2(100, 200);
        UIActivatedGecko.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


        string path1 = "Sprites/imgGecko" + index.ToString();
        Sprite sprite1 = Resources.Load<Sprite>(path1);
        UIActivatedGecko.transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
        UIActivatedGecko.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "you have optained " + ((geckoSkinTYPE)index).ToString();
        UIActivatedGecko.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnBtnOptain(UIActivatedGecko));

    

    }

    public IEnumerator fillDistance(float amount)
    {
        Debug.Log("fill distance slider" + amount);
        Transform uiWin = GameObject.Find("Canvas").transform.GetChild(1);
        Slider slider = uiWin.GetChild(2).GetChild(1).GetChild(1).GetChild(0).GetComponent<Slider>();

        int distanceRemain = 5;
        slider.maxValue = GameManager.Instance.TotalDistance - distanceRemain; 
        float startValue = 0;
        float targetValue = amount;

        float dur =2;
        float timer = 0f;

        while (timer < dur)
        {
            slider.value = Mathf.Lerp(startValue, targetValue, timer / dur);

            yield return null;
            timer += Time.deltaTime;
        }

    }


    public IEnumerator alarm(int index)
    {

        yield return new WaitForSeconds(5);

        GameObject Prefab = Resources.Load<GameObject>("Prefabs/UI/UIAlarm");
        GameObject alarm = Instantiate(Prefab);

      //  Sprite sprite = Resources.Load<Sprite>("Sprites/" + ((checkerTYPE)index).ToString());
      //  alarm.transform.GetChild(0).GetComponent<Image>().sprite = sprite;

        Transform spacer = GameObject.Find("Canvas").transform.GetChild(0).GetChild(3);
        alarm.transform.SetParent(spacer);
        alarm.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        alarm.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        alarm.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

        float t = 0.0f;
        float dur = 5;

        float updateTime = 0;
        float cooldown = 1;

        while (t < dur)
        {
            t += Time.deltaTime;
            if (updateTime < cooldown)
            {
                updateTime += Time.deltaTime;
            }
            else
            {

                alarm.SetActive(!alarm.activeInHierarchy);
                updateTime = 0.0f;
            }
            yield return null;
        }
        Destroy(alarm);
    }
    
    


    #endregion
}
