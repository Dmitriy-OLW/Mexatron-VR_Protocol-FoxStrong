using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy_Enemy_Controller : MonoBehaviour
{
    

    public GameObject ship;
    [SerializeField] private JSon_Save_Controller _SC_Plus_Destroy;
    
    private bool plus_one = true;

    private void Start()
    {
        plus_one = true;
    }

    void Update()
    {
        if (plus_one && ship.gameObject.activeInHierarchy == false)
        {
            plus_one = false;
            _SC_Plus_Destroy.destroy += 1;
        }
    }
}
