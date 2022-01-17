using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject LazarEmitter;
    private GameObject HitObject;
    private float Distance;
    public ParticleSystem ps;

    public GameObject player;
    public playerMovement playerScript;
    private float MovementBack = 5f;

    private GameObject RDCube;
    private GameObject LaserRelay;

    public GameObject LazarTemplate;

    [HideInInspector]
    private GameObject Lazar;
    private GameObject Lazar1;

    private bool CreatedLaser = false;
    public bool CreatedLaser1 = false;
    // Start is called before the first frame update
    void Start()
    {
        LaserRelay = GameObject.Find("LaserRelay");
        RDCube = GameObject.Find("RedirectionCube");
        player = GameObject.Find("FirstPersonPlayer");
        playerScript = player.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(LazarEmitter.transform.position, LazarEmitter.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {

            }
            if (!CreatedLaser && !hit.collider.CompareTag("LaserRelay"))
            {
                HitObject = hit.collider.gameObject;
                Distance = Vector3.Distance(LazarEmitter.transform.position, HitObject.transform.position);
                this.transform.localScale = new Vector3(0.02f, 0.02f, Distance);
                ps.transform.position = new Vector3(0f, 0f, Distance);

                if (hit.collider.CompareTag("RDCube"))
                {
                    Lazar = Instantiate(LazarTemplate, RDCube.transform.position, RDCube.transform.rotation);
                    CreatedLaser = true;
                    /*if (Physics.Raycast(Lazar.transform.position, Lazar.transform.forward, out hit))
                    {
                        if (hit.collider.CompareTag("LaserRelay"))
                        {
                            Lazar.transform.localScale = new Vector3(0.02f, 0.02f, 4f);
                        }
                    }*/
                }
            }
            if (CreatedLaser)
            {
                if (!hit.collider.CompareTag("RDCube"))
                {
                    Destroy(Lazar);
                    Destroy(Lazar);
                    Destroy(Lazar);
                    CreatedLaser = false;
                }
            }
           /* if (!CreatedLaser1)
            {
                HitObject = hit.collider.gameObject;
                Distance = Vector3.Distance(LazarEmitter.transform.position, HitObject.transform.position);
                this.transform.localScale = new Vector3(0.02f, 0.02f, Distance);
                ps.transform.position = new Vector3(0f, 0f, Distance);
                if (hit.collider.CompareTag("LaserRelay"))
                {
                    Lazar1 = Instantiate(LazarTemplate, LaserRelay.transform.position, LaserRelay.transform.rotation);
                    Lazar1.transform.position = new Vector3(LaserRelay.transform.position.x, LaserRelay.transform.position.y + 1f, LaserRelay.transform.position.z);
                    Lazar1.transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
                    CreatedLaser1 = true;
                }
            }
            if (CreatedLaser1)
            {
                if (!hit.collider.CompareTag("LaserRelay"))
                {
                    Destroy(Lazar1);
                    Destroy(Lazar1);
                    Destroy(Lazar1);
                    CreatedLaser1 = false;
                }
            }*/
            else
            {
                HitObject = hit.collider.gameObject;
                Distance = Vector3.Distance(LazarEmitter.transform.position, HitObject.transform.position);
                this.transform.localScale = new Vector3(0.02f, 0.02f, Distance);
                ps.transform.position = new Vector3(0f, 0f, Distance);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           /* playerScript.velocity = this.transform.up * MovementBack;*/
        }
    }
}
