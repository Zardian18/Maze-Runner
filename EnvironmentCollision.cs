using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCollision : MonoBehaviour
{
    Animator anim;
    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            player.canAttack = false;
            if (!anim.GetBool("Idle"))
            {
                anim.SetBool("Idle", true);
            }
        }
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            player.canAttack = true;
        }
    }
}
