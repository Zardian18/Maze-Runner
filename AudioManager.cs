using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip hoverClip;
    [SerializeField]
    AudioClip clickClip;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = clip;
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {
        aud.Pause();
        aud.clip = hoverClip;
        aud.Play();
    }
    public void OnHoverExit()
    {
        aud.clip = clip;
        aud.Play();
    }
    public void OnClick()
    {
        aud.clip = clickClip;
        aud.Play();
        SceneManager.LoadScene(1);
    }
}
