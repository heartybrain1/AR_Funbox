using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Chest : MonoBehaviour
{
    public float msToWait = 8000;

    public TextMeshProUGUI chestTimer;
    public Button chestButton;
    private ulong lastChestOpen;

    private void Start()
    {

       // chestButton = GetComponent<Button>();
        //lastChestOpen = ulong.Parse(PlayerPrefs.GetString("lastChestOpen"));


        lastChestOpen = SaveManager.playerData.chest;
     //   chestTimer = GetComponentInChildren<TextMeshProUGUI>();

        if (!isChestReady())
            chestButton.interactable = false;

    }

    private void Update()
    {

        if (!chestButton.IsInteractable())
        {
            if (isChestReady())

            {
                chestButton.interactable = true;
               
                return;
            }

            //set the timer
            ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            //hours
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;

            r += ((int)secondsLeft / 60).ToString("00") + "m ";

            r += (secondsLeft % 60).ToString("00") + "s"; ;
            chestTimer.text = r;
        }


    }


    public void chestClick()
    {
        lastChestOpen = (ulong)DateTime.Now.Ticks;

        SaveManager.playerData.chest = lastChestOpen;
        SaveManager.Instance.Save();

       //PlayerPrefs.SetString("lastChestOpen", lastChestOpen.ToString());

       chestButton.interactable = false;
    }

    private bool isChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;
        Debug.Log(secondsLeft);


        if (secondsLeft < 0)

        {
            chestTimer.text = "Ready";
            return true;
        }
           

        return false;

    }

    

}
