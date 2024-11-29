using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone_EMP : MonoBehaviour
{
    public GameObject Audio_Dvig;
    void Start()
    {
        Audio_Dvig.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy_Linkor" || other.tag == "Enemy_Fighter")
        {
            Debug.Log("Trust_offf");
            Audio_Dvig.SetActive(true);
            try
            {
                other.gameObject.GetComponent<Rigidbody>().angularDrag = 0.1f;
                other.gameObject.GetComponent<Rigidbody>().drag = 0.1f;
                other.gameObject.GetComponent<VSX.UniversalVehicleCombat.VehicleEngines3D>().enabled = false;
            }
            catch{}
        }
        
        
    }
}
