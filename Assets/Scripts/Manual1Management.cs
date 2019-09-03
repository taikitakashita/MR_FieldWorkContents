using HoloToolkit.Unity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manual1Management : Singleton<Manual1Management>
{

    [SerializeField]
    private GameObject m_manual1Panel;

    [SerializeField]
    private TextMesh m_manual1Label;

    [SerializeField]
    private TextMeshPro m_manual1Text;

    [SerializeField]
    private GameObject m_backButton;

    [SerializeField]
    private GameObject m_nextButton;

    [SerializeField]
    private GameObject m_endButton;

    [SerializeField]
    private TextMeshProUGUI m_resultText;

    private int m_manualNum;

    private int m_manualTotalNum;

    List<ManualEntity> m_manualEntityList;

    ManualHistoryEntity m_manualResultEntity;
    List<ManualHistoryEntity> m_manualResultEntityList;

    // Start is called before the first frame update
    void Start()
    {
        Manual1PanelHide();
        m_manualResultEntityList = new List<ManualHistoryEntity>();
    }

    public void Manual1PanelShow()
    {
        this.m_manual1Panel.SetActive(true);
    }

    public void Manual1PanelShowDefault()
    {
        m_manualNum = 0;
        StartCoroutine(ManualFunctionRun.Instance.CallLoadManualFunctions("1"));
        NextButtonShow();
        EndButtonHide();
        BackButtonHide();
    }

    public void Manual1PanelHide()
    {
        this.m_manual1Panel.SetActive(false);
    }

    public void BackButtonShow()
    {
        this.m_backButton.SetActive(true);
    }

    public void BackButtonHide()
    {
        this.m_backButton.SetActive(false);
    }

    public void NextButtonShow()
    {
        this.m_nextButton.SetActive(true);
    }

    public void NextButtonHide()
    {
        this.m_nextButton.SetActive(false);
    }

    public void EndButtonShow()
    {
        this.m_endButton.SetActive(true);
    }

    public void EndButtonHide()
    {
        this.m_endButton.SetActive(false);
    }

    public void SetManual(List<ManualEntity> manualEntityList)
    {
        m_manualTotalNum = manualEntityList.Count;
        m_manualEntityList = manualEntityList;
        m_manual1Label.text = string.Format("点検手順 " + (m_manualNum + 1) + "／" + m_manualTotalNum);
        m_manual1Text.text = string.Format(m_manualEntityList[m_manualNum].Data);
        this.m_manual1Panel.SetActive(true);
    }

    public void ManualNextPage()
    {
        BackButtonShow();
        m_manualResultEntity = new ManualHistoryEntity();
        m_manualResultEntity.ManualID = "1";
        m_manualResultEntity.ManualStep = (m_manualNum + 1).ToString();
        m_manualResultEntity.Data = m_resultText.text;
        m_manualResultEntityList.Add(m_manualResultEntity);
        Debug.Log("手順" + m_manualResultEntity.ManualStep + "の結果：" + m_manualResultEntityList[m_manualNum].Data);

        if (m_manualNum < (m_manualTotalNum - 1))
        {
            m_manualNum = m_manualNum + 1;
            m_manual1Label.text = string.Format("点検手順 " + (m_manualNum + 1) + "／" + m_manualTotalNum);
            m_manual1Text.text = string.Format(m_manualEntityList[m_manualNum].Data);
            m_resultText.text = string.Format("異常無し");
        }
        else
        {
            m_manualNum = m_manualNum + 1;
            m_manual1Label.text = string.Format("点検手順 " + (m_manualTotalNum) + "／" + m_manualTotalNum);
            m_manual1Text.text = string.Format("作業は完了しました。\nお疲れ様でした。");
            m_resultText.text = string.Format("ー");
            NextButtonHide();
            EndButtonShow();
        }

    }

    public void ManualBackPage()
    {
        if(m_manualNum > 0) {
            m_manualNum = m_manualNum - 1;
            m_manual1Label.text = string.Format("点検手順 " + (m_manualNum + 1) + "／" + m_manualTotalNum);
            m_manual1Text.text = string.Format(m_manualEntityList[m_manualNum].Data);
            NextButtonShow();
            EndButtonHide();

            if (m_manualNum == 0)
            {
                BackButtonHide();
            }
        }
    }

    public void ManualCompleted()
    {
        string manualResultJson = JsonConvert.SerializeObject(m_manualResultEntityList);
        Debug.Log(manualResultJson);
        StartCoroutine(ManualFunctionRun.Instance.CallSaveManualHistoryFunctions(manualResultJson));
    }

    public void WriteManualPanelText(string text)
    {
        m_manual1Text.text = string.Format(text);
    }
}
