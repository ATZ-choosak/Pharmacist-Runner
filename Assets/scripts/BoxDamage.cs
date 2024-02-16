using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamage : MonoBehaviour
{
    MeshCollider meshCollider;
    BoxCollider boxCollider;

    private void Start()
    {
        if (GetComponent<MeshCollider>())
        {
            meshCollider = GetComponent<MeshCollider>();
        }

        if (GetComponent<BoxCollider>())
        {
            boxCollider = GetComponent<BoxCollider>();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!scoreManager.instance.movement.speed_buff)
        {
            if (collision.transform.CompareTag("Player"))
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    // Check the normal of each contact point
                    if (contact.normal.y > 0)
                    {
                        Animator animator = collision.transform.GetComponent<Animator>();
                        ragdollSystem ragdoll = collision.transform.GetComponent<ragdollSystem>();
                        Movement movement = collision.transform.GetComponent<Movement>();

                        movement.setRunSpeed(0);
                        animator.enabled = false;
                        ragdoll.enableRagdoll();
                    }

                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (meshCollider)
        {
            meshCollider.enabled = !scoreManager.instance.movement.speed_buff;
        }

        if (boxCollider)
        {
            boxCollider.enabled = !scoreManager.instance.movement.speed_buff;
        }

    }
}
