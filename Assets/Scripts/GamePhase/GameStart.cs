using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameManager manager;

    private BlockStatus emptyBlock;

    private int shufleCount = 0;

    public void Execute()
    {        
        manager.blockEmpty = SetEmptyBlock();
        manager.blocksActive = SetActiveBlock();
        ShufleBlocks();

        GameManager.gameState = GameState.Turn;
    }

    public BlockStatus SetEmptyBlock()
    {
        emptyBlock = manager.blocksList.First(x => x.type == "Empty");

        return emptyBlock;
    }

    public List<int> SetActiveBlock()
    {
        emptyBlock.position = CheckBlockPosition(emptyBlock.place);
        var list = emptyBlock.gameObjectScript.CheckPosition();

        return list;
    }

    public void ShufleBlocks()
    {
        if (!manager.isShufle)
        {
            return;
        }
        
        if (shufleCount < manager.shufleCount)
        {
            var rIndex = UnityEngine.Random.Range(0, manager.blocksActive.Count);
            manager.blockCurrent = manager.blocksList.First(x => x.place == manager.blocksActive[rIndex]);

            shufleCount++;
            return;
        }

        manager.isShufle = false;        
        manager.gameSpeed = 1;

        manager.saveLoadManager.Save();        
    }

    private BlockPosition CheckBlockPosition(int count)
    {
        var fieldSize = manager.fieldSize;

        bool top = count <= fieldSize;
        bool bottom = count > Math.Pow(fieldSize, 2) - fieldSize;
        bool left = count % fieldSize == 1;
        bool right = count % fieldSize == 0;

        /* Check if TopLeft */
        if (top && left)
        {
            return BlockPosition.TopLeft;
        }

        /* Check if TopRight */
        if (top && right)
        {
            return BlockPosition.TopRight;
        }

        /* Check if BottomLeft */
        if (bottom && left)
        {
            return BlockPosition.BottomLeft;
        }

        /* Check if BottomRight */
        if (bottom && right)
        {
            return BlockPosition.BottomRight;
        }

        /* Check if Top */
        if (top)
        {
            return BlockPosition.Top;
        }

        /* Check if Bottom */
        if (bottom)
        {
            return BlockPosition.Bottom;
        }

        /* Check if Left */
        if (left)
        {
            return BlockPosition.Left;
        }

        /* Check if Right */
        if (right)
        {
            return BlockPosition.Right;
        }

        return BlockPosition.Center;
    }
}
