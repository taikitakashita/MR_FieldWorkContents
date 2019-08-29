using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInformation1Management : Singleton<MachineInformation1Management>
{
    [SerializeField]
    private GameObject m_information1Button;

    [SerializeField]
    private GameObject m_information1Panel;

    [SerializeField]
    private TextMesh m_machineName;

    [SerializeField]
    private TextMesh m_machineCategory;

    [SerializeField]
    private TextMesh m_machineStatus;

    [SerializeField]
    private TextMesh m_machineStartDate;


    void Start()
    {
        Information1PanelHide();
    }

    public void Information1ButtonShow()
    {
        this.m_information1Button.SetActive(true);
    }

    public void Information1ButtonHide()
    {
        this.m_information1Button.SetActive(false);
    }

    public void Information1PanelShow()
    {
        StartCoroutine(MachineInformationFunctionRun.Instance.CallLoadFunctions("Information1"));
        this.m_information1Panel.SetActive(true);
    }

    public void Information1PanelHide()
    {
        this.m_information1Panel.SetActive(false);
    }

    public void SetMachineInformation(MachineInformationEntity entity)
    {
        m_machineName.text = string.Format(entity.Name);
        m_machineCategory.text = string.Format(entity.Category);
        m_machineStatus.text = string.Format(entity.CurrentStatus);
        m_machineStartDate.text = string.Format(entity.StartDate);
    }
}
