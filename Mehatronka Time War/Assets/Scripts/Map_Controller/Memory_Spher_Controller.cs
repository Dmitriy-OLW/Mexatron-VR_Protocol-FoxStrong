using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory_Spher_Controller : MonoBehaviour
{
    [SerializeField] private float Health_Plus = 3000;
    [SerializeField] private JSon_Save_Controller _SC_JSON;
    [SerializeField] private Health_Controller _SC_Health;

    private void Start()
    {
        Health_Plus = 3000;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _SC_JSON.orb += 1;
            _SC_Health.Hilka(Health_Plus);
            Destroy(gameObject);
        }
    }
}
