using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Pause, GameOver };

    private GameState gameState;

    [SerializeField] private float maxTimeToCatchNextChild = 60f;
    [SerializeField] private float timeReductionFactorBetweenTwoChildrenCaught = 0.075f;
    [SerializeField] private TimerVisual timerVisual;
    [SerializeField] private GameOverMenu gameOverMenu;

    private float timer;

    private int score = 0;


    //public class OnGameStateChangedEventArgs : EventArgs
    //{
    //    public GameState newState;
    //}
    //public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;

    //public event EventHandler OnChildCaught;



    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            timer += Time.deltaTime;

            timerVisual.UpdateVolumeValues(GetNormalizedTimeLeft());

            if (timer >= maxTimeToCatchNextChild)
            {
                EndGame("Out of time");
                //OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
            }
        }
    }


    public void ChildCaught()
    {
        score++;
        maxTimeToCatchNextChild -= maxTimeToCatchNextChild * timeReductionFactorBetweenTwoChildrenCaught;
        //OnChildCaught?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerCaughtByPolice()
    {

    }

    private void StartGame()
    {
        if (gameState != GameState.Playing)
        {
            gameState = GameState.Playing;
            timer = 0;
        }
        //OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
    }

    private void EndGame(string causeOfGameOver)
    {
        gameState = GameState.GameOver;
        gameOverMenu.Show(score, causeOfGameOver);
    }

    public void TogglePauseGame()
    {
        if (gameState == GameState.Pause)
        {
            gameState = GameState.Playing;
        }
        else
        {
            gameState = GameState.Pause;
        }
    }


    public GameState GetGameState()
    {
        return gameState;
    }

    private float GetNormalizedTimeLeft()
    {
        return timer / maxTimeToCatchNextChild;
    }

    public int GetScore()
    {
        return score;
    }
}
