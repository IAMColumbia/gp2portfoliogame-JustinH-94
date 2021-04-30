using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject controlPanel;

    public void Start()
    {
        mainPanel.SetActive(true);
        controlPanel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlPanel()
    {
        controlPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void NopeNotPlaying()
    {
        Application.Quit();
    }
}
