using UnityEngine;
using UnityEngine.SceneManagement;

public class ModalHandler : MonoBehaviour
{
    private GameManager _manager;

    private void Start()
    {
        _manager = GameObject.Find("Managers").GetComponent<GameManager>();
    }    

    public void SetFieldSize(int size)
    {
        _manager.fieldSize = size;
        _manager.isModal = false;

        Destroy(_manager.modalManager.modalStart.gameObject);
    }

    public void RestartGame()
    {
        Destroy(_manager.modalManager.modalWin.gameObject);
        SceneManager.LoadScene("Game");
    }
}
