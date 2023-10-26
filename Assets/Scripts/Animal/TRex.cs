using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : Interactable
{
    Transform tPlayer;
    Animator animator;
    public float speed;
    AudioClip audioFootstep;
    AudioClip audioRoar1;
    AudioClip audioRoar2;
    AudioClip audioRoar3;
    AudioSource audioSource;
    // Start is called before the first frame update

    protected override void interaction()
    {
        Debug.Log("");

    }



    void Start()
    {
        tPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        animator =   GetComponent<Animator>();
        audioSource.clip = audioFootstep;
        audioSource.Play();
        StartCoroutine(loopAnimationState());
        StartCoroutine(loopRandomAudio());
    }


   
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, tPlayer.position, speed * Time.deltaTime);
        transform.LookAt(tPlayer);
    }


    IEnumerator loopRandomAudio()
    {

        while (true)
        {
          
            yield return new WaitForSeconds(0.5f);
            int chance = Random.Range(0, 3);
            if (chance == 0)
            {
                audioSource.PlayOneShot(audioRoar1);
            }
            else if (chance == 1)
            {
                audioSource.PlayOneShot(audioRoar2);
            }
            else
            {
                audioSource.PlayOneShot(audioRoar3);
            }


        }

    }




    IEnumerator loopAnimationState()
    {

        while(true)
        {
            yield return new WaitForSeconds(0.5f);

        }
      
    }

    void calcDistancePlayer()
    {
        float distance = Vector3.Distance(tPlayer.position, transform.position);
        if(distance<0.2)
        {

        }


    }


    void run()
    {
        animator.SetTrigger("run");
    }
    void walk()
    {
        animator.SetTrigger("walk");
    }
    void roar()
    {
        animator.SetTrigger("roar");
    }


}
