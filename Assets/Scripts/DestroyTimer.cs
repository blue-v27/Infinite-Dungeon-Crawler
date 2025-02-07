using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float life, size;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", life);
    }

    private void FixedUpdate()
    {
        transform.localScale *= size;
    }

    void Death() 
    {
        Destroy(gameObject);
    }
}
