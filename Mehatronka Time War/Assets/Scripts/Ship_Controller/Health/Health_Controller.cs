using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine.UI;

public class Health_Controller : MonoBehaviour
{
    public float maxHealth = 100000;
    public float currentHealth;
    public bool in_menu;
    
    [SerializeField] private float ConstantmaxHealth;

    [SerializeField] private VSX.UniversalVehicleCombat.Damageable _SC_Health;
    [SerializeField] private Menu_Controller _SC_Menu;
    [SerializeField] private Image Health_Barr;
    [SerializeField] private TextMeshProUGUI Dead_Text;
    [SerializeField] private GameObject Dead_Explosive_FX;
    [SerializeField] private GameObject[] My_Ship_All;
    [SerializeField] private GameObject Color_Panel;
    [SerializeField] private GameObject[] Warning_Alarm;
    [SerializeField] private JSon_Save_Controller JSON_Controller;
    

    private float Last_Max_Health;
    private float Explosive_Timer;
    private bool one_r;
    
    protected Input_Map_SpaceFighter input;
    
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
        input = new Input_Map_SpaceFighter();
        maxHealth = 40000;
        Dead_Explosive_FX.SetActive(false);
        ConstantmaxHealth = maxHealth;
        Explosive_Timer = 0;
    }

    void Start()
    {
        one_r = false;
        currentHealth = maxHealth;
        foreach (GameObject Obj in Warning_Alarm)
        {
            Obj.SetActive(false);
        }
        
    }

    void Update()
    {
        if (input._SpaceFighter_Controls_XR.Secret_Slesh.ReadValue<float>() > 0)
        {
            Admin_Helth();
        }
        
        if (in_menu == false)
        {
            currentHealth = maxHealth - (_SC_Health.get_max_health() - _SC_Health.get_Damage_Helf());
        }
        
        if (currentHealth <= 0)
        {
            SpaceFighter_Dead();
            if (Explosive_Timer == 0)
            {
                JSON_Controller.Pokaz_Time();
            }
            
            Explosive_Timer += Time.deltaTime;
            if (Explosive_Timer > 3f)
            {
                foreach (GameObject Obj in My_Ship_All)
                {
                    try
                    {
                        Obj.SetActive(false);
                    }
                    catch{
                    }
                    
                }

            }
            if (Explosive_Timer > 3.6f)
            {
                Dead_Explosive_FX.SetActive(false);
                _SC_Menu.Dead_Game();
            }
            if (Explosive_Timer > 3.2f && Explosive_Timer < 3.5f )
            {
                _SC_Menu.Music_Controller(3);
            }
            
        }

        if (in_menu == true && one_r == false)
        {
            maxHealth = currentHealth;
            one_r = true;
        }
        else
        {
            one_r = false;
        }
        

        if (in_menu == false && currentHealth > 0 && currentHealth <= maxHealth * 0.1)
        {
            foreach (GameObject Obj in Warning_Alarm)
            {
                Obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject Obj in Warning_Alarm)
            {
                Obj.SetActive(false);
            }
        }
    }

    public void Hilka(float Health_Plus)
    {
        maxHealth += Health_Plus;
    }
    public void Damage(float Damage_Minus)
    {
        maxHealth += Damage_Minus;
    }

    private void FixedUpdate()
    {
        Health_Barr.fillAmount = (currentHealth / ConstantmaxHealth);
    }

    private void SpaceFighter_Dead()
    {
        foreach (GameObject Obj in Warning_Alarm)
        {
            Obj.SetActive(false);
        }
        Dead_Explosive_FX.SetActive(true);
        Color_Panel.SetActive(false);
        Dead_Text.text = "SpaceFighter Destroyed...";
        Dead_Text.color = Color.red;
    }

    private void Admin_Helth()
    {
        if (input._SpaceFighter_Controls_XR.Secret_God_Mod.ReadValue<float>() > 0)
        {
            maxHealth = 0;
            currentHealth = 0;
        }
        if (input._SpaceFighter_Controls_XR.Secret_Minus_Hell.ReadValue<float>() > 0)
        {
            maxHealth -=100;
            currentHealth -= 100; 
        }
        if (input._SpaceFighter_Controls_XR.Secret_Plus_Hell.ReadValue<float>() > 0)
        {
            maxHealth +=100;
            currentHealth += 100; 
        }
        //Start helth
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_1.ReadValue<float>() > 0)
        {
            maxHealth = 10000; 
            ConstantmaxHealth = 10000;
            Health_Barr.color = new Color(0.2f, 0.05749005f, 0.6f);
        }
        
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_2.ReadValue<float>() > 0)
        {
            maxHealth = 20000;
            ConstantmaxHealth = 20000; 
            Health_Barr.color = new Color(0f, 0, 1);
        }
        
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_3.ReadValue<float>() > 0)
        {
            maxHealth = 30000; 
            ConstantmaxHealth = 30000; 
            Health_Barr.color = new Color(0.112f, 0.7039999f, 1);
        }
        
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_4.ReadValue<float>() > 0)
        {
            maxHealth = 40000;
            ConstantmaxHealth = 40000; 
            Health_Barr.color = new Color(0f, 0.9456642f, 1);
        }
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_5.ReadValue<float>() > 0)
        {
            maxHealth = 50000;
            ConstantmaxHealth = 50000; 
            Health_Barr.color = new Color(0.1137255f, 1f, 0.7088208f);
        }
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_6.ReadValue<float>() > 0)
        {
            maxHealth = 70000;
            ConstantmaxHealth = 70000; 
            Health_Barr.color = new Color(0.8792453f, 0.8018634f, 0.1111499f);
        }
        if (input._SpaceFighter_Controls_XR.Secret_Dificult_7.ReadValue<float>() > 0)
        {
            maxHealth = 100000;
            ConstantmaxHealth = 100000;
            Health_Barr.color = new Color(1f, 1f, 1f);
            
        }
    }
    
}
