using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHome : MonoBehaviour
{

    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => setMenuTheme());

        updateLife();
    }


    void setMenuTheme()
    {
        transform.parent.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();

        gameObject.SetActive(false);

    }

    void updateLife()
    {
        int life = GameManager.Instance.Life;
        Transform tLife = transform.parent.GetChild(2).GetChild(0);
        for (int i = 0; i < life; i++)
        {
            tLife.GetChild(i).gameObject.SetActive(true);
        }

    }



}
