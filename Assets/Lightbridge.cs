using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbridge : MonoBehaviour
{
    public GameObject LBE;
    public GameObject hitItem;
    public Material lightBridge;

    public float Distance;
    private float Tiling;
    //private new Renderer renderer;

    public bool Activated;

    // Start is called before the first frame update
    void Start()
    {
        /*renderer = this.gameObject.GetComponent<Renderer>();*/
        BridgeBuild();
    }

    void BridgeBuild()
    {
        RaycastHit hit;
        if (Physics.Raycast(LBE.transform.position, LBE.transform.forward, out hit))
        {
            hitItem = hit.collider.gameObject;
            Distance = Vector3.Distance(LBE.transform.position, hitItem.transform.position);
            this.transform.localScale = new Vector3(2f, 0.1f, Distance  - 0.5f);
            Tiling = Distance / 2;
            Tiling = Mathf.Round(Tiling);

            lightBridge.SetVector("Vector2_D0DA0095", new Vector4(Tiling, 1f));
            //renderer.material.mainTextureScale = new Vector2(Tiling, 1f);
        }
    }

    void BridgeBuildThroughPortal()
    {
        /*RaycastHit hit;
        if (Physics.Raycast(LBE.transform.position, LBE.transform.forward, out hit))
        {
            hitItem = hit.collider.gameObject;
            Distance = Vector3.Distance(LBE.transform.position, hitItem.transform.position);
            this.transform.localScale = new Vector3(1.2f, 0.1f, Distance);
        }*/
    }
}
