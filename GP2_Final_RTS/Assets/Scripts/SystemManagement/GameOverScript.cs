using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScript : MonoBehaviour
{
    public Text winner;
    public GameObject GameOverPanel;

    public void Start()
    {
        GameOverPanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver(string _winner)
    {
        GameOverPanel.SetActive(true);
        if (_winner == "Player")
            winner.name = "You Won";
        else if(_winner == "AI")
            winner.name = "AI Won";
    }
}
