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
    private bool IsGround , IsSlide , Islook;

    [SerializeField]
    private float maximunWay = 3.0f;

    float currentWay = 0.0f;

    CapsuleCollider Collider;

    [SerializeField]
    private float setCenterBox, setSizeYBox;

    float initCenterBox , initSizeYBox , stateLook;

    [SerializeField]
    private float rayToGround = 0.1f;


    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       animator = GetComponent<Animator>();
       Collider = GetComponent<CapsuleCollider>();
       initCenterBox = Collider.center.y;
       initSizeYBox = Collider.height;
       InvokeRepeating("randomLook" , 1.0f , 5.0f);
    }

    void randomLook()
    {

        Islook = Random.Range(0 , 5) == 3 ? true : false;
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

        //jump Up
        if (Input.GetKeyDown(KeyCode.Space) && IsGround && !IsSlide)
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
            Collider.center = new Vector3(0 , setCenterBox , 0);
            Collider.height = setSizeYBox;
            Invoke("resetBox" , 1.0f);
            IsSlide = true;
        }

        animator.SetBool("IsJump" , Input.GetKeyDown(KeyCode.Space) && IsGround);
        animator.SetBool("IsGround" , IsGround);
        animator.SetBool("IsSlide" , IsSlide);

        stateLook = Mathf.Lerp(stateLook , Islook ? 1.0f : 0.0f , 10.0f * Time.deltaTime);
        animator.SetFloat("look" , stateLook);


        //RaycastHit hit;

        //IsGround = Physics.Raycast(transform.position + new Vector3(0, 0.5f , 0), Vector3.down , out hit , rayToGround, layer);

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


    private void OnCollisionStay(Collision collision)
    {
        IsGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGround = false;
    }

    void resetBox()
    {
        Collider.center = new Vector3(0, initCenterBox, 0);
        Collider.height = initSizeYBox;
        IsSlide = false;
    }
}
