using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sfxWord, sfxCollide, sfxUIbtn, sfxUIlocked, sfxUIupgrade, sfxUIperchase
public enum soundTYPE { ambiance,bgm1,bgm2,bgmBonus,bgmHome, jingleWin, jingleLose, sfxEcho, sfxCoin, sfxItem, sfxHP, sfxEat }
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Coroutine corBGM;
   
    private AudioSource sourceAmbiance;
    private AudioSource sourceBackgroundM;
    private AudioSource sourcePlayer;
   
    private float bookmark = 0;

    void Awake()
    {
        Instance = this;
        sourceAmbiance = transform.GetChild(0).GetComponent<AudioSource>();
        sourceBackgroundM = transform.GetChild(1).GetComponent<AudioSource>();
        sourcePlayer = transform.GetChild(2).GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
      
    }

    public void setSoundHome()
    {
        int index = (int)soundTYPE.bgmHome;
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        sourceBackgroundM.clip = clip;
        sourceBackgroundM.Play();
    }

    public void setSoundBonus()
    {
        int index = (int)soundTYPE.bgmBonus;
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        sourceBackgroundM.clip = clip;
        sourceBackgroundM.Play();
    }

    public void setSoundJingleWin()
    {
        sourceBackgroundM.Stop();
        int index = (int)soundTYPE.jingleWin;
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        sourcePlayer.PlayOneShot(clip, 1);
    }
    public void setSoundJingleLose()
    {
        sourceBackgroundM.Stop();
        int index = (int)soundTYPE.jingleLose;
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        sourcePlayer.PlayOneShot(clip, 1);
    }

    public IEnumerator audioTransition()
    {
        int index = (int)soundTYPE.ambiance;
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        sourceAmbiance.clip = clip;
        sourceAmbiance.Play();

        index = (int)soundTYPE.bgm1;
        clip = GameManager.Instance.prefabAudioClips[index];
        sourceBackgroundM.clip = clip;

        while (true)
        {
            int interval1 = Random.Range(5, 10);
            yield return new WaitForSeconds(interval1);

            sourceBackgroundM.time = bookmark;
            sourceBackgroundM.Play();
            sourceBackgroundM.volume = 0f;
          
            float startVolBGM = 0;
            float endVolBGM = 1;

            float startVolAMB = 1;
            float endVolAMB = 0.2f;
            float t;
           
            t = 0.0f;
            while (t < 5)
            {
                t += Time.deltaTime;
                float vol1 = Mathf.Lerp(startVolBGM, endVolBGM, t / 5);
                sourceBackgroundM.volume = vol1;

                float vol2 = Mathf.Lerp(startVolAMB, endVolAMB, t / 5);
                sourceAmbiance.volume = vol2;
                yield return null;
            }
            sourceBackgroundM.volume = 1f;

            int interval2 = Random.Range(20, 30);
            yield return new WaitForSeconds(interval2);

            startVolBGM = 1;
            endVolBGM = 0;

            startVolAMB = 0.2f;
            endVolAMB = 1;

            t = 0.0f;
            while (t < 5)
            {
                t += Time.deltaTime;
                float vol1 = Mathf.Lerp(startVolBGM, endVolBGM, t / 5);
                sourceBackgroundM.volume = vol1;

                float vol2 = Mathf.Lerp(startVolAMB, endVolAMB, t / 5);
                sourceAmbiance.volume = vol2;
                yield return null;
            }
            sourceBackgroundM.volume = 0f;
            bookmark = sourceBackgroundM.time;
            sourceBackgroundM.Stop();
        }
    }


    public void setPlayerAudiosource()
    {
        sourcePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }


    public void playerSFX(int index)
    {
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        //sourcePlayer.pitch = Random.Range(lowPitchRange, highPitchRange);
        //float randomVol = Random.Range(0.5f, 1.0f);
        sourcePlayer.PlayOneShot(clip, 1);
    }
   

    public void uiSFX(int index)
    {
        AudioClip clip = GameManager.Instance.prefabAudioClips[index];
        //sourcePlayer.pitch = Random.Range(lowPitchRange, highPitchRange);
        //float randomVol = Random.Range(0.5f, 1.0f);
        sourcePlayer.PlayOneShot(clip, 1);
    }
}
