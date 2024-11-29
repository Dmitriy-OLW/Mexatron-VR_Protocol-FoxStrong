using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;
using UnityEngine.Audio;
using System.IO;
public class Start_Cut_Menu : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    [SerializeField] private Slider general;
    SaveData _data = new SaveData();
    [SerializeField]
    private string SavePath;

    private void Start()
    {
        SavePath = Application.persistentDataPath + "/save.gameData";
        if (File.Exists(SavePath))
        {
            string fileData = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(fileData);

            Mixer.audioMixer.SetFloat("MasterVolume", ((data.general * 80) - 60));
            Mixer.audioMixer.SetFloat("MusicVolume", ((data.music * 80) - 60));
            Mixer.audioMixer.SetFloat("SFXVolume", ((data.sfx * 80) - 60));
            Mixer.audioMixer.SetFloat("VoiseVolume", ((data.voise * 80) - 60));

            general.value = data.general;
        }

    }

    private void Update()
    {
        Mixer.audioMixer.SetFloat("MasterVolume", ((general.value * 80) - 60));
    }

    public void Enter_Garage()
    {
        SceneManager.LoadScene("Garage");
    }
    public void Exit_Game()
    {
        Application.Quit();
    }
}
