using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    private int nextSceneToLoad;
    private int previousSceneToLoad;

    private void Start()
    {
        previousSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void Update()
    {
        ControllerInput();
    }

    private void ControllerInput()
    {
        float leftTrigger = Input.GetAxis("LT");
        float rightTrigger = Input.GetAxis("RT");

        if (Input.GetButton("Restart"))
        {
            RestartLevel();
        }
        else if (leftTrigger > 0)
        {
            if (previousSceneToLoad < 0)
            {
                previousSceneToLoad = 0;
                SceneManager.LoadScene(previousSceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(previousSceneToLoad);
            }
        }
        else if (rightTrigger > 0)
        {
            if (nextSceneToLoad >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
