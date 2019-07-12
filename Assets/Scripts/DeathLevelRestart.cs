using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLevelRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("RestartLevel", 2.0f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
