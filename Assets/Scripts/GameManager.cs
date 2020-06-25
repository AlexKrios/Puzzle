using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [NonSerialized]
    public int fieldSize = 0;               //Field size

    [Range(1, 10)]
    public int gameSpeed = 3;               //Game speed
    [Range(0, 500)]
    public int shufleCount = 200;           //Start shufle count

    [NonSerialized]
    public List<BlockStatus> blocksList;    //List of object     
    [NonSerialized]
    public List<int> blocksActive;          // List of active block, which can move

    [NonSerialized]
    public BlockStatus blockEmpty;          //Empty block without sprite and number
    [NonSerialized]
    public BlockStatus blockCurrent;        //Pressed block

    /* Sub modules */
    public SaveLoadManager saveLoadManager;
    public GameInit gameInit;
    public GameStart gameStart;
    public GameTurn gameTurn;
    public GameEnd gameEnd;
    public CheckWin checkWin;
    public ModalManager modalManager;

    [NonSerialized]
    public static GameState gameState;      //Game state

    [NonSerialized]
    public bool isShufle = true;            //If blocks shufling
    [NonSerialized]
    public bool isModal = false;            //If pop-up open
    [NonSerialized]
    public bool isMove = false;             //If block is moving

    void Start()
    {
        gameState = GameState.Init;
        modalManager.CreateModalStart();               
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Init:
                //Debug.LogWarning("Init");
                blocksList = gameInit.CreateAllBlocks();
                return;
            case GameState.TurnStart:
                //Debug.LogWarning("Start");
                gameStart.Execute();
                return;
            case GameState.Turn:
                //Debug.LogWarning("Turn");
                gameTurn.Execute();
                return;
            case GameState.TurnEnd:
                //Debug.LogWarning("End");
                gameEnd.Execute();
                return;
            case GameState.CheckWin:
                //Debug.LogWarning("CheckWin");
                checkWin.Execute();
                return;
            default:
                return;
        }
    }    
}
