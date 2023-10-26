using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class ARManager : MonoBehaviour
{

    public static ARManager Instance;

    XROrigin arOrigin;

    public ARRaycastManager arRaycater;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public bool isIndicatable = false;

    public GameObject indicator;

    bool isSpawned = false;
    public ARPlaneManager arPlane;
    public GameObject gamePrefab;

    public GameObject spawnedObj;


    public ARCameraManager arCameraManager;
    public Light currentLight;
  
  
    public  void getPrefab()
    { 

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


        themeType myType = (themeType)GameManager.indexTheme;
        string nameTheme = myType.ToString();
        int index = GameManager.indexLevel;



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

    public void updateTargePlaneValue()
    {


    }


    private void Awake()
    {
        getPrefab();
        Instance = this;
    }

    void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdated;
        //arImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdated;
        //arImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void Update()
    {
        if(isIndicatable)
        {
            updateIndicator();
        }
        if (isSpawned)
        {
            //OnTouched();
        }

        
        //PlayerMove();
    }

    void updateIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            indicator.transform.position = hits[0].pose.position;
            indicator.transform.rotation = hits[0].pose.rotation;
        }
    }

    public void showOnIndicator()
    {
        string path = "temp/" + "Indicator";
        GameObject obj = Resources.Load(path) as GameObject;
        indicator =  Instantiate(obj, transform.position, Quaternion.identity);
        isIndicatable = true ;

    }
    public void showOffIndicator()
    {
        indicator.SetActive(false);
        isIndicatable = false;
       
    }


    public void spawnGameObject()
    {
        Pose hitPose = hits[0].pose;
        spawnedObj= Instantiate(gamePrefab, hitPose.position, Quaternion.identity);
        isSpawned = true;
    }



    void OnTouched()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                hit.collider.gameObject.GetComponent<Interactable>().OnTouchDetected();
            }
        }
    }

    /*

     if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(raycast, out RaycastHit raycastHit))
                    {
                        var planeAreaBehaviour =
       raycastHit.collider.gameObject.GetComponent<PlaneAreaBehaviour>();
                        if (planeAreaBehaviour != null)
                        {
                          //  planeAreaBehaviour.ToggleAreaView();
                        }
                    }

                }
            }
        }
   

  */



    /*
    void PlacePrefabOld()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (arRaycater.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            //Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
            GameObject obj = Instantiate(gamePrefab, hitPose.position, Quaternion.identity);
        }
    }


    public void placeIndexPrefab()
    {
        string str = temp_index.str;
      
        string path1 = "Prefabs/" + str;

        GameObject gamePrefab = Resources.Load(path1) as GameObject;
        Pose hitPose = indicatorHits[0].pose;
        GameObject obj= Instantiate(gamePrefab, hitPose.position, Quaternion.identity);
        obj.transform.position = obj.transform.position + new Vector3(0, 0, 3);

    }




    */


    public void resetARSession()
    {
        ARSession arsession = GameObject.Find("AR Session").GetComponent<ARSession>();
        arsession.Reset();

    }

    public void ShowPlane(bool b)
    {
        foreach (var plane in arPlane.trackables)
            plane.gameObject.SetActive(b);
    }


    void FrameUpdated(ARCameraFrameEventArgs args)
    {
        var brightness = args.lightEstimation.averageBrightness;

        if (brightness.HasValue)
        {
            bool isBright = brightness.Value > 0.3f;
            float fixBrightness = brightness.Value * 4f;
            currentLight.intensity = fixBrightness;
            print($"밝기 : {brightness.Value} \n빛의 세기 : {fixBrightness} \n밝음 : {isBright}");
        }
    }







    #region 이미지 감지


    public ARTrackedImageManager arImageManager;

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        List<ARTrackedImage> addedImages = args.added;

        foreach (ARTrackedImage image in addedImages)
        {
            if (image.referenceImage.name == "R2")
                image.GetComponent<Renderer>().material.color = Color.red;

            else if (image.referenceImage.name == "R3")
                image.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    #endregion



    #region 플레이어를 중심으로 이동

    public NavMeshAgent agent;
    public GameObject TouchParticle;

   
    public void MoveTarget()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            agent.SetDestination(hit.point);
            Destroy(Instantiate(TouchParticle, hit.point, Quaternion.identity), 3);
        }
    }

    void PlayerMove()
    {
        PlayerPos.playerPos = agent.transform.position;
        arOrigin.transform.position = PlayerPos.playerPos;
        SetMapCenter(PlayerPos.playerPos);
    }

    #endregion


    #region 머테리얼 숨기기

    public Material[] MapMts;

    void Start() => SetMapRadius(40f);
    void OnApplicationQuit() => SetMapRadius(float.MaxValue);

    void SetMapCenter(Vector3 vec)
    {
        Vector4 myVec = new Vector4(vec.x, vec.y, vec.z, 0);

        for (int i = 0; i < MapMts.Length; i++)
            MapMts[i].SetVector("_Center", myVec);
    }

    void SetMapRadius(float r)
    {
        for (int i = 0; i < MapMts.Length; i++)
            MapMts[i].SetFloat("_Radius", r);
    }

    #endregion
}
public static class PlayerPos
{
    public static Vector3 playerPos;
}





