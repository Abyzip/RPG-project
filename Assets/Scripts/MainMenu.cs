using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
    {
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ChangeScene(int nextSceneIndex) //index of scene to move to
    {
        Scene nextScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex + 1);
        SceneManager.LoadScene(nextSceneIndex+1);
    }
}
