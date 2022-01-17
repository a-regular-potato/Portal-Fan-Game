using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Transform playerTransform;
    //public Transform TPObj;

    public float speed = 5f;
    public float gravity = -40f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [HideInInspector]
    public Vector3 velocity;
    bool isGrounded;

    public Camera cam;
    Interact Interact;

    public bool TriggerEntered;

    public Animator animatorPG;

    public AudioManager aManager;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if (isGrounded)
        {
            if (velocity.z > 1f)
            {
                velocity.z -= 2f;
            }
            else if(velocity.z < -1f)
            {
                velocity.z += 2f;
            }
            else
            {
                velocity.z = 0f;
            }

            if (velocity.x > 1f)
            {
                velocity.x -= 2f;
            }
            else if (velocity.x < -1f)
            {
                velocity.x += 2f;
            }
            else
            {
                velocity.x = 0f;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            cam.transform.position = new Vector3(0f, 0.8f, 0f);
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            cam.transform.position = new Vector3(0f, 1.2f, 0f);
        }*/

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    // if (other.CompareTag("PropulsionGel"))
    //{
    // speed = 30f;
    // }

    //  else if (other.CompareTag("RepulsionGel"))
    // {
    //jumpHeight = 10f;
    //     speed = 7f;
    //}
    // else if (other.CompareTag("AdehsionGel"))
    // {
    // playerTransform = GameObject.Find("player").transform;
    // playerTransform.position = TPObj.transform.position;
    // //playerTransform.rotation = TPObj.rotation;
    //   }
    //  }

    private void OnTriggerEnter(Collider other)
    {
        Interact = GetComponent<Interact>();
        if (other.gameObject.CompareTag("RCube2") || other.gameObject.CompareTag("RDCube2") || other.gameObject.CompareTag("PBTN2"))
        {
            TriggerEntered = true;
        }
        if (other.gameObject.CompareTag("EG")) 
        {
            animatorPG.SetBool("PortalFizzle", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interact = GetComponent<Interact>();
        if (other.gameObject.CompareTag("RCube2") || other.gameObject.CompareTag("RDCube2") || other.gameObject.CompareTag("PBTN2"))
        {
            TriggerEntered = false;
        }
        if (other.gameObject.CompareTag("EG"))
        {
            animatorPG.SetBool("PortalFizzle", false);
        }
    }
}
