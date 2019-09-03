using HoloToolkit.Unity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ManualFunctionRun : Singleton<ManualFunctionRun>
{

    /// <summary>
    /// AzureFunctionのエンドポイントのURL
    /// </summary>
    private readonly string ManualFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger4";

    /// <summary>
    /// AzureFunctionのエンドポイントのURL
    /// </summary>
    private readonly string LoadManualHistoryFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger5";

    /// <summary>
    /// AzureFunctionのエンドポイントのURL
    /// </summary>
    private readonly string SaveManualHistoryFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger6";

    /// <summary>
    /// マニュアルの内容を取得する処理を呼び出す
    /// </summary>
    public IEnumerator CallLoadManualFunctions(string bodyString)
    {
        Debug.Log("CallManualFunctions Start:" + bodyString);
        List<ManualEntity> manualEntityList = null;

        UnityWebRequest request = new UnityWebRequest(ManualFunctionsEndpoint + "?ManualID=" + bodyString, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        string Response = request.downloadHandler.text;
        manualEntityList = JsonConvert.DeserializeObject<List<ManualEntity>>(Response);
        Debug.Log("CallManualFunctions Status Code: " + request.responseCode);
        Debug.Log("Load Manual: " + manualEntityList[0].Data);
        Debug.Log("CallManualFunctions End");
        Manual1Management.Instance.SetManual(manualEntityList);
    }

    /// <summary>
    /// マニュアルの前回の結果を取得する処理を呼び出す
    /// </summary>
    public IEnumerator CallLoadManualHistoryFunctions(string bodyString)
    {
        Debug.Log("CallManualHistoryFunctions Start:" + bodyString);
        List<ManualHistoryEntity> manualHistoryEntityList = null;

        UnityWebRequest request = new UnityWebRequest(LoadManualHistoryFunctionsEndpoint + "?ManualID=" + bodyString, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        string Response = request.downloadHandler.text;
        manualHistoryEntityList = JsonConvert.DeserializeObject<List<ManualHistoryEntity>>(Response);
        Debug.Log("CallManualHistoryFunctions Status Code: " + request.responseCode);
        Debug.Log("Load ManualHistory: " + manualHistoryEntityList[0].ManualID + " " + manualHistoryEntityList[0].ManualStep + " " + manualHistoryEntityList[0].Data);
        Debug.Log("CallManualHistoryFunctions End");
        Manual1HistoryManagement.Instance.SetManualHistory(manualHistoryEntityList);
    }

    /// <summary>
    /// 点検結果を登録する処理を呼び出す
    /// </summary>
    public IEnumerator CallSaveManualHistoryFunctions(string bodyJsonString)
    {
        Manual1Management.Instance.WriteManualPanelText("点検結果をサーバに保存しています。");
        Debug.Log("CallSaveManualHistoryFunctions Start:" + bodyJsonString);
        UnityWebRequest request = new UnityWebRequest(SaveManualHistoryFunctionsEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        Debug.Log("CallSaveManualHistoryFunctions Status Code: " + request.responseCode);
        Debug.Log("CallSaveManualHistoryFunctions End");

        if (request.isHttpError || request.isNetworkError)
        {
            Manual1Management.Instance.WriteManualPanelText("ネットワークのエラーが発生しました。\nシステム管理者に連絡してください。");
            yield return new WaitForSeconds(5.0f);
        }
        else
        {
            Manual1Management.Instance.WriteManualPanelText("点検結果の保存が完了しました。\n画面は5秒後に自動で閉じます。");
            yield return new WaitForSeconds(5.0f);

            Manual1Management.Instance.Manual1PanelHide();
            MachineInformation1Management.Instance.Information1ButtonShow();
        }
    }
}

/// <summary>
/// Manualを格納するクラス
/// </summary>
[System.Serializable]
public class ManualEntity
{
    public string Data;
}

/// <summary>
/// ManualHistoryを格納するクラス
/// </summary>
[System.Serializable]
public class ManualHistoryEntity
{
    public string ManualID;
    public string ManualStep;
    public string Data;
}

/// <summary>
/// ManualHistoryを格納するクラス
/// </summary>
[System.Serializable]
public class ManualHistoryEntityList
{
    public List<ManualHistoryEntity> EntityList;
}
