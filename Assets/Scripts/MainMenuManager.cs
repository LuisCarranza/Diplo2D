using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    private int sceneToLoad;

    void Start()
    {
        sceneToLoad = DataLoader.instance.currentPlayer.lastLevel;
        Debug.Log(sceneToLoad);
        MainMenu();
    }

    void CleanUI()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void MainMenu()
    {
        CleanUI();
        mainMenuPanel.SetActive(true);
    }

    public void OptionsMenu()
    {
        CleanUI();
        optionsPanel.SetActive(true);
    }
}
