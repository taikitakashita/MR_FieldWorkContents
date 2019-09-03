using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual1HistoryManagement : Singleton<Manual1HistoryManagement>
{
    [SerializeField]
    private GameObject m_manual1HistoryPanel;

    [SerializeField]
    private TextMesh m_manual1Num1;

    [SerializeField]
    private TextMesh m_manual1Num2;

    [SerializeField]
    private TextMesh m_manual1Num3;

    [SerializeField]
    private TextMesh m_manual1Num4;

    [SerializeField]
    private TextMesh m_manual1Num5;

    // Start is called before the first frame update
    void Start()
    {
        Manual1HistoryPanelHide();
    }

    public void Manual1HistoryPanelShow()
    {
        StartCoroutine(ManualFunctionRun.Instance.CallLoadManualHistoryFunctions("1"));
    }

    public void Manual1HistoryPanelHide()
    {
        this.m_manual1HistoryPanel.SetActive(false);
    }

    public void SetManualHistory(List<ManualHistoryEntity> manualHistoryEntityList)
    {
        m_manual1Num1.text = string.Format(manualHistoryEntityList[0].Data);
        m_manual1Num2.text = string.Format(manualHistoryEntityList[1].Data);
        m_manual1Num3.text = string.Format(manualHistoryEntityList[2].Data);
        m_manual1Num4.text = string.Format(manualHistoryEntityList[3].Data);
        m_manual1Num5.text = string.Format(manualHistoryEntityList[4].Data);
        this.m_manual1HistoryPanel.SetActive(true);
    }
}
