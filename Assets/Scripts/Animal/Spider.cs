using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    Transform target;
    bool activated = false;
    public float speed = 0.5f;
    float height = 0;
    public float damping = 1;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
        height = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance < 2)
            {
                activated = true;
                GetComponentInChildren<Animator>().SetTrigger("walk");
            }
        }
           
        if(activated)
        {
            // transform.LookAt(target);
            var step = speed * Time.deltaTime; // calculate distance to move
           
            Vector3 targetPos = Vector3.MoveTowards(transform.position, target.position, step);

            targetPos.y = height;
            transform.position = targetPos;

            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            // var rotation = Quaternion.LookRotation(target.position - transform.position);
            //  transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
        }
       

    }
}
