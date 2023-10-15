using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Text text;
    public Image image;
    private void Start()
    {
        StartCoroutine(AsynchronousLoad("1_Home"));
    }

    IEnumerator AsynchronousLoad(string index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            image.fillAmount = progress;
            text.text = progress * 100f + "%";
            yield return null;
            
        }
    }
}
