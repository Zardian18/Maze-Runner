using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioClip runClip;
    [SerializeField]
    AudioClip attackClip;
    [SerializeField]
    AudioClip jumpClip;
    [SerializeField]
    AudioClip hitClip;
    [SerializeField]
    AudioSource aud;
    [SerializeField]
    AudioSource aud2;
    [SerializeField]
    AudioSource aud3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRunClip()
    {
        
        aud2.clip = runClip;
        
        if (!aud2.isPlaying)
        {
            aud2.Play();
        }
    }
    public void PlayAttackClip()
    {
        aud.clip = attackClip;
        aud.Play();
    }
    public void PlayjumpClip()
    {
        StopClip();
        aud.clip = jumpClip;
        aud.PlayDelayed(0.7f);
        Invoke("PLayClip", 0.71f);
    }
    public void PlayHitClip()
    {
        aud3.clip = hitClip;
        if (!aud3.isPlaying)
        {
            aud3.Play();
        }
    }
    public void RunAttack()
    {
        aud2.clip = attackClip;
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }
    public void StopClip()
    {
        aud2.Pause();
    }
    public void PlayClip()
    {
        aud2.Play();
    }

    
}
