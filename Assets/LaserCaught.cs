using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCaught : MonoBehaviour
{
    Animator animator;

    private bool Activated = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Activated)
        {
            animator.SetBool("LaserCaught", true);
        }
        else if (!Activated)
        {
            animator.SetBool("LaserCaught", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            Activated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            Activated = false;
        }
    }
}
