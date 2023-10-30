using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI causeOfGameOverText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject background;


    public void Show(int score, string causeOfGameOver)
    {
        causeOfGameOverText.text = causeOfGameOver;

        if (score < 2)
        {
            scoreText.text = $"{score} child massacred";
        }
        else
        {
            scoreText.text = $"{score} children massacred";
        }

        background.SetActive(true);
    }


    public void QuitApplication()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
