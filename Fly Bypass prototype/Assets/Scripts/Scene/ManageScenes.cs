using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Added on Script Holder
public class ManageScenes : MonoBehaviour
{
    int currentLevelIndex = 0;
    public int CurrentLevelIndex { get => currentLevelIndex; }

    private void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadNextLevel()
    {
        //After last level, it returns back to first level
        if ((currentLevelIndex + 1) == SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = 0;
        }
        else
        {
            currentLevelIndex += 1;
        }
        SceneManager.LoadScene(currentLevelIndex);
    }

}
