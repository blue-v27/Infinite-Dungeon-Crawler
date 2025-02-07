using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeeFffect : MonoBehaviour
{
    float time = .3f;
    void Start()
    {
        Invoke("DestroyOgject", time);
        
    }

    void DestroyOgject()
    {
        Destroy(gameObject);
    }
}
