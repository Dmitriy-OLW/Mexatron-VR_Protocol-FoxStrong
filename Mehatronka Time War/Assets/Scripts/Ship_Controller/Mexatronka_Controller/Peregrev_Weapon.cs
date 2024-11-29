using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peregrev_Weapon : MonoBehaviour
{
    public bool proverka = false;
    protected InputController input;
    [SerializeField] private GameObject _WeaponController;
    [SerializeField] private GameObject _Audio_Peregrev;
    [SerializeField] private Image _Speed_Barr;
    private bool Peregrev;
    private float _Time;

    protected void Awake()
    {
        input = new InputController();
        _Audio_Peregrev.SetActive(false);
        _Time = 0;
        Peregrev = false;
    }
    
    protected virtual void OnEnable()
    {
        input.Enable();
    }


    protected virtual void OnDisable()
    {
        input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Peregrev == true)
        {
            _Time -= Time.deltaTime * 5;
        }
        else if (_Time>=30)
        {
            _WeaponController.SetActive(false);
            _Audio_Peregrev.SetActive(true);
            Peregrev = true;
        }
        else if (Peregrev == false && ( input.Buttons.RightShift.ReadValue<float>() > 0 || input.Buttons.LeftShift.ReadValue<float>() > 0))
        {
            _Time += Time.deltaTime;
        }
        else if (_Time > 0)
        {
            _Time -= Time.deltaTime;
        }
        if (Peregrev == true && _Time <= 0)
        {
            _WeaponController.SetActive(true);
            _Audio_Peregrev.SetActive(false);
            _Time = 0;
            Peregrev = false;
        }
        _Speed_Barr.fillAmount = _Time/30;
    }
}
