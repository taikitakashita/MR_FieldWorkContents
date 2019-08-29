using HoloToolkit.Unity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
    private readonly string ManualHistoryFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger5";

    /// <summary>
    /// マニュアルの内容を取得する処理を呼び出す
    /// </summary>
    public IEnumerator CallManualFunctions(string bodyString)
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
    public IEnumerator CallManualHistoryFunctions(string bodyString)
    {
        Debug.Log("CallManualHistoryFunctions Start:" + bodyString);
        List<ManualHistoryEntity> manualHistoryEntityList = null;

        UnityWebRequest request = new UnityWebRequest(ManualHistoryFunctionsEndpoint + "?ManualID=" + bodyString, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        string Response = request.downloadHandler.text;
        manualHistoryEntityList = JsonConvert.DeserializeObject<List<ManualHistoryEntity>>(Response);
        Debug.Log("CallManualHistoryFunctions Status Code: " + request.responseCode);
        Debug.Log("Load ManualHistory: " + manualHistoryEntityList[0].Data);
        Debug.Log("CallManualHistoryFunctions End");
        Manual1HistoryManagement.Instance.SetManualHistory(manualHistoryEntityList);
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
