using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using UnityEngine.SceneManagement;

//using System;


public class JSon_Save_Controller : MonoBehaviour
{
    //DOD - TIMER SECTION----------------
    public TextMeshProUGUI DOD_text_Score_Player;
    public TextMeshProUGUI DOD_text_Score_PC_Menu;

    public GameObject UI_END_Score;
    //-----------------------------------
    
    
    public string next_level_name;
    
    public bool is_MainMenu;
    public bool SpaceFighter;
    
    public bool story_mode;
    public float timer;
    public int destroy;
    public int orb;
    
    public TextMeshProUGUI text_Score;
    public TextMeshProUGUI text_Score_OnGAME;

    public AudioMixerGroup Mixer;
    
    protected Input_Map_SpaceFighter input;
    
    SaveData _data = new SaveData();
    [SerializeField]
    private string SavePath;

    [SerializeField] private GameObject Xr_Obj;

    [SerializeField] private GameObject CutScene_Warp_Galaxy;
    [SerializeField] private GameObject[] All_Map_Del;
    [SerializeField] private GameObject Warp_Text;
    
    [SerializeField] private Transform UI_End;
    
    [SerializeField] private AudioSource End_not_enemy_Audio;
    [SerializeField] private AudioSource End_all_mission_Audio;
    

    [SerializeField] private Slider general;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider voise;
    
    [SerializeField] private Slider map;

    [SerializeField] private Menu_Controller _SC_MENU;
    [SerializeField] private Main_Menu_Script _SC_MAINMENU;
    [SerializeField] private Infinity_Map_Controller Infinity_Wave_Controller;
    
    
    private float _timeLeft = 0f;
    private float _Nachal_Time;
    private bool _Stop_Time;

    private float Timer_CutScene;


    //----
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
        input = new Input_Map_SpaceFighter();
            //UI_END_Score.SetActive(false);
    }
    

    private void Start()
    {
        timer = 0;
        destroy = 0;
        orb = 0;
        _Stop_Time = false;
        Timer_CutScene = 0;
        SpaceFighter = false;
        CutScene_Warp_Galaxy.SetActive(false);
        Warp_Text.SetActive(false);
        //UI_ALL.SetActive(false);
        SavePath = Application.persistentDataPath + "/save.gameData";
        if (File.Exists(SavePath)){
            string fileData = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(fileData);

            float minutes_A = Mathf.FloorToInt(data.best_time / 60);
            float seconds_A = Mathf.FloorToInt(data.best_time % 60);
            string A = string.Format("Best Time: {0:00} : {1:00}", minutes_A, seconds_A);
            

            text_Score.text = string.Format(A + "\r\nDestroyed: "+ data.kill_count + "\r\nCollected: " + data.spher_collect);
            

            if (is_MainMenu)
            {
                _SC_MAINMENU.Save_Ship_Color = data.ship_color;
                _SC_MAINMENU.Change_Collor(data.ship_color);
            }
            else
            {
                Infinity_Wave_Controller.infinity_wave_mode = !data.story_mode;;
                _SC_MENU.Change_Collor(data.ship_color);
                _SC_MENU.Save_Ship_Color = data.ship_color;
                if (data.story_mode)
                {
                    _SC_MENU.Change_Collor(0);  
                }
            }

            
            
            //ChageXRT_Start(data.player_scale);

            //Sound
            Mixer.audioMixer.SetFloat("MasterVolume", ((data.general * 80) - 60));
            Mixer.audioMixer.SetFloat("MusicVolume", ((data.music * 80) - 60));
            Mixer.audioMixer.SetFloat("SFXVolume", ((data.sfx * 80) - 60));
            Mixer.audioMixer.SetFloat("VoiseVolume", ((data.voise * 80) - 60));
            
            general.value = data.general;
            music.value = data.music;
            sfx.value = data.sfx;
            voise.value = data.voise;
            map.value = data.player_scale;

            story_mode = data.story_mode;


            _data.general = data.general;
            _data.music = data.music;
            _data.sfx = data.sfx;
            _data.voise = data.voise;
            //Setting
            _data.player_scale = data.player_scale;
            _data.ship_color = data.ship_color;
            _data.story_mode = data.story_mode;
            //Score
            _data.best_time = data.best_time;
            _data.kill_count = data.kill_count;
            _data.spher_collect = data.spher_collect;

        }   
        //StartTime();


    }  
    private void Update() {
        ChageVolume();
        End_CutScene();

    } 
    public void ChageVolume()
    {

        Mixer.audioMixer.SetFloat("MasterVolume", ((general.value * 80) - 60));
        Mixer.audioMixer.SetFloat("MusicVolume", ((music.value * 80) - 60));
        Mixer.audioMixer.SetFloat("SFXVolume", ((sfx.value * 80) - 60));
        Mixer.audioMixer.SetFloat("VoiseVolume", ((voise.value * 80) - 60));
        
        _data.general = general.value;
        _data.music = music.value;
        _data.sfx = sfx.value;
        _data.voise = voise.value;
    }
    
    /*public void ChageXRT()
    {
        ChageXRT_Start(map.value);
    }*/
    
    public void Enter_Next_Map()
    {
        SceneManager.LoadScene(next_level_name);
    }

    private void End_CutScene()
    {
        if (_data.story_mode && destroy >= 5)
        {
            Warp_Text.SetActive(true);
            if(SpaceFighter == true || Timer_CutScene>0)
            {
                if (Timer_CutScene == 0)
                {
                    if (destroy >= 14)
                    {
                        End_all_mission_Audio.Play();
                    }
                    else
                    {
                        End_not_enemy_Audio.Play();
                    }
                    Save();
                    _SC_MENU.Music_Controller(2);
                    CutScene_Warp_Galaxy.SetActive(true);
                    UI_End.transform.Rotate(0, 180, 0);
                    foreach (GameObject Obj in All_Map_Del)
                    {
                        Obj.SetActive(false);
                    }
                    
                    
                }
                

                Timer_CutScene += Time.deltaTime;
                if (Timer_CutScene > 14)
                {
                    _SC_MENU.Dead_Game();
                }
                if (Timer_CutScene > 15)
                {
                    Enter_Next_Map();
                }

            }
            
            
        }
    }

    public void Pokaz_Time()
    {
        
        DOD_text_Score_Player.text = "Time: " + text_Score_OnGAME.text;
        DOD_text_Score_PC_Menu.text = "Time: " + text_Score_OnGAME.text;
        
        //UI_END_Score.SetActive(true);
    }
    
    private void ChageXRT_Start(float znac)
    {
        float _Scale = (znac*100) * 40 + 1000;
        Xr_Obj.transform.localScale = new Vector3(_Scale, _Scale, _Scale);
        _data.player_scale = znac;
    }
    
    public void Save()
    {
        //Score
        if (_timeLeft > _data.best_time)
        {
            _data.best_time = _timeLeft;
        }

        if (destroy > _data.kill_count)
        {
            _data.kill_count = destroy;
        }

        if (orb > _data.spher_collect)
        {
            _data.spher_collect = orb;
        }
        if (is_MainMenu)
        {
                
            _data.ship_color = _SC_MAINMENU.Save_Ship_Color;
        }
        else
        {
            //_data.ship_color = _SC_MENU.Save_Ship_Color;
        }

        
        _data.story_mode = story_mode;
        string jsonString = JsonUtility.ToJson(_data);
        File.WriteAllText(SavePath, jsonString);
    }
    
    
    private IEnumerator StartTimer()
    {
        while (_timeLeft > -1)
        {
            _timeLeft += Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }
    public void StartTime()
    {
        _timeLeft = timer;
        _Stop_Time = false;
        StartCoroutine(StartTimer());
    }

    public void StopTime()
    {
        timer = _timeLeft;
        _Stop_Time = true;
    }

    public void ContiniueTime()
    {
        _timeLeft = timer;
        _Stop_Time = false;
    }
 
    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        string A = string.Format("Time: {0:00} : {1:00}", minutes, seconds);
        if (_Stop_Time == false)
        {
            text_Score_OnGAME.text = string.Format(A + "\r\nDestroyed: "+ destroy + "\r\nCollected: " + orb);
            DOD_text_Score_Player.text = string.Format(A + "\r\nDestroyed: "+ destroy + "\r\nCollected: " + orb);
            DOD_text_Score_PC_Menu.text = string.Format(A + "\r\nDestroyed: "+ destroy + "\r\nCollected: " + orb);
        }

        
        /*if (first_start && _timeLeft > 3 && _SC_MENU.Hand_UI.gameObject.activeInHierarchy == false)
        {
            
            UI_ALL.SetActive(true);
            first_start = false;
            StartTimer();
            _SC_MENU.AudioListner_Controller(false);
            _SC_MENU.TimeScale(0);
        }*/
    }


    


}

[System.Serializable]
public class SaveData
{
    //Sound
    public float general = 0.6f;
    public float music = 1;
    public float sfx = 1;
    public float voise = 1;

    //Setting
    public float player_scale = 0.5f;
    public int ship_color = 0;
    public bool story_mode = false;
    
    
    //Score
    public float best_time = 0;
    public int kill_count = 0;
    public int spher_collect = 0;
    



}


