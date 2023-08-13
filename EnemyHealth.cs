using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float health = 30f;
    EnemyScript eScript;
    Animator anim;
    EnemyAudio audio;
    void Awake()
    {
        eScript = GetComponent<EnemyScript>();
        anim = GetComponent<Animator>();
        audio = GetComponent<EnemyAudio>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            anim.Play(Tags.DEAD_ANIM);
            eScript.enabled = false;
            audio.PlayDieClip();
            audio.enabled = false;
            Invoke("DeactivateEnemy", 3f);
        }
        Debug.Log("Enemy health: "+ health);
    }

    void DeactivateEnemy()
    {
        gameObject.SetActive(false);
    }

}
