using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Will change our scene to the string passed in
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Reloads the surrrent scene we are in
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Loads out the Title scene. Muist be called Title exactly
    public void ToTitleScene()
    {
        GameController.instance.controlType = ControlType.Normal;
        SceneManager.LoadScene("Title");
    }

    //Gets our active scenes name 
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //Quits our game
    public void QuitGame()
    {
        Application.Quit();
    }
}
