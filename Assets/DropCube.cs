using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCube : MonoBehaviour
{
    public bool AutoDropCube;
    public bool AutoDropFirstCube;
    public GameObject TypeOfBoxToDrop;
    private GameObject Cube;

    [HideInInspector]
    public bool CubeDissolved = false;
    [HideInInspector]
    public bool CubeSpawned;

    public AudioManager aManager;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();

        if (AutoDropFirstCube)
        {
            Cube = Instantiate(TypeOfBoxToDrop, this.transform.position, this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AutoDropCube)
        {
            if (CubeDissolved && !CubeSpawned)
            {
                Destroy(Cube);
                Cube = Instantiate(TypeOfBoxToDrop, this.transform.position, this.transform.rotation);
                CubeSpawned = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RCube"))
        {
            animator.SetBool("DispenseCube", true);
            aManager.Play("DropperOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("DispenseCube", false);
        aManager.Play("DropperClose");
    }
}
