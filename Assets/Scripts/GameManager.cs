using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Pause, GameOver };

    private GameState gameState;

    public static GameManager instance;

    [SerializeField] private float maxTimeToCatchNextChild = 60f;
    [SerializeField] private float timeReductionFactorBetweenTwoChildrenCaught = 0.075f;
    private float timer;

    private float score = 0;


    public class OnGameStateChangedEventArgs : EventArgs
    {
        public GameState newState;
    }
    public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;

    public event EventHandler OnChildCaught;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            gameState = GameState.GameOver;
            OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
        }
    }


    public void ChildCaught()
    {
        score++;
        maxTimeToCatchNextChild -= maxTimeToCatchNextChild * timeReductionFactorBetweenTwoChildrenCaught;
        OnChildCaught?.Invoke(this, EventArgs.Empty);
    }

    public void StartGame()
    {
        gameState = GameState.Playing;
        timer = maxTimeToCatchNextChild;
        OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { newState = gameState });
    }


    public GameState GetGameState()
    {
        return gameState;
    }
}
