using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Added on Script Holder on jierarchy
public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    ManageScenes manageScenes;

    [SerializeField] GameObject levelStartPanel;
    [SerializeField] TMP_Text levelName;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        manageScenes = GetComponent<ManageScenes>();
    }
    private void Start()
    {
        levelName.text = "Level "+ (manageScenes.CurrentLevelIndex+1);
    }
    public void CloseLevelStartPanel()
    {
        levelStartPanel.SetActive(false);
    }
    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: " + gameManager.Score;
    }
    public void LoadNextLevelButton()
    {
        manageScenes.LoadNextLevel();
    }

}
