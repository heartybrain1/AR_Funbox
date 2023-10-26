using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public Transform uiMain;
    public Transform uiSub;
    public Button btnScan;
    public Button btnSpawn;

    public GameObject toggleRecord;


    ARPlaneManager arPlaneManager;
    ARPointCloudManager arPointManager;
    ARRaycastManager arRaycastManager;

    bool scanning = false;

    private void Awake()
    {
        Instance = this;
    }
    float scannedValue =0;
    public float maxValue;

    public void ShowPlane(bool b)
    {
        foreach (var plane in arPlaneManager.trackables)
            plane.gameObject.SetActive(b);


    }

    public void toggleRecordingUI()
    {
        toggleRecord.SetActive(!toggleRecord.gameObject.activeInHierarchy);
    }

    public void updatePlaneValue(float amount)
    {
        scannedValue = amount;

    }


    // Start is called before the first frame update
    void Start()
    {
        arPlaneManager = GameObject.Find("XR Origin").GetComponent<ARPlaneManager>();
        arPointManager = GameObject.Find("XR Origin").GetComponent<ARPointCloudManager>();

    }
    public void Home()
    {
        SceneManager.LoadScene("1_Home");
    }

    public void OnSelectScan()
    {
        btnScan.interactable = false;

        arPointManager.enabled = true;
        arPlaneManager.enabled = true;

        uiMain.GetChild(2).gameObject.SetActive(true);
        scanning = true;

        StartCoroutine(fillSlider());

    }



    public void OnSelectSpawn()
    {


        foreach (var plane in arPlaneManager.trackables)
        {
            plane.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
            plane.GetComponent<MeshRenderer>().enabled = false;
        }
         

        arRaycastManager = GameObject.Find("XR Origin").GetComponent<ARRaycastManager>();
        arRaycastManager.enabled = true;
        uiMain.gameObject.SetActive(false);
        ARManager.Instance.showOnIndicator();
        uiSub.gameObject.SetActive(true);



    }

    public void rotateClock()
    {

        ARManager.Instance.spawnedObj.transform.eulerAngles = ARManager.Instance.spawnedObj.transform.eulerAngles + new Vector3(0, 30, 0);



    }
    public void rotateInverseClock()
    {
        ARManager.Instance.spawnedObj.transform.eulerAngles = ARManager.Instance.spawnedObj.transform.eulerAngles - new Vector3(0, 30, 0);

    }
    public void scaleOrigin()
    {
        ARManager.Instance.spawnedObj.transform.localScale = new Vector3(1, 1, 1);
    }

    public void scaleHalf()
    {
        ARManager.Instance.spawnedObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    public void scaleQuad()
    {
        ARManager.Instance.spawnedObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public void OnSelectPosition()
    {
        ARManager.Instance.spawnGameObject();
        ARManager.Instance.showOffIndicator();
        uiSub.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
    }

    public void OnSelectRotation()
    {

        uiSub.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = false;
    }

    public void OnSelectScale()
    {
        uiSub.transform.GetChild(2).GetChild(0).GetComponent<Button>().interactable = false;

    }

    public void OnFinishSpawned()
    {
        uiSub.gameObject.SetActive(false);
    }

    public void OnFinishedScan()
    {
     
       
        btnSpawn.interactable = true;
        uiMain.GetChild(2).gameObject.SetActive(false);
        arPlaneManager.enabled = false;
        arPointManager.enabled = false;
    }


    IEnumerator fillSlider()
    {

        Slider slider = uiMain.GetChild(2).GetChild(0).GetComponent<Slider>();
        slider.maxValue = maxValue;
        float t = 0.0f;


        while (slider.value< maxValue)
        {
            t += Time.deltaTime;
            slider.value = Mathf.Lerp(0, scannedValue, t / 3);


            yield return null;
        }


        OnFinishedScan();


    }
}
