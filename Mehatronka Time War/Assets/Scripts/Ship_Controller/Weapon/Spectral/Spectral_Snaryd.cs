using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectral_Snaryd : MonoBehaviour
{
    [SerializeField] private GameObject Spher_Del;
    [SerializeField] private GameObject Popadanir_Audio;

    private Rigidbody _rb;
    private float Timer_lifTime;
    private float Timer_Scale;
    private float Scale;
    private float Scale_Del;
    [SerializeField]private bool OnEnemy;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        OnEnemy = false;
        Timer_lifTime = 0;
        Timer_Scale = 0;
        Popadanir_Audio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Timer_lifTime += Time.deltaTime;
        if (Timer_lifTime > 20)
        {
            Destroy(gameObject);
        }

        if (OnEnemy)
        {
            Timer_Scale += Time.deltaTime;
            Scale = 25 * Timer_Scale;
            Spher_Del.transform.localScale = new Vector3(Scale, Scale, Scale);
        }

        if (Scale > 50)
        {
            OnEnemy = false;
            if (Timer_lifTime > 6)
            {
                
                Scale_Del = 25 * Math.Abs(Timer_lifTime - 8);
                Spher_Del.transform.localScale = new Vector3(Scale_Del, Scale_Del, Scale_Del);
                if (Timer_lifTime > 8)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroids" || other.tag == "Hull")
        {
            OnEnemy = true;
            Timer_lifTime = 0;
            Popadanir_Audio.SetActive(true);
            try
            {
                _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                _rb.isKinematic = true;
            }
            catch
            {
                
            }
        }
    }
    
}


/* if(other.tag == "Asteroids")
            
            Enemy_Linkor
                Enemy_Fighter*/