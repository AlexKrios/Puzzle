using System.Collections;
using UnityEngine;

public class GameTurn : MonoBehaviour
{
    public GameManager manager;

    public void Execute()
    {
        var isBlockEmpty = manager.blockCurrent == null;
        var isMove = manager.isMove;

        if (isBlockEmpty || isMove)
        {
            return;
        }

        StartCoroutine(Swap());
    }

    public IEnumerator Swap()
    {
        var speed = manager.gameSpeed;
        var emptyBlockPos = manager.blockEmpty.gameObject.transform.position;
        var currentBlockPos = manager.blockCurrent.gameObject.transform.position;

        manager.isMove = true;

        var time = 0f;
        while (manager.blockCurrent.gameObject.transform.position != emptyBlockPos)
        {
            manager.blockEmpty.gameObject.transform.position = Vector3.Lerp(emptyBlockPos, currentBlockPos, time);
            manager.blockCurrent.gameObject.transform.position = Vector3.Lerp(currentBlockPos, emptyBlockPos, time);

            time += Time.deltaTime * speed * 20;
            yield return null;
        }

        GameManager.gameState = GameState.TurnEnd;
        manager.isMove = false;
    }
}
