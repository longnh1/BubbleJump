using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTest : MonoBehaviour
{
    Collider2D[] inExplosionRadius = null;

    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { 
            Explode();
        }
    }

    void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        

        foreach (var item in inExplosionRadius)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log(item.name);
                Vector2 distanceVector = item.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = explosionForceMulti / distanceVector.magnitude;
                    rb.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
