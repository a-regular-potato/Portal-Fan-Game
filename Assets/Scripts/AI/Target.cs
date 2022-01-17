using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float Health = 50f;
    public void TakeDamage(float amount) 
    {
        Health -= amount;
        if (Health <= 0f) 
        {
            Die();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die() 
    {
        Debug.Log("You ded!!");
    }
}
