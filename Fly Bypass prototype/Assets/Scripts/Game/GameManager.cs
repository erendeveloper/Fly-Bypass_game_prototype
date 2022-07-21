using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on ScriptHolder
public class GameManager : MonoBehaviour
{

    UIManager uiManager;

    private bool gamePlay = false;
    public bool GamePlay { get => gamePlay; }

    private int score = 0;
    public int Score { get => score; }


    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
    }

    private void Update()
    {
        if (!gamePlay)
        {
            StartGame();
        }
            
    }
    void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gamePlay = true;
            uiManager.CloseLevelStartPanel();
        }

    }
    public void GameOver(string tag,float score)
    {
        if (tag=="Player")
        {
            int _score = Mathf.FloorToInt(score);
            gamePlay = false;
            this.score = _score;
            uiManager.OpenGameOverPanel();
        }
        
    }
}
