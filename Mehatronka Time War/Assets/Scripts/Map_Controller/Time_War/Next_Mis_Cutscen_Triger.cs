using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Mis_Cutscen_Triger : MonoBehaviour
{
    [SerializeField] private JSon_Save_Controller JSON_Controller;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            JSON_Controller.SpaceFighter = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            JSON_Controller.SpaceFighter = true; 
        }
    }
    
    
}
