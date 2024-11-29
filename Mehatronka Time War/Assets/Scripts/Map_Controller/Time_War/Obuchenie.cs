using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obuchenie : MonoBehaviour
{
    [SerializeField] private AudioSource[] _Obuchenie;
    [SerializeField] private GameObject PlayrShip;
    [SerializeField] private GameObject Enemy_Ship;
    [SerializeField] private GameObject Enemy;
    protected InputController input;
    [SerializeField]private int _nomer;
    private float _Time;
    protected virtual void OnEnable()
    {
        input.Enable();
    }


    protected virtual void OnDisable()
    {
        input.Disable();
    }

    protected void Awake()
    {
        Enemy.SetActive(false);
        _nomer = 0;
        input = new InputController();
    }
    void Start()
    {
        
        
        _Obuchenie[0].Play();
        _nomer++;
    }


    void Update()
    {
        if (_nomer == 1 && (input.Buttons.Return.ReadValue<float>() > 0 || input.Buttons.Options.ReadValue<float>() > 0 ||
                            input.Buttons.South.ReadValue<float>() > 0))
        {
            _nomer++;
            AllStop();
            _Obuchenie[1].Play();
        }

        if (_nomer==3 && (PlayrShip.transform.eulerAngles.y > 80 && PlayrShip.transform.eulerAngles.y < 100))
        {
            _nomer++;
            AllStop();
            _Obuchenie[3].Play();
        }
        if(_nomer == 4 && (input.Pedals.Brake.ReadValue<float>()>0))
        {
            _nomer++;
            AllStop();
            _Obuchenie[4].Play();
        }
        if(_nomer == 5 && (input.Handbrake.Handbrake.ReadValue<float>()>0))
        {
            _nomer++;
            AllStop();
            _Obuchenie[5].Play();
            Enemy.SetActive(true);
        }

        if (_nomer == 8 && Enemy_Ship.activeInHierarchy == false)
        {
            _nomer++;
            AllStop();
            _Obuchenie[8].Play();
        }

        _Time += Time.deltaTime;
        if (_Time > 1000)
        {
            AllStop();
            _Obuchenie[9].Play();
        }
        
    }

    public void Next_Return()
    {
        if (_nomer == 2)
        {
            _nomer++;
            AllStop();
            _Obuchenie[2].Play();
        }
        
    }

    private void AllStop()
    {
        for (int i = 0; i < _Obuchenie.Length; i++)
        {
            _Obuchenie[i].Stop();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_nomer == 6 && other.tag =="Player")
        {
            _nomer++;
            AllStop();
            _Obuchenie[6].Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_nomer == 7 && other.tag =="Player")
        {
            _nomer++;
            AllStop();
            _Obuchenie[7].Play();
        }
    }
    
    public void Enter_Train()
    {
        SceneManager.LoadScene("Dev_Fox_Space");
    }
    public void Enter_Story()
    {
        SceneManager.LoadScene("Start_CutScene_CockpitScene");
    }
}
