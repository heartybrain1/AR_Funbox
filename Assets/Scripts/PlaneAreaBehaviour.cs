using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlaneAreaBehaviour : MonoBehaviour
{
    public TextMeshPro areaText;
    public ARPlane arPlane;

    public ARPlaneManager arPlaneManager;
  
    private void ArPlane_BoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
    {
        //arPlane = obj.plane;
  
        areaText.text = CalculatePlaneArea(arPlane).ToString();
    }
    private float CalculatePlaneArea(ARPlane plane)
    {
        return plane.size.x * plane.size.y;
    }
    public void ToggleAreaView()
    {
        if (areaText.enabled)
            areaText.enabled = false;
        else
            areaText.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        arPlane.boundaryChanged += ArPlane_BoundaryChanged;
      
    }
    private void OnDisable()
    {
        arPlane.boundaryChanged -= ArPlane_BoundaryChanged;
    }


    // Update is called once per frame
    void Update()
    {
        areaText.transform.rotation =
      Quaternion.LookRotation(areaText.transform.position -
         Camera.main.transform.position);

    }
}
