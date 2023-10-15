using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class temp_UIManager : MonoBehaviour
{
    public Button btn;
    public Dropdown dropdown;
    public Text textChanged;

  
    void Start()
    {
        setUI();
        btn.onClick.AddListener(OnBtnClicked);
        dropdown.onValueChanged.AddListener(delegate { OnDropdownChanged(dropdown); });
    }
  
    void setUI()
    {
        /*
          string path1 = "PTYPEtmp/";
          Object[] objs = Resources.LoadAll(path1) ;
          int count = objs.Length;
          Debug.Log(objs);


          List<string> fileNames = new List<string>();
          */

        int count = System.Enum.GetValues(typeof(playType)).Length;
        List<string> fileNames = new List<string>();
        for (int i = 0; i < count; i++)
        {
            int index = i;
            playType myTYPE = (playType)index;
            string str = myTYPE.ToString();
            fileNames.Add(str);
        }

        for (int i = 0; i < count; i++)
        {
            Dropdown.OptionData options = new Dropdown.OptionData();

            options.text = fileNames[i];
            //options.text = objs[i].name;
            dropdown.options.Add(options);
        }
        dropdown.SetValueWithoutNotify(-1);
        dropdown.SetValueWithoutNotify(0);
    }


    private void OnDropdownChanged(Dropdown select)
    {
        string op = select.options[select.value].text;
        textChanged.text = op;
        Debug.Log("Dropdown Change!\n" + op);
    }

    void OnBtnClicked()
    {
        int index = dropdown.value;
        string txt = dropdown.options[index].text;
        temp_index.str = txt;

        playType myTYPE = (playType)System.Enum.Parse(typeof(playType), txt);
        temp_index.index = (int)myTYPE;

        SceneManager.LoadScene("_Test_Play");

    }
}
