using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        ResetForNewGame();
        SceneManager.LoadScene("Scene_GaiasHouse");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ResetForNewGame()
    {
        DayCycle.ResetForNewGame();
        PlantPickup.ResetForNewGame();
    }
}