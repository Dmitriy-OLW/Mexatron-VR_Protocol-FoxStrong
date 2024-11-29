using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using Object = System.Object;

public class Menu_Controller : MonoBehaviour
{
    //Записка UI: у togle, два положения и можно сделать включение и выключение вол, через сет актив объекта спавнера и включения активатора 
    // или же две кнопки на главном меню как с A. R. и всё . а инвертированное управление через лушание сет актива togela
    //Save_info
    public int Save_Ship_Color;

    protected Input_Map_SpaceFighter input;
    
    [SerializeField] private JSon_Save_Controller _SC_JSON;
    [SerializeField] private Health_Controller _SC_Health;
    [SerializeField] private Infinity_Map_Controller Infinity_Wave_Controller;

    [SerializeField] private GameObject Button_Resume;
    [SerializeField] private GameObject[] Ship_Color;
    [SerializeField] private GameObject[] Obj_in_Menu_UI;
    [SerializeField] private GameObject[] Obj_in_Game_M;
    
    [SerializeField] private GameObject Level;
    [SerializeField] private GameObject Player_Ship;
    [SerializeField] private GameObject Weapon_Controller;
    //[SerializeField] private GameObject Music_in_Menu;

    [SerializeField] private GameObject[] Musics;
    

   
    


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
        Change_Collor(0);
        Music_Controller(1);
        _SC_JSON.StartTime();
        foreach (GameObject Obj in Obj_in_Menu_UI)
        {
            try
            {
                Obj.SetActive(false);
            }
            catch{
            }
                    
        }
        _SC_Health.in_menu = false;

    }
    
    
    void Update()
    {

        if (input._SpaceFighter_Controls_XR.Exit_Menu.ReadValue<float>() > 0)
        {
            TimeScale(0);
        }
        if (input._SpaceFighter_Controls_XR.Secret_Menu_off.ReadValue<float>() > 0)
        {
            TimeScale(1);
        }
    }
    
    
    public void Dead_Game()
    {
        Button_Resume.SetActive(false);
        TimeScale(0);
    }
    public void Start_Game()
    {
        TimeScale(1);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Infected_Planet");
    }
    public void For_Restart()
    {
        SceneManager.LoadScene("Forerunner_Space");
    }

    public void Enter_Garage()
    {
        SceneManager.LoadScene("Garage");
    }
    public void Exit_Game()
    {
        Application.Quit();
    }
    
    public void Change_Collor(int num)
    {
        foreach (GameObject Ship in Ship_Color)
        {
            Ship.SetActive(false);
        }
        Ship_Color[num].SetActive(true);
        Save_Ship_Color = num;
    }
    
    
    public void Music_Controller(int num)
    {
        foreach (GameObject MUZ in Musics)
        {
            MUZ.SetActive(false);
        }
        Musics[num].SetActive(true);
    }
    

    public void TimeScale(float time)
    {
        //Time.timeScale = time;
        //Time.timeScale = 1;
        if (time == 1)
        {
            Infinity_Wave_Controller.in_Menu = false;
            _SC_Health.in_menu = false;
            Level.SetActive(true);
            //Obj_in_Menu_UI.SetActive(false);
            foreach (GameObject Obj in Obj_in_Menu_UI)
            {
                try
                {
                    Obj.SetActive(false);
                }
                catch{
                }
                    
            }
            Weapon_Controller.SetActive(true);
            Player_Ship.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //Player_Ship.gameObject.GetComponent<SpaceFighter_Controller>().enabled = true;
            //Player_Ship.gameObject.GetComponent<AR_Aim_Controller>().enabled = true;
            foreach (GameObject Obj in Obj_in_Game_M)
            {
                try
                {
                    Obj.SetActive(true);
                }
                catch{
                }
                    
            }
            _SC_JSON.ContiniueTime();
        }

        if (time == 0)
        {
            Infinity_Wave_Controller.in_Menu = true;
            _SC_Health.in_menu = true;
            Level.SetActive(false);
            Weapon_Controller.SetActive(false);
            //Obj_in_Menu_UI.SetActive(true);
            foreach (GameObject Obj in Obj_in_Menu_UI)
            {
                try
                {
                    Obj.SetActive(true);
                }
                catch{
                }
                    
            }
            //Player_Ship.gameObject.GetComponent<AR_Aim_Controller>().enabled = false;
            //Player_Ship.gameObject.GetComponent<SpaceFighter_Controller>().enabled = false;
            foreach (GameObject Obj in Obj_in_Game_M)
            {
                try
                {
                    Obj.SetActive(false);
                }
                catch{
                }
                    
            }
            Player_Ship.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _SC_JSON.StopTime();
        }
        //Функция варп остановки, как одна из фич игры. это можно использовать в тактике как сверх торможение.
    }

}
