using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameManager manager;

    public void Execute()
    {
        SwapPlace();
        ResetVariable();
        SaveGameData();

        GameManager.gameState = GameState.CheckWin;
    }

    private void SwapPlace()
    {
        var blockEmptyPlace = manager.blockEmpty.place;
        var blockCurrentPlace = manager.blockCurrent.place;

        foreach (BlockStatus block in manager.blocksList)
        {
            if (block.place == blockEmptyPlace && block.type == "Empty")
            {
                block.place = blockCurrentPlace;
                continue;
            }

            if (block.place == blockCurrentPlace && block.type == "Block")
            {
                block.place = blockEmptyPlace;
                continue;
            }
        }
    }

    private void ResetVariable()
    {
        manager.blocksActive = null;
        manager.blockEmpty = null;
        manager.blockCurrent = null;
    }

    private void SaveGameData()
    {
        if (manager.isShufle)
        {
            return;
        }

        manager.saveLoadManager.Save();
    }
}
