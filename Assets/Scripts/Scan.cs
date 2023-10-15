using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;

public class Scan : MonoBehaviour
{

    public Button btnScan;
    public Button btnSpawn;
    public Text txtProgress;

    XROrigin arOrigin;
    public ARPlaneManager arPlaneManager;
    public ARPointCloudManager arPointManager;

    bool scanning = false;

    float sliderValye;

    public void ShowPlane(bool b)
    {
        foreach (var plane in arPlaneManager.trackables)
            plane.gameObject.SetActive(b);

    
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
     


        if (scanning)
        {

        }
    }

    public void OnSelectScan()
    {
        btnScan.interactable = false;
        arPointManager.enabled = true;
        arPlaneManager.enabled = true;
        scanning = true;

    }
    void calculatePlane()
    {
        float total = 0;
       
        foreach (var plane in arPlaneManager.trackables)
        {
            float value = plane.size.x * plane.size.y;
            total = total = value;
        }
         
    }

    public void OnSelectSpawn()
    {


    }

    public void OnFinishedScan()
    {
        btnSpawn.interactable = true;
    }
}
