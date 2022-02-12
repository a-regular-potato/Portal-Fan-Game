using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFP : MonoBehaviour
{
    Animator animator;

    public AudioManager aMangager;

    public playerMovement player;
    public Interactable cube;

    public float MovementForward;
    public float MovementUp;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Launched?", true);
            aMangager.Play("AFPLaunch");
            player.velocity = this.transform.up * -MovementForward;
            player.velocity.y = MovementUp;
        }
        if (other.CompareTag("RCube"))
        {
            cube = other.gameObject.GetComponent<Interactable>();
            if (cube.holdingItem == false)
            {
                animator.SetBool("Launched?", true);
                aMangager.Play("AFPLaunch");
                cube.Cube = true;
            }
            cube.velocity = this.transform.up * -MovementForward;
            cube.velocity.y = MovementUp;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (cube !=null)
        {
            cube.Cube = false;
        }
        animator.SetBool("Launched?", false);
    }
}
