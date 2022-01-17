using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Camera cam;

    private bool CubePickedUp = false;

    private Interactable ObjectPickedUp;

    public AudioManager aManager;

    public Animator animatorPG;

    public playerMovement PlayerMovement;

    public Material ItemWingsMaterial;

    //[HideInInspector]
    public bool RDCubePickedUp = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) 
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            bool foundObj = interactable != null;

            if (foundObj && PlayerMovement.TriggerEntered)
            {
                animatorPG.SetBool("PickUp", true);
                aManager.Play("PGunItemWingsOpen");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ObjectPickedUp != null)
                    {
                        ObjectPickedUp.CubeDrop();
                        ObjectPickedUp = null;
                        ItemWingsMaterial.SetFloat("Intensity", 12f);
                        return;
                    }
                    if (hit.collider.CompareTag("RCube"))
                    {
                        if (PlayerMovement.TriggerEntered)
                        {
                            RDCubePickedUp = false;
                            CubePickedUp = true;
                            interactable.CubePickUp();
                            ObjectPickedUp = interactable;
                            aManager.Play("CubePickup");

                            animatorPG.SetBool("PickedUp", true);
                            aManager.Play("PickedUpItemLoop");
                            ItemWingsMaterial.SetFloat("Intensity", 50f);
                        }
                    }
                    else if (hit.collider.CompareTag("RDCube"))
                    {
                        if (PlayerMovement.TriggerEntered)
                        {
                            RDCubePickedUp = true;
                            CubePickedUp = true;
                            interactable.CubePickUp();
                            ObjectPickedUp = interactable;
                            aManager.Play("CubePickup");

                            animatorPG.SetBool("PickedUp", true);
                            aManager.Play("PickedUpItemLoop");
                            ItemWingsMaterial.SetFloat("Intensity", 50f);
                        }
                    }
                    else if (hit.collider.CompareTag("PBTN"))
                    {
                        if (PlayerMovement.TriggerEntered)
                        {
                            interactable.ButtonPressed();
                            RDCubePickedUp = false;
                        }
                    }
                    else
                    {
                        RDCubePickedUp = false;
                    }
                }
            }
            else 
            {
                animatorPG.SetBool("PickUp", false);
                //aManager.Play("PGunItemWingsClose");
            }
        }

        if (Input.GetMouseButtonDown(1) && ObjectPickedUp == null) 
        {
            animatorPG.SetBool("PortalShot", true);
            aManager.Play("PortalShot1");
            StartCoroutine(ShotDelay());
        }
        if (Input.GetMouseButtonDown(0) && ObjectPickedUp == null) 
        {
            animatorPG.SetBool("PortalShot", true);
            aManager.Play("PortalShot2");
            StartCoroutine(ShotDelay());
        }
        
        IEnumerator ShotDelay()
        {
            yield return new WaitForSeconds(0.4f);
            animatorPG.SetBool("PortalShot", false);
        }




        //Is "E" Pressed
        /*if (Input.GetKeyDown(KeyCode.E)) 
        {
            
            if (ObjectPickedUp != null)
            {
                ObjectPickedUp.CubeDrop();
                ObjectPickedUp = null;
                return;
            }

            //What am I looking at?
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            *//*if (Physics.Raycast(transform.position, transform.forward, out hit))*//*
            if (Physics.Raycast(ray, out hit))
            {
                //Is the object interactable
                Interactable interactable = hit.transform.GetComponent<Interactable>();
                bool foundObj = interactable != null;
                
                
                if (foundObj) 
                {
                    if (hit.collider.CompareTag("RCube"))
                    {
                        if (PlayerMovement.TriggerEntered)
                        {
                            RDCubePickedUp = false;
                            CubePickedUp = true;
                            interactable.CubePickUp();
                            ObjectPickedUp = interactable;
                            aManager.Play("CubePickup");

                            animatorPG.SetBool("PickedUp", true);
                            aManager.Play("PickedUpItemLoop");
                        }
                    }
                    else if (hit.collider.CompareTag("RDCube"))
                    {
                        if (PlayerMovement.TriggerEntered)
                        {
                            RDCubePickedUp = true;
                            CubePickedUp = true;
                            interactable.CubePickUp();
                            ObjectPickedUp = interactable;
                            aManager.Play("CubePickup");

                            animatorPG.SetBool("PickedUp", true);
                            aManager.Play("PickedUpItemLoop");
                        }
                    }
                    else if (hit.collider.CompareTag("PBTN"))
                    {
                        interactable.ButtonPressed();
                        RDCubePickedUp = false;
                    }
                    else 
                    {
                        RDCubePickedUp = false;
                    }
                }
            }
        }*/
    }
}
