using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneLoader : MonoBehaviour
{

    protected abstract void sceneLoad();
    private void Start()
    {
        sceneLoad();
    }
   


}
