using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("isLoad");

        SceneManager.LoadScene("Game");        
    }
}