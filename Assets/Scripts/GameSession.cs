using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    void Awake()
    {
        SetUpSingelton();   
    }

    private void SetUpSingelton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
 
        if(numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // Don't destory the target object when new scene is loaded
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
