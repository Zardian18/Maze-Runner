using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip runClip;
    [SerializeField]
    AudioClip attackClip;
    [SerializeField]
    AudioClip deadClip;
    [SerializeField]
    AudioSource aud;
    [SerializeField]
    AudioSource aud2;
    [SerializeField]
    AudioSource aud3;
    // Start is called before the first frame update
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

        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }
    public void PlayDieClip()
    {

        aud3.clip = deadClip;

        if (!aud3.isPlaying)
        {
            aud3.Play();
        }
    }
}
