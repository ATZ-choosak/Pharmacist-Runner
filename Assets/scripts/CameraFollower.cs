using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smooth = 10.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position , player.position + offset , smooth * Time.deltaTime);
    }
}
