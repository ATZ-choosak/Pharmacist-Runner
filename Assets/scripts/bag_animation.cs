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

    item item;

    void Start()
    {
        startTime = Time.time;
        initPos = transform.localPosition;

        item = GetComponent<item>();
    }

    void Update()
    {
        bool Is_boot = item.type == item.ItemType.jump_boot;

        float yPos = Mathf.Sin((Time.time - startTime) * frequency) * amplitude;
        transform.localPosition = initPos + (Is_boot ? Vector3.up : Vector3.forward) * yPos;
        transform.Rotate((Is_boot ? Vector3.up : Vector3.forward) * rotationSpeed * Time.deltaTime);
    }
}
