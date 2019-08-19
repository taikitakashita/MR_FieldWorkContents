using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsPositionMove : MonoBehaviour, IInputClickHandler
{

    private GameObject m_labModel;
    private GameObject m_noticeMSG;
    private TextMesh m_noticeMSGLabel;

    void Start()
    {
        m_labModel = CloudAnchorManagement.Instance.GetLabModel;
        m_noticeMSG = CloudAnchorManagement.Instance.GetNoticeMSG;
        m_noticeMSGLabel = CloudAnchorManagement.Instance.GetNoticeMSGLabel;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Vector3 beforePos = m_labModel.transform.position;
        Vector3 afterPos = beforePos;

        afterPos.x = this.gameObject.transform.position.x;
        afterPos.z = this.gameObject.transform.position.z;

        m_labModel.transform.position = afterPos;
        m_labModel.SetActive(true);

        m_noticeMSGLabel.text = string.Format("位置を保存する場合は「OK」" + "\n" + "再度調整する場合は「RETRY」");
        Destroy(this.gameObject);
    }
}
