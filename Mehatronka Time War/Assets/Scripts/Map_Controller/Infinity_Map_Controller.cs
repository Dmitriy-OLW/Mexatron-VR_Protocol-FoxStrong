using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using UnityEngine.UIElements;

public class Infinity_Map_Controller : MonoBehaviour
{
    public bool infinity_wave_mode = false;
    public bool in_Menu = false;

    [SerializeField] private GameObject[] Memory_Spher_Variant;
    [SerializeField] private GameObject Parent_Spher;

    [SerializeField] private  GameObject Linkor_Referense;
    [SerializeField] private  GameObject Fighter_Referense;

    [SerializeField] private  GameObject[] Spavner_Point;
    [SerializeField] private GameObject Center_map;
    [SerializeField] private GameObject Parent_on_Map;

    private GameObject[] Linkor_On_Scene;
    private GameObject[] Fighter_On_Scene;
    private GameObject[] Memory_On_Scene;

    private float Timer_Wave;
    private float Timer_Memory;
    private bool First_Spavn_Spher;

    private void Start()
    {
        Timer_Wave = 0;
        Timer_Memory = 0;
        First_Spavn_Spher = true;
    }

    void FixedUpdate()
    {
        if (First_Spavn_Spher)
        {
            for (int i = 0; i < 18; i++)
            {
                Spavn_Memory();
            }

            First_Spavn_Spher = false;
        }
        if (infinity_wave_mode)
        {
            Timer_Wave += Time.deltaTime;
            Timer_Memory += Time.deltaTime;
            if (in_Menu == false && Timer_Wave > 10f)
            {
                Memory_On_Scene = GameObject.FindGameObjectsWithTag("Memory_Spher");
                Linkor_On_Scene = GameObject.FindGameObjectsWithTag("Enemy_Linkor");
                if (Linkor_On_Scene.Length < 3)
                {
                    Spavner(Linkor_Referense);
                }
                Fighter_On_Scene = GameObject.FindGameObjectsWithTag("Enemy_Fighter");
                if (Fighter_On_Scene.Length <3) //&& Linkor_On_Scene.Length < 3
                {
                    Spavner(Fighter_Referense);
                }
                Timer_Wave = 0;
            }

            if (in_Menu == false && Timer_Wave > 60f && Memory_On_Scene.Length < 25)
            {
                Spavn_Memory();
                Timer_Memory = 0;
            }
        }
    }

    private void Spavner(GameObject Ship)
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(0, Spavner_Point.Length);
        Transform _point_transform = Spavner_Point[randomNumber].GetComponent<Transform>();
        GameObject newObj = Instantiate(Ship, new Vector3(_point_transform.position.x, _point_transform.position.y, _point_transform.position.z), Quaternion.Euler(0, 0, 0));
        Vector3 newDir = Vector3.RotateTowards(newObj.transform.forward, (Center_map.transform.position-newObj.transform.position), 10f, 0.0F);
        newObj.transform.rotation = Quaternion.LookRotation(newDir);
        newObj.transform.SetParent(Parent_on_Map.transform, true);
        newObj.SetActive(true);
    }

    private void Spavn_Memory()
    {
        Random rnd = new Random();
        GameObject newObj = Instantiate(Memory_Spher_Variant[rnd.Next(0, Memory_Spher_Variant.Length)], new Vector3(rnd.Next(-1300, 1300), rnd.Next(50, 650) , rnd.Next(-1300, 1300)), Quaternion.Euler(0, 0, 0));
        newObj.transform.SetParent(Parent_Spher.transform, true);
        newObj.SetActive(true);
        /*try
        {
            int sostoynie = rnd.Next(0, 4);
            Rigidbody R_rb = newObj.gameObject.GetComponent<Rigidbody>();
            if(sostoynie == 1)
            {
                R_rb.useGravity = true;
            }

        }
        catch
        {
            
        }*/
    }
}
