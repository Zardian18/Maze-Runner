using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorwayCollision : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER_TAG)
        {
            anim.SetTrigger("playerEnter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER_TAG)
        {
            anim.SetTrigger("playerExit");
        }
    }
}
