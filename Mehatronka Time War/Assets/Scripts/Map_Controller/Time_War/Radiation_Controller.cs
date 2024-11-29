using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation_Controller : MonoBehaviour
{
    [SerializeField] private GameObject _Radioktiv_Warning;
    [SerializeField] private GameObject _Radioktiv_Audio_Warning;

    private void Awake()
    {
        _Radioktiv_Warning.SetActive(false);
        _Radioktiv_Audio_Warning.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _Radioktiv_Warning.SetActive(false);
            _Radioktiv_Audio_Warning.SetActive(false);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _Radioktiv_Warning.SetActive(false);
            _Radioktiv_Audio_Warning.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _Radioktiv_Warning.SetActive(true);
            _Radioktiv_Audio_Warning.SetActive(true);
        }
    }
}
