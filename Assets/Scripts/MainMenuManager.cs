using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneLoader.GoToNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneLoader.GoToPrevScene();
    }

    public void GotToMenu()
    {
        SceneLoader.GoToStart();
    }
}