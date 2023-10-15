using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stork : MonoBehaviour
{
    Transform target;
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(!activated)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance < 2)
            {
                activated = true;
                GetComponent<Animator>().SetTrigger("fly");
            }

        }
       

    }



}
