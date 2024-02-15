using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag_animation : MonoBehaviour
{
    public float amplitude = 1f;
    public float frequency = 1f;
    public float rotationSpeed = 50f;

    private float startTime;

    private Vector3 initPos;

    void Start()
    {
        startTime = Time.time;
        initPos = transform.localPosition;
    }

    void Update()
    {
        float yPos = Mathf.Sin((Time.time - startTime) * frequency) * amplitude;
        transform.localPosition = initPos + Vector3.forward * yPos;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
