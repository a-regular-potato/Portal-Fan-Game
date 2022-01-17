using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    //public Camera LineOfSight;

    public float damage;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        animator = GetComponent<Animator>();
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 1f), transform.forward, out hit))
        {
            // Raycast has hit something...

            // Does the thing we've hit, have a "Target"?
            Target target = hit.transform.GetComponent<Target>();
            bool hasTarget = target != null;

            // Yes, start firing...
            animator.SetBool("SeenPlayer", hasTarget);

            if (hasTarget) 
            {
                //transform.rotation = Quaternion.Look(target.transform.position, Vector3.zero);
                Vector3 diff = target.transform.position - transform.position;
                diff.Normalize();
                Quaternion rot = Quaternion.LookRotation(diff, Vector3.up);
                //rot.y -= 0;
                //transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
                transform.localRotation = rot;


                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    animator.SetBool("PlayerVisable", hasTarget);
                    FindObjectOfType<AudioManager>().Play("TurretShooting");
                }

            }


            //Physics.BoxCast(LineOfSight.transform.position, new Vector3(.2f, .2f, 1f), LineOfSight.transform.forward, out hit);
            //Physics.Raycast(LineOfSight.transform.position, LineOfSight.transform.forward, out hit
        }
        else
        {
            animator.SetBool("SeenPlayer", false);
            animator.SetBool("PlayerVisable", false);
        }
    }
}
