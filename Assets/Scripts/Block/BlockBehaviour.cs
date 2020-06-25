using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    private GameManager _manager;
    private int _fieldSize;

    void Start()
    {
        _manager = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    public void TouchHandler()
    {
        if (_manager.isMove || _manager.isShufle)
        {
            return;
        }

        var blockCurrent = _manager.blocksList.First(x => x.gameObject == gameObject);
        var isActive = _manager.blocksActive.Contains(blockCurrent.place);
        if (!isActive)
        {
            return;
        }

        _manager.blockCurrent = blockCurrent;        
    }

    #region Set position
    public List<int> CheckPosition()
    {
        var blocksActive = new List<int>();
        _fieldSize = _manager.fieldSize;

        switch (_manager.blockEmpty.position)
        {
            case BlockPosition.TopLeft:
                blocksActive.Add(BottomElement());
                blocksActive.Add(RightElement());
                break;

            case BlockPosition.TopRight:
                blocksActive.Add(BottomElement());
                blocksActive.Add(LeftElement());
                break;

            case BlockPosition.BottomLeft:
                blocksActive.Add(TopElement());
                blocksActive.Add(RightElement());
                break;

            case BlockPosition.BottomRight:
                blocksActive.Add(TopElement());
                blocksActive.Add(LeftElement());
                break;

            case BlockPosition.Top:
                blocksActive.Add(BottomElement());
                blocksActive.Add(LeftElement());
                blocksActive.Add(RightElement());
                break;

            case BlockPosition.Bottom:
                blocksActive.Add(TopElement());
                blocksActive.Add(LeftElement());
                blocksActive.Add(RightElement());
                break;

            case BlockPosition.Left:
                blocksActive.Add(TopElement());
                blocksActive.Add(BottomElement());
                blocksActive.Add(RightElement());
                break;

            case BlockPosition.Right:
                blocksActive.Add(TopElement());
                blocksActive.Add(BottomElement());
                blocksActive.Add(LeftElement());
                break;

            case BlockPosition.Center:
                blocksActive.Add(TopElement());
                blocksActive.Add(BottomElement());
                blocksActive.Add(LeftElement());
                blocksActive.Add(RightElement());
                break;
        }

        return blocksActive;
    }

    public int TopElement()
    {
        //Debug.Log($"Top element: {place - _fieldSize}");
        return _manager.blockEmpty.place - _fieldSize;
    }

    public int BottomElement()
    {
        //Debug.Log($"Bottom element: {place + _fieldSize}");
        return _manager.blockEmpty.place + _fieldSize;
    }

    public int LeftElement()
    {
        //Debug.Log($"Left element: {place - 1}");
        return _manager.blockEmpty.place - 1;
    }

    public int RightElement()
    {
        //Debug.Log($"Right element: {place + 1}");
        return _manager.blockEmpty.place + 1;
    }
    #endregion
}
