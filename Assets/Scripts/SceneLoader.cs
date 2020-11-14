using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void GoToScene(int idScene)
    {
        if (idScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(idScene);
    }

    public static void GoToNextScene()
    {
        GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void GoToPrevScene()
    {
        GoToScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void GoToStart()
    {
        GoToScene(0);
    }

    public static void GoToEnd()
    {
        GoToScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
