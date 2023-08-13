using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject player;
    Rigidbody myBody;
    Animator anim;
    [SerializeField]
    float enemySpeed = 10f;
    [SerializeField]
    float enemyWatchThreshold = 70f;
    [SerializeField]
    float enemyAttackThreshold = 6f;
    [SerializeField]
    GameObject damagePoint;

    EnemyAudio audio;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        myBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<EnemyAudio>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (GameplayController.instance.isPlayerAlive)
        {
            EnemyAI();

        }
        else
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIM)|| anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIM))
            {
                anim.SetTrigger(Tags.STOP_TRIGGER);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyAI()
    {
        Vector3 direction = player.transform.position - transform.position;
        float mag = direction.magnitude;
        direction.Normalize();

        Vector3 velocity = direction * enemySpeed;
        if(mag>enemyAttackThreshold && mag< enemyWatchThreshold)
        {
            myBody.velocity = new Vector3(velocity.x, myBody.velocity.y, velocity.z);
            /*if (anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIM))
            {
                //anim.SetTrigger(Tags.STOP_TRIGGER);
                anim.SetTrigger(Tags.RUN_ANIM);
            }*/
            anim.SetTrigger(Tags.RUN_ANIM);
            audio.PlayRunClip();

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
           // anim.SetTrigger(Tags.RUN_ANIM);
        }

        else if (mag < enemyAttackThreshold)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIM))
            {
                anim.SetTrigger(Tags.ATTACK_ANIM);
                audio.PlayAttackClip();
            }
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        }

        else
        {
            myBody.velocity = Vector3.zero;
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIM)|| anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIM))
            {
                anim.SetTrigger(Tags.STOP_TRIGGER);
            }
        }
    }

    void ActivateDamagePoint()
    {
        damagePoint.SetActive(true);
    }
    void DeactivateDamagePoint()
    {
        damagePoint.SetActive(false);
    }
}
