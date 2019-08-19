using HoloToolkit.Unity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.WSA;

public class MachineInformationFunctionRun : Singleton<MachineInformationFunctionRun>
{

    /// <summary>
    /// AzureFunctionのエンドポイントのURL
    /// </summary>
    private readonly string LoadFunctionsEndpoint = "https://mrtestfunctions.azurewebsites.net/api/HttpTrigger3";

    /// <summary>
    /// Identifierの検索を行う処理を呼び出す
    /// </summary>
    public IEnumerator CallLoadFunctions(string bodyString)
    {
        Debug.Log("CallLoadFunctions Start:" + bodyString);
        MachineInformationEntity machineinformationentity = null;

        UnityWebRequest request = new UnityWebRequest(LoadFunctionsEndpoint + "?InfoKey=" + bodyString, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        string Response = request.downloadHandler.text;
        machineinformationentity = JsonConvert.DeserializeObject<MachineInformationEntity>(Response);
        Debug.Log("CallLoadFunctions Status Code: " + request.responseCode);
        Debug.Log("Load MachineInformation: " + Response);
        Debug.Log("CallLoadFunctions End");
        Information1Management.Instance.SetMachineInformation(machineinformationentity);
    }
}

/// <summary>
/// MachineInformationを格納するクラス
/// </summary>
[System.Serializable]
public class MachineInformationEntity
{
    public string Name;
    public string Category;
    public string CurrentStatus;
    public string StartDate;
}
