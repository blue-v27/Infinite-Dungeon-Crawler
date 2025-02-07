using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{

    public GameObject meleWeapon;

    public static bool isMeleWeapon = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Cursour = Input.mousePosition;

        if (isMeleWeapon)
            Instantiate(meleWeapon, Cursour, Quaternion.identity); //negrii
    }
} 
