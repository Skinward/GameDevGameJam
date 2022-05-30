using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] songs;

    private new AudioSource audio;
    private int scene = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.clip = songs[0];
        audio.Play();
        
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        if(scene == 3 && audio.clip != songs[1])
        {
            audio.Stop();
            audio.clip = songs[1];
            audio.Play();
        }
        else if(scene != 3 && audio.clip != songs[0])
        {
            audio.Stop();
            audio.clip = songs[0];
            audio.Play();
        }
    }



}
