using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Material btnGlow;

    public float delay = 0.5f;

    private AudioManager aManager;

    private bool BTNDown = false;

    public Camera PlayerCam;
    public float DistanceFromPlayer = 1f;
    public float SlerpingFactor = 1f;

    [HideInInspector]
    public bool holdingItem = false;

    private Vector3 currentVelocity;
    private Vector3 lastPosition;

    [HideInInspector]
    public bool Cube;

    public Vector3 velocity;

    Interact interact;

    Rigidbody rb;

    Animator animator;

    private Animator animatorPG;
    private GameObject PG;
    // Start is called before the first frame update
    void Start()
    {
        btnGlow.SetFloat("IsActivated", 0f);
        PlayerCam = FindObjectOfType<Camera>();
        aManager = FindObjectOfType<AudioManager>();
        animator = GetComponentInParent<Animator>();
        interact = FindObjectOfType<Interact>();
        rb = GetComponent<Rigidbody>();

        if (rb != null) { 
            lastPosition = rb.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Cube == true)
        {
            rb.velocity = velocity;
        }
        if (holdingItem == true && interact.RDCubePickedUp == false)
        {
            Cube = false;
            rb.useGravity = false;

            Vector3 ObjPosition = PlayerCam.transform.position + PlayerCam.transform.forward * DistanceFromPlayer;

            /*rb.position = Vector3.Lerp(rb.position, ObjPosition, Time.deltaTime * SlerpingFactor);*/
            rb.MovePosition(Vector3.Lerp(rb.position, ObjPosition, Time.deltaTime * SlerpingFactor));

            currentVelocity = (rb.position - lastPosition) / Time.deltaTime;
            lastPosition = rb.position;

            Quaternion newRotation = new Quaternion(0, PlayerCam.transform.rotation.y, 180, PlayerCam.transform.rotation.w);
            newRotation.Normalize();

            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.deltaTime * SlerpingFactor);
        }
        else if (holdingItem == true && interact.RDCubePickedUp == true) 
        {
            Cube = false;
            rb.useGravity = false;

            Vector3 ObjPosition = PlayerCam.transform.position + PlayerCam.transform.forward * DistanceFromPlayer;
            rb.MovePosition(Vector3.Lerp(rb.position, ObjPosition, Time.deltaTime * SlerpingFactor));

            currentVelocity = (rb.position - lastPosition) / Time.deltaTime;
            lastPosition = rb.position;

            Quaternion newRotation = new Quaternion(0, PlayerCam.transform.rotation.y, 0, PlayerCam.transform.rotation.w);
            newRotation.Normalize();

            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.deltaTime * SlerpingFactor);
        }

        /*if (Input.GetKeyDown(KeyCode.E) && interact.ObjectPickedUp != null) 
        {
            CubeDrop();
        }*/
    }

    public void ButtonPressed() 
    {
        BTNDown = true;
        if (BTNDown)
        {
            aManager.Play("PBTNClock");
        }
        animator.SetBool("Pressed?", true);
        btnGlow.SetFloat("IsActivated", 1f);
        aManager.Play("PedestalButtonDown");

        StartCoroutine(ButtonDelay());
    }

    IEnumerator ButtonDelay() 
    {
        yield return new WaitForSeconds (delay);
        aManager.Play("PedestalButtonUp");
        animator.SetBool("Pressed?", false);
        btnGlow.SetFloat("IsActivated", 0f);
        BTNDown = false;
        aManager.StopSounds("PBTNClock");
    }

    public void CubePickUp() 
    {
        holdingItem = true;
    }

    public void CubeDrop() 
    {
        holdingItem = false;
        rb.useGravity = true;
        rb.velocity = currentVelocity;
        PG = GameObject.FindGameObjectWithTag("PortalGun");
        animatorPG = PG.GetComponent<Animator>();
        animatorPG.SetBool("PickedUp", false);
        aManager.StopSounds("PickedUpItemLoop");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {

        }
        else
        {
            aManager.Play("CubeDrop");
        }
    }
}
