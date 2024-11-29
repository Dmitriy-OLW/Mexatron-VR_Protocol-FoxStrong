using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour
{
    public int Save_Ship_Color;
    [SerializeField] private JSon_Save_Controller JSON_Controller;

    [SerializeField] private GameObject[] Ship_Color;
    
    protected Input_Map_SpaceFighter input;
    
    
    private void Awake()
    {
        JSON_Controller.story_mode = false;
        Change_Collor(0);
    }

    private void Start()
    {
        Change_Collor(Save_Ship_Color);
    }

    public void Story_Game(bool storymode)
    {
        JSON_Controller.story_mode = storymode;
    }

    public void Enter_Mission()
    {
        SceneManager.LoadScene("Infected_Planet");
    }
    
    public void Enter_Garage()
    {
        SceneManager.LoadScene("Garage");
    }
    public void Enter_Infinity()
    {
        SceneManager.LoadScene("Forerunner_Space");
    }
    public void Enter_Story()
    {
        SceneManager.LoadScene("Start_CutScene_CockpitScene");
    }
    public void Enter_Train()
    {
        SceneManager.LoadScene("Dev_Fox_Space");
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





}
