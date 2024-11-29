using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refocusirator_End_Snaryd : MonoBehaviour
{
    public bool OnEnemy;
    
    [SerializeField] private GameObject Spher_Del;
    [SerializeField] private GameObject Audio_vs;
    [SerializeField] private GameObject Polet_Audio;

    private Rigidbody _rb;
    private float Timer_lifTime;
    private float Timer_Scale;
    private float Scale;
    private float Scale_Del;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        OnEnemy = false;
        Timer_lifTime = 0;
        Timer_Scale = 0;
        Audio_vs.SetActive(false);
        Polet_Audio.SetActive(true);
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
            Scale = 15 * Timer_Scale;
            Spher_Del.transform.localScale = new Vector3(Scale, Scale, Scale);
            
            
        }

        if (Scale > 30)
        {
            OnEnemy = false;
            if (Timer_lifTime > 11)
            {
                
                Scale_Del = 15 * Math.Abs(Timer_lifTime - 13);
                Spher_Del.transform.localScale = new Vector3(Scale_Del, Scale_Del, Scale_Del);
                if (Timer_lifTime > 13)
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
            Polet_Audio.SetActive(false);
            Audio_vs.SetActive(true);
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