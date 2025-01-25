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

    private void OnMouseDown()
    {
        Explode();
    }

    public void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        

        foreach (var item in inExplosionRadius)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log(item.name);

                PlayerMovement playerMovement = rb.transform.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.IsExploded = true;
                }
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;

                Vector2 distanceVector = item.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = explosionForceMulti / distanceVector.magnitude;

                    Debug.Log(distanceVector.normalized);
                    rb.AddForce(distanceVector.normalized * explosionForce);

                    //rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
