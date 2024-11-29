using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Cut_Mis1_Next_Level : MonoBehaviour
{
    private float _Time;
    protected InputController input;
    
    protected virtual void OnEnable()
    {
        input.Enable();
    }


    protected virtual void OnDisable()
    {
        input.Disable();
    }

    private void Awake()
    {
        input = new InputController();
    }

    private void Start()
    {
        _Time = 0;
    }

    void Update()
    {
        _Time += Time.deltaTime;
        if (_Time > 83 || (input.Buttons.West.ReadValue<float>() > 0) || (input.Buttons.RightShift.ReadValue<float>() > 0 && input.Buttons.LeftShift.ReadValue<float>() > 0))
        {
            Enter_Infinity();
        }
    }
    public void Enter_Infinity()
    {
        SceneManager.LoadScene("Forerunner_Space");
    }
}
