using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float health = 100f;
    PlayerScript pScript;
    Animator anim;

    PlayerAudio audio;
    void Awake()
    {
        pScript = GetComponent<PlayerScript>();
        anim = GetComponent<Animator>();
        audio = GetComponent<PlayerAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        audio.PlayHitClip();
        if (health <= 0)
        {
            health = 0;
            anim.Play(Tags.DEAD_ANIM);
            pScript.enabled = false;
            GameplayController.instance.isPlayerAlive = false;

        }
        Debug.Log("Player health: "+ health);
        GameplayController.instance.DisplayHealth(int.Parse(health.ToString()));
    }
}
