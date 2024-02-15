using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;
    AudioSource audioSource;

    [SerializeField]
    private float stepPitch = 0.5f , timeToResetPicth = 3.0f;

    [SerializeField]
    private AudioClip coin;

    public int score = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void addScore(int s)
    {
        score += s;
        audioSource.pitch += stepPitch;
        audioSource.PlayOneShot(coin);
        Invoke("resetPicth" , 1.0f);
    }
    
    void resetPicth()
    {
        audioSource.pitch = 0;
    }
}
