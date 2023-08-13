using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    float damageAmount = 10f;
    [SerializeField]
    LayerMask playerLayer;
    //bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f, playerLayer);
        if (hits.Length > 0)
        {
            if(hits[0].tag == Tags.PLAYER_TAG)
            {
                hits[0].gameObject.GetComponent<PlayerHealth>().DealDamage(damageAmount);
                Debug.Log("Hit the player");

            }
        }
    }

    /*IEnumerator ToogleCanHit(float t)
    {
        canDamage = false;
        yield return new WaitForSeconds(t);
        canDamage = true;

    }*/
}
