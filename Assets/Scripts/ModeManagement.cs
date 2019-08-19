using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManagement : Singleton<ModeManagement>
{
    private string m_Mode = "";

    [SerializeField]
    private GameObject m_SelectModeMenu;


    public void SelectAuthorMode()
    {
        m_Mode = "Author";
        Debug.Log("Selected AuthorMode.");
        SelectModeMenuHide();
    }

    public void SelectOperaterMode()
    {
        m_Mode = "Operater";
        Debug.Log("Selected OperaterMode.");
        SelectModeMenuHide();
    }

    public string GetSelectMode
    {
        get { return m_Mode; }
    }

    public void SelectModeMenuShow()
    {
        this.m_SelectModeMenu.SetActive(true);
    }

    public void SelectModeMenuHide()
    {
        this.m_SelectModeMenu.SetActive(false);
    }
}
