using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
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
