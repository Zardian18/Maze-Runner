using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    float damageAmount = 10f;
    [SerializeField]
    LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f, enemyLayer);
        if (hits.Length > 0)
        {
            if (hits[0].tag == Tags.ENEMY_TAG)
            {
                hits[0].gameObject.GetComponent<EnemyHealth>().DealDamage(damageAmount);
                Debug.Log("Hit the enemy");
            }
        }
    }
}
