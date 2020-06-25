using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInit : MonoBehaviour
{
    public GameManager manager;

    public GameObject gameFieldBg;
    public GameObject gameFieldZone;

    private SaveLoadData _loadData;

    private int _fieldSize;
    private float _blockSize;
    
    private int _count = 0;

    private readonly int _borderPaddingSize = 35;    

    public List<BlockStatus> CreateAllBlocks()
    {
        _loadData = LoadGameData();
        _fieldSize = SetFieldSize();
        _blockSize = SetBlockSize();

        if (_fieldSize == 0)
        {
            return null;
        }        

        var blocks = new List<BlockStatus>();
        for (int i = 0; i < Math.Pow(_fieldSize, 2); i++)
        {
            BlockStatus go = CreateBlock();
            
            blocks.Add(go);
            _count++;
        }

        GameManager.gameState = GameState.TurnStart;
        return blocks;
    }

    private int SetFieldSize()
    {
        if (PlayerPrefs.HasKey("isLoad"))
        {
            manager.fieldSize = _loadData.fieldSize;
        }

        return manager.fieldSize;
    }    

    private float SetBlockSize()
    {
        if (_fieldSize == 0)
        {
            return 0;
        }

        var _canvasSize = gameFieldBg.GetComponent<RectTransform>().sizeDelta.x - 2 * _borderPaddingSize;
        return _canvasSize / _fieldSize;
    }

    private SaveLoadData LoadGameData()
    {
        if (!PlayerPrefs.HasKey("isLoad"))
        {
            return null;
        }

        manager.gameSpeed = 1;
        manager.isShufle = false;

        return manager.saveLoadManager.Load();
    }

    private BlockStatus CreateBlock() 
    {
        int number = SetBlockNumber();

        /* Block GameObject */
        var block = new BlockStatus();        
        var blockGameObject = Instantiate(Resources.Load("Block/Block") as GameObject);        
        var blockRectT = blockGameObject.GetComponent<RectTransform>();

        /* Position calculating */
        var blockPosX = _blockSize * (_count % _fieldSize);
        var blockPosY = _blockSize * (_count / _fieldSize);
        /* End position calculating */

        blockGameObject.name = $"Block{number}";
        blockRectT.SetParent(gameFieldZone.transform);
        blockRectT.sizeDelta = new Vector2(_blockSize, _blockSize);
        blockRectT.localPosition = new Vector3(blockPosX, -blockPosY, 0);
        blockRectT.localScale = new Vector3(1, 1, 1);
        /* End block GameObject */

        /* Block info export */
        block.gameObject = blockGameObject;
        block.gameObjectScript = blockGameObject.GetComponent<BlockBehaviour>();
        block.place = _count + 1;
        block.number = number;
        block.type = "Block";
        /* End block info export */

        /* Empty block GameObject */
        if (number == Math.Pow(_fieldSize, 2))
        {
            blockGameObject.GetComponent<Image>().sprite = null;
            blockGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            block.type = "Empty";

            return block;
        }
        /* End empty block GameObject */

        /* Block text GameObject */
        var blockNumber = blockGameObject.transform.Find("Number");
        var blockNumberRectT = blockNumber.GetComponent<RectTransform>();
        var blockNumberText = blockNumber.GetComponent<Text>();

        blockNumberRectT.sizeDelta = new Vector2(_blockSize - 75, _blockSize - 75);
        blockNumberText.text = number.ToString();
        /* End block text GameObject */

        return block;
    }

    private int SetBlockNumber()
    {
        if (PlayerPrefs.HasKey("isLoad"))
        {
            return _loadData.blockNumbers[_count];
        }

        return _count + 1;
    }
}
