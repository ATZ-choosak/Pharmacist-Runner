using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ragdollSystem : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();

    [SerializeField]
    private List<BoxCollider> boxColliders = new List<BoxCollider>();

    [SerializeField]
    private List<CapsuleCollider> capsuleColliders = new List<CapsuleCollider>();

    [SerializeField]
    private List<SphereCollider> sphereColliders = new List<SphereCollider>();

    [SerializeField]
    private GameObject root;

    [SerializeField]
    private CapsuleCollider capsule;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private CinemachineTargetGroup[] targetGroup;

    [SerializeField]
    private Movement movement;

    private void Start()
    {
        rigidbodies = root.transform.GetComponentsInChildren<Rigidbody>().ToList();
        boxColliders = root.transform.GetComponentsInChildren<BoxCollider>().ToList();
        capsuleColliders = root.transform.GetComponentsInChildren<CapsuleCollider>().ToList();
        sphereColliders = root.transform.GetComponentsInChildren<SphereCollider>().ToList();

        disableRagdoll();
        
    }

    public void disableRagdoll()
    {
        movement.setDead(false);

        foreach (CinemachineTargetGroup item in targetGroup)
        {
            item.m_Targets[0].weight = 1;
            item.m_Targets[1].weight = 0;
        }

        foreach (Rigidbody item in rigidbodies)
        {
            item.useGravity = false;
        }

        foreach (BoxCollider item in boxColliders)
        {
            item.enabled = false;
        }

        foreach (CapsuleCollider item in capsuleColliders)
        {
            item.enabled = false;
        }

        foreach (SphereCollider item in sphereColliders)
        {
            item.enabled = false;
        }
    }

    public void enableRagdoll()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        capsule.enabled = false;

        movement.setDead(true);

        scoreManager.instance.IsDead();

        foreach (CinemachineTargetGroup item in targetGroup)
        {
            item.m_Targets[0].weight = 0;
            item.m_Targets[1].weight = 1;
        }

        foreach (Rigidbody item in rigidbodies)
        {
            item.useGravity = true;
            if (item.velocity.magnitude != 0)
            {
                Vector3 dampeningDirection = item.velocity.normalized * -1.0f;
                item.AddForce(dampeningDirection * 10.0f);
            }
        }

        foreach (BoxCollider item in boxColliders)
        {
            item.enabled = true;
        }

        foreach (CapsuleCollider item in capsuleColliders)
        {
            item.enabled = true;
        }

        foreach (SphereCollider item in sphereColliders)
        {
            item.enabled = true;
        }
    }
}
