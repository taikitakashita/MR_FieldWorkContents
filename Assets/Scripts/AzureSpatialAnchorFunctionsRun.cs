using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.WSA;

public class AzureSpatialAnchorFunctionsRun : Singleton<AzureSpatialAnchorFunctionsRun>
{

    /// <summary>
    /// AzureFunctionのエンドポイントのURL
    /// </summary>
    private readonly string SaveFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger1";
    private readonly string LoadFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger2";

    /// <summary>
    /// Identifierの登録及び更新を行う処理を呼び出す
    /// </summary>
    public IEnumerator CallSaveFunctions(string bodyJsonString)
    {
        Debug.Log("CallSaveFunctions Start:" + bodyJsonString);
        UnityWebRequest request = new UnityWebRequest(SaveFunctionsEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("CallSaveFunctions Status Code: " + request.responseCode);
        Debug.Log("CallSaveFunctions End");

        CloudAnchorManagement.Instance.LabModelHide();
        CloudAnchorManagement.Instance.NoticeMSGHide();
        ModeManagement.Instance.SelectModeMenuShow();
    }

    /// <summary>
    /// Identifierの検索を行う処理を呼び出す
    /// </summary>
    public IEnumerator CallLoadFunctions(string bodyJsonString)
    {
        Debug.Log("CallLoadFunctions Start:" + bodyJsonString);
        UnityWebRequest request = new UnityWebRequest(LoadFunctionsEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        string Response = request.downloadHandler.text;
        CloudAnchorManagement.Instance.IdentifierEntityProperty.Identifier = Response;
        Debug.Log("CallLoadFunctions Status Code: " + request.responseCode);
        Debug.Log("Load Identifier: " + Response);
        Debug.Log("CallLoadFunctions End");

        CloudAnchorManagement.Instance.LoadAzureSpatialAnchors();
    }
}
