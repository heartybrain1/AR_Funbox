using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungbeetle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("walk");
        transform.GetChild(0).GetChild(1).GetComponent<Animator>().SetTrigger("roll");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
