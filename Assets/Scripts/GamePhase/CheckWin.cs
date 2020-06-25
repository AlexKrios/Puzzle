using System.Linq;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public GameManager manager;

    public void Execute()
    {
        if (manager.isModal)
        {
            return;
        }

        var checkWin = manager.blocksList.SkipWhile(x => x.place == x.number);
        if (!checkWin.Any() && !manager.isShufle)
        {
            manager.modalManager.CreateModalWin();
            PlayerPrefs.DeleteKey("isLoad");

            return;
        }

        GameManager.gameState = GameState.TurnStart;
    }
}
