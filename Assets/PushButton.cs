using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    Animator animator;

    public AudioManager aManager;

    public Material btnGlow;
    // Start is called before the first frame update
    void Start()
    {
        btnGlow.SetFloat("IsActivated", 0f);
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RCube") || other.CompareTag("Player")) 
        {
            animator.SetBool("Pressed?", true);
            aManager.Play("FBTNDown");
            btnGlow.SetFloat("IsActivated", 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Pressed?", false);
        aManager.Play("FBTNUp");
        btnGlow.SetFloat("IsActivated", 0f);
    }
}
