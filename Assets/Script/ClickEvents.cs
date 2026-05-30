using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickEvents : MonoBehaviour
{
    public GameObject Settings, Games;

    public void onStartClicked()
    {
        Games.SetActive(true);
    }

    public void onSettingsClicked()
    {
        Settings.SetActive(true);
    }

    public void onBackClicked()
    {
        Games.SetActive(false);
        Settings.SetActive(false);
    }

    public void onBowlingClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }


}
