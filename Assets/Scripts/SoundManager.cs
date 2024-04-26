using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip cardFlipSound;
    public AudioClip matchingSound;
    public AudioClip mismatchingSound;
    public AudioClip gameEndSound;
    public AudioSource backGroundSrc;
    
    private AudioSource audioSource;
    
    public static SoundManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if(sound!= null)
            audioSource.PlayOneShot(sound);
    }
    
    public void PlayGameEndClip()
    {
        backGroundSrc.Stop();
        audioSource.PlayOneShot(gameEndSound);
    }
    
}
