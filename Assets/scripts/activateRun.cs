using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateRun : MonoBehaviour
{
    [SerializeField]
    private Movement movement;

    private void Awake()
    {
        movement.enabled = true;
    }
}
