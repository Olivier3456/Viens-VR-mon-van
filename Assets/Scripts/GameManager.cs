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


    public class OnGameStateChangedEventArgs : EventArgs { public GameState newState; }
    public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;

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
            }
        }
    }


    public void ChildCaught()
    {
        score++;
        timer = 0;
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
        OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
    }

    private void EndGame(string causeOfGameOver)
    {
        gameState = GameState.GameOver;
        gameOverMenu.Show(score, causeOfGameOver);
        OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
    }

    // Pas de pause : la pause, c'est pour les faibles.
    //public void TogglePauseGame()
    //{
    //    if (gameState == GameState.Pause)
    //    {
    //        gameState = GameState.Playing;
    //    }
    //    else if (gameState == GameState.Playing)
    //    {
    //        gameState = GameState.Pause;
    //    }

    //    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
    //}


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
