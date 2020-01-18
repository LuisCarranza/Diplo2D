using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject HUDPanel;
    public GameObject pausePanel;
    // HUD Elements
    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI livesText;
    // Pause Elements
    public Slider musicSlider;
    public Slider sfxSlider;
    // Audio Elements
    public AudioMixer mainMixer;
    private float musicVolume;
    private float sfxVolume;

    void Start()
    {
        CleanUI();
        HUDPanel.SetActive(true);
    }

    void Update()
    {
        itemsText.text = "x" + DataLoader.instance.currentPlayer.items;
        livesText.text = "x" + DataLoader.instance.currentPlayer.lives;
    }

    private void CleanUI()
    {
        HUDPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void ShowPause()
    {
        CleanUI();
        pausePanel.SetActive(true);
        mainMixer.GetFloat("musicVolume", out musicVolume);
        musicSlider.value = musicVolume;
        mainMixer.GetFloat("sfxVolume", out sfxVolume);
        sfxSlider.value = sfxVolume;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        CleanUI();
        HUDPanel.SetActive(true);
    }

    public void Exit()
    {
        DataLoader.instance.WriteData();
        Application.Quit();
    }

    public void SetMusic(float volume)
    {
        mainMixer.SetFloat("musicVolume", volume);
    }

    public void SetSFX(float sfx)
    {
        mainMixer.SetFloat("sfxVolume", sfx);
    }

}
