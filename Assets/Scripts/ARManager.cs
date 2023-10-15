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
        PlacePrefab();
        PlaceIndicator();
        //PlayerMove();
    }


    #region 바닥에 프리팹 놓기

    public ARRaycastManager arRaycater;
    public GameObject spawnPrefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();



    public void loadScene()
    {
        ARSession arsession = GameObject.Find("AR Session").GetComponent<ARSession>();
        arsession.Reset();
        SceneManager.LoadScene(0);
    }
    void PlacePrefab()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (arRaycater.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            //Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);

            string str = temp_index.str;

            string path1 = "Prefabs/" + str;

            GameObject gamePrefab = Resources.Load(path1) as GameObject;
          
            GameObject obj = Instantiate(gamePrefab, hitPose.position, Quaternion.identity);
            //obj.transform.position = ars + transform.TransformDirection(Vector3.forward * 3);
            //obj.transform.position = obj.transform.position + transform.TransformDirection(Vector3.forward*3);
            // obj.transform.position = obj.transform.position + new Vector3(0, 0, 3);

        }
    }

    #endregion


    #region 바닥 활성화

    public ARPlaneManager arPlane;

    public void ShowPlane(bool b)
    {
        foreach (var plane in arPlane.trackables)
            plane.gameObject.SetActive(b);
    }

    #endregion


    #region 바닥 표시기

    public Transform Indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();

    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            Indicator.position = indicatorHits[0].pose.position;
            Indicator.rotation = indicatorHits[0].pose.rotation;
        }
    }

    public void PlaceIndicatorPrefab()
    {
        Pose hitPose = indicatorHits[0].pose;
        Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
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

    #endregion


    


    #region 빛 감지

    public ARCameraManager arCameraManager;
    public Light currentLight;

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

    #endregion


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
    XROrigin arOrigin;

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



