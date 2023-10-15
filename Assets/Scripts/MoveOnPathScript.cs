using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPathScript : MonoBehaviour
{

    public EditorPathScript pathToFollow;


    public int currentWayPoinID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(pathToFollow.path_objs[currentWayPoinID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[currentWayPoinID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(pathToFollow.path_objs[currentWayPoinID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);


        if(distance<=reachDistance)
        {
            currentWayPoinID++;
            

        }
        if(currentWayPoinID>=pathToFollow.path_objs.Count)
        {
            currentWayPoinID = 0;
            transform.position = pathToFollow.path_objs[0].position;




        }
        
    }
}
