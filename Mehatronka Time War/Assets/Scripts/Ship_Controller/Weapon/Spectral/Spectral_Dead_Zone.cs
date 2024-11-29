using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectral_Dead_Zone : MonoBehaviour
{
    [SerializeField] private GameObject Audio_dead_enemy;
    [SerializeField] private Health_Controller _SC_Dead;

    private void Start()
    {
        Audio_dead_enemy.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Asteroids" || other.tag == "Enemy_Linkor" || other.tag == "Enemy_Fighter")
        {
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Enemy_Linkor")
        {
            Audio_dead_enemy.SetActive(true);
        }

        if (other.tag == "Player")
        {
            _SC_Dead.maxHealth -= 1000;
        }


            
    }
}
