using System;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    public GameManager manager;

    [NonSerialized]
    public GameObject modalStart;
    [NonSerialized]
    public GameObject modalWin;

    public void CreateModalStart()
    {
        if (PlayerPrefs.HasKey("isLoad"))
        {
            return;
        }

        manager.isModal = true;

        modalStart = Instantiate(Resources.Load("Modal/Start") as GameObject);
        modalStart.name = "ModalStart";
    }

    public void CreateModalWin()
    {
        manager.isModal = true;

        modalWin = Instantiate(Resources.Load("Modal/Win") as GameObject);
        modalWin.name = "ModalWin";
    }
}
