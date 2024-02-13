using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 0.1f , jump = 1.0f;
    Rigidbody rb;
    Animator animator;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private bool IsGround , IsSlide;

    [SerializeField]
    private float maximunWay = 3.0f;

    float currentWay = 0.0f;

    BoxCollider boxCollider;

    [SerializeField]
    private float setCenterBox, setSizeYBox;

    float initCenterBox , initSizeYBox;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        initCenterBox = boxCollider.center.y;
        initSizeYBox = boxCollider.size.y;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

        //jump Up
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            rb.AddForce(Vector3.up * jump ,ForceMode.Impulse);
        }

        //jump Down
        if (Input.GetKeyDown(KeyCode.LeftControl) && !IsGround)
        {
            rb.AddForce(Vector3.down * jump, ForceMode.Impulse);
        }

        //Slide
        if (Input.GetKeyDown(KeyCode.LeftControl) && IsGround && !IsSlide)
        {
            boxCollider.center = new Vector3(0 , setCenterBox , 0);
            boxCollider.size = new Vector3(1 , setSizeYBox , 1);
            Invoke("resetBox" , 1.0f);
            IsSlide = true;
        }

        animator.SetBool("IsJump" , Input.GetKeyDown(KeyCode.Space) && IsGround);
        animator.SetBool("IsGround" , IsGround);
        animator.SetBool("IsSlide" , Input.GetKeyDown(KeyCode.LeftControl) && IsGround);

        RaycastHit hit;

        IsGround = Physics.Raycast(transform.position + new Vector3(0, 0.5f , 0), Vector3.down , out hit , 1f , layer);

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentWay -= maximunWay;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentWay += maximunWay;
        }

        currentWay = Mathf.Clamp(currentWay , -maximunWay , maximunWay);

        transform.position = Vector3.Lerp(transform.position , new Vector3(currentWay , transform.position.y , transform.position.z) , 10.0f * Time.deltaTime);

    }

    void resetBox()
    {
        boxCollider.center = new Vector3(0, initCenterBox, 0);
        boxCollider.size = new Vector3(1, initSizeYBox, 1);
        IsSlide = false;
    }
}
