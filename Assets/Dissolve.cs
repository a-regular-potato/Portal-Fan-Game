using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material Mat1;
    public Material Mat2;
    public Material Mat3;
    public Material Mat4;
    public Material Mat5;
    private float StartDissolveValue = -1f;
    public float DissolveValue;

    private bool StartDissolve = false;

    public Interactable interactable;
    public GameObject Holograms;
    public AudioManager aManager;
    public DropCube DC;
    // Start is called before the first frame update
    void Start()
    {
        DC = FindObjectOfType<DropCube>();
        aManager = FindObjectOfType<AudioManager>();
        Holograms.SetActive(true);
        DissolveValue = Mat1.GetFloat("Vector1_6AE1D498");
        Mat1.SetFloat("Vector1_6AE1D498", -1f);
        Mat2.SetFloat("Vector1_6AE1D498", -1f);
        Mat3.SetFloat("Vector1_6AE1D498", -1f);
        Mat4.SetFloat("Vector1_6AE1D498", -1f);
        Mat5.SetFloat("Vector1_6AE1D498", -1f);
    }

    // Update is called once per frame
    void Update()
    {
        DissolveValue = Mat1.GetFloat("Vector1_6AE1D498");
        if (StartDissolve && DissolveValue < 1f)
        {
            interactable.CubeDrop();
            Holograms.SetActive(false);
            Mat1.SetFloat("Vector1_6AE1D498", StartDissolveValue += 0.001f);
            Mat2.SetFloat("Vector1_6AE1D498", StartDissolveValue += 0.001f);
            Mat3.SetFloat("Vector1_6AE1D498", StartDissolveValue += 0.001f);
            Mat4.SetFloat("Vector1_6AE1D498", StartDissolveValue += 0.001f);
            Mat5.SetFloat("Vector1_6AE1D498", StartDissolveValue += 0.001f);
        }
        if (DissolveValue > 1f)
        {
            Mat1.SetFloat("Vector1_6AE1D498", 1f);
            Mat2.SetFloat("Vector1_6AE1D498", 1f);
            Mat3.SetFloat("Vector1_6AE1D498", 1f);
            Mat4.SetFloat("Vector1_6AE1D498", 1f);
            Mat5.SetFloat("Vector1_6AE1D498", 1f);
            StartDissolve = false;
            DC.CubeDissolved = true;
            DC.CubeSpawned = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EG"))
        {
            StartDissolve = true;
            aManager.Play("MaterialEmancipation");
        }
    }
}
