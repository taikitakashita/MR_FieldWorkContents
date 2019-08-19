using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using Microsoft.Azure.SpatialAnchors;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.WSA;

public class CloudAnchorManagement : Singleton<CloudAnchorManagement>
{
    [SerializeField]
    private GameObject m_labModel;

    [SerializeField]
    private GameObject m_markerPrefab;

    [SerializeField]
    private GameObject m_noticeMSG;

    [SerializeField]
    private TextMesh m_noticeMSGLabel;

    IdentifierEntity identifierEntity;

    /// <summary>
    /// AzureSpatialAnchorsリソースのアカウントIDを設定
    /// </summary>
    protected string SpatialAnchorsAccountId = "9961fda6-546e-45eb-8530-9aa208ec939d";

    /// <summary>
    /// AzureSpatialAnchorsリソースの主キーを設定
    /// </summary>
    protected string SpatialAnchorsAccountKey = "v79Od3dM1jQ+L9WmvbunQUwI+eUVWcDbLHkpjNKNrvw=";

    protected CloudSpatialAnchorSession cloudSpatialAnchorSession;
    protected CloudSpatialAnchor currentCloudAnchor;

    /// <summary>
    /// アンカーを保存する準備ができているかどうかを示す値。値が1より大きい場合はアンカーを保存可能
    /// </summary>
    protected float recommendedForCreate = 0;

    void Start()
    {
        identifierEntity = new IdentifierEntity();
        m_noticeMSG.SetActive(false);
        m_labModel.SetActive(false);
    }

    public void SettingContentsPosition()
    {
        Debug.Log("Selected SettingContentsPosition.");
        Instantiate(m_markerPrefab);
        m_noticeMSG.SetActive(true);
        m_labModel.SetActive(false);
        m_noticeMSGLabel.text = string.Format("床の上でマーカーをタップして" + "\n" + "原点位置を設定してください");
    }

    public void SaveContentsPosition()
    {
        Debug.Log("Selected SaveContentsPosition.");
        m_noticeMSGLabel.text = string.Format("アンカー情報を保存しています。");

        /*
         * IdentifierとAnchorsをAzureSpatialAnchorsに登録
         */
        //CloudSpatialAnchorセッションを初期化するための処理を実行
        ASAInitializeSession();
        Debug.Log("ASA Info: 新しいアンカーを生成します。");
        currentCloudAnchor = null;
        // CloudSpatialAnchorを生成する
        currentCloudAnchor = new CloudSpatialAnchor();
        // CloudSpatialAnchorをクラウドに保存する
        m_labModel.AddComponent<WorldAnchor>();
        Debug.Log("ASA Info: ローカルアンカーを生成しました");
        WorldAnchor worldAnchor = m_labModel.GetComponent<WorldAnchor>();
        if (worldAnchor == null)
        {
            throw new Exception("ASA Error: ローカルアンカーのポインタを取得できませんでした。");
        }
        currentCloudAnchor.LocalAnchor = worldAnchor.GetNativeSpatialAnchorPtr();

        var context = SynchronizationContext.Current;
        Task.Run(async () =>
        {
            // アンカーを保存する周辺の環境情報を取得するまで待つ
            while (recommendedForCreate < 1.0F)
            {
                await Task.Delay(330);
            }
            bool success = false;
            try
            {
                await cloudSpatialAnchorSession.CreateAnchorAsync(currentCloudAnchor);
                success = currentCloudAnchor != null;

                if (success)
                {
                    // 検索するアンカーを記録
                    identifierEntity.Identifier = currentCloudAnchor.Identifier;
                    Debug.Log("ASA Info: Azure Spatial Anchorsへのアンカーを保存しました! Identifier: " + identifierEntity.Identifier);
                    ASAResetSession();
                }
                else
                {
                    Debug.LogError("ASA Error:保存に失敗しましたが、例外はスローされませんでした");
                }

                /*
                 * ObjectIDとIdentifierをFunctions経由でTableStorageに登録(既に同一ObjectIDのものが存在する場合は更新)
                 */
                identifierEntity.ObjectID = m_labModel.GetInstanceID().ToString();
                string jsonstr = JsonUtility.ToJson(identifierEntity);
                context.Post(_ => {
                    StartCoroutine(AzureSpatialAnchorFunctionsRun.Instance.CallSaveFunctions(jsonstr));
                    DestroyImmediate(m_labModel.GetComponent<WorldAnchor>());
                }, null);
            }
            catch (Exception ex)
            {
                Debug.LogError("ASA Error: " + ex.Message);
            }
        });
    }

    public void LoadContentsPosition()
    {
        Debug.Log("Selected LoadContentsPosition.");
        m_noticeMSGLabel.text = string.Format("アンカー情報を読み込んでいます。");

        /*
         * ObjectIDをキーとして、Functions経由でTableStorageからIdentifierを検索
         */
        identifierEntity.ObjectID = m_labModel.GetInstanceID().ToString();
        identifierEntity.Identifier = "";
        string jsonstr = JsonUtility.ToJson(identifierEntity);
        StartCoroutine(AzureSpatialAnchorFunctionsRun.Instance.CallLoadFunctions(jsonstr));
    }

    public GameObject GetLabModel
    {
        get { return m_labModel; }
    }

    public GameObject GetNoticeMSG
    {
        get { return m_noticeMSG; }
    }

    public TextMesh GetNoticeMSGLabel
    {
        get { return m_noticeMSGLabel; }
    }

    public IdentifierEntity IdentifierEntityProperty
    {
        get { return identifierEntity; }
        set { identifierEntity = value; }
    }

    /// <summary>
    /// 新しいCloudSpatialAnchorセッションを初期化
    /// </summary>
    void ASAInitializeSession()
    {
        Debug.Log("ASA Info: Initializing a CloudSpatialAnchorSession.");

        if (string.IsNullOrEmpty(SpatialAnchorsAccountId))
        {
            Debug.LogError("アカウントIDが設定されていません。");
            return;
        }

        if (string.IsNullOrEmpty(SpatialAnchorsAccountKey))
        {
            Debug.LogError("アカウントキーが設定されていません。");
            return;
        }

        cloudSpatialAnchorSession = new CloudSpatialAnchorSession();

        cloudSpatialAnchorSession.Configuration.AccountId = SpatialAnchorsAccountId.Trim();
        cloudSpatialAnchorSession.Configuration.AccountKey = SpatialAnchorsAccountKey.Trim();

        cloudSpatialAnchorSession.LogLevel = SessionLogLevel.All;

        cloudSpatialAnchorSession.Error += CloudSpatialAnchorSession_Error;
        cloudSpatialAnchorSession.OnLogDebug += CloudSpatialAnchorSession_OnLogDebug;
        cloudSpatialAnchorSession.SessionUpdated += CloudSpatialAnchorSession_SessionUpdated;
        cloudSpatialAnchorSession.AnchorLocated += CloudSpatialAnchorSession_AnchorLocated;
        cloudSpatialAnchorSession.LocateAnchorsCompleted += CloudSpatialAnchorSession_LocateAnchorsCompleted;

        cloudSpatialAnchorSession.Start();
        Debug.Log("ASA Info: セッションは初期化されました。");
    }

    private void CloudSpatialAnchorSession_Error(object sender, SessionErrorEventArgs args)
    {
        Debug.LogError("ASA Error: " + args.ErrorMessage);
    }

    private void CloudSpatialAnchorSession_OnLogDebug(object sender, OnLogDebugEventArgs args)
    {
        //Debug.Log("ASA Log: " + args.Message);
        //System.Diagnostics.Debug.WriteLine("ASA Log: " + args.Message);
    }

    private void CloudSpatialAnchorSession_SessionUpdated(object sender, SessionUpdatedEventArgs args)
    {
        //Debug.Log("ASA Log: recommendedForCreate: " + args.Status.RecommendedForCreateProgress);
        recommendedForCreate = args.Status.RecommendedForCreateProgress;
    }

    private void CloudSpatialAnchorSession_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        UnityEngine.WSA.Application.InvokeOnAppThread(() =>
        {
            switch (args.Status)
            {
                case LocateAnchorStatus.Located:
                    Debug.Log("ASA Info: アンカーが見つかりました! Identifier: " + args.Identifier);
                    // CloudSpatialAnchorからWorldAnchorを取得し、それを使ってモデルを配置
                    m_labModel.AddComponent<WorldAnchor>();
                    m_labModel.GetComponent<WorldAnchor>().SetNativeSpatialAnchorPtr(args.Anchor.LocalAnchor);
                    break;
                case LocateAnchorStatus.AlreadyTracked:
                    Debug.Log("ASA Info: アンカーは既に読み取られています。 Identifier: " + args.Identifier);
                    break;
                case LocateAnchorStatus.NotLocated:
                    Debug.Log("ASA Info: アンカーが見つかりません。 Identifier: " + args.Identifier);
                    break;
                case LocateAnchorStatus.NotLocatedAnchorDoesNotExist:
                    Debug.LogError("ASA Error: アンカーが見つかりません - 削除されたか、照会された識別子が正しくありません。 Identifier: " + args.Identifier);
                    break;
            }
        }, false);

    }

    private void CloudSpatialAnchorSession_LocateAnchorsCompleted(object sender, LocateAnchorsCompletedEventArgs args)
    {
        Debug.Log("ASA Info: アンカーの検索が完了しました。 Watcher identifier: " + args.Watcher.Identifier);
        args.Watcher.Stop();
        ASAResetSession();
        UnityEngine.WSA.Application.InvokeOnAppThread(() =>
        {
            LabModelShow();
        }, false);
    }

    /// <summary>
    /// オブジェクトをクリーンアップしてCloudSpatialAnchorセッションを停止する処理
    /// </summary>
    public void ASAResetSession()
    {
        Debug.Log("ASA Info: セッションをリセットします。");

        if (cloudSpatialAnchorSession.GetActiveWatchers().Count > 0)
        {
            Debug.LogError("ASA Error: ActiveなWatcherがあるセッションをリセットしようとしています。これは期待された動作ではありません。");
        }

        currentCloudAnchor = null;

        this.cloudSpatialAnchorSession.Reset();

        if (cloudSpatialAnchorSession != null)
        {
            cloudSpatialAnchorSession.Stop();
            cloudSpatialAnchorSession.Dispose();
            Debug.Log("ASA Info: セッションはリセットされました。");
        }
        else
        {
            Debug.LogError("ASA Error: セッションはNullでした。これは期待された動作ではありません。");
        }
    }

    public void LabModelShow()
    {
        this.m_labModel.SetActive(true);
    }

    public void LabModelHide()
    {
        this.m_labModel.SetActive(false);
    }

    public void NoticeMSGShow()
    {
        this.m_noticeMSG.SetActive(true);
    }

    public void NoticeMSGHide()
    {
        this.m_noticeMSG.SetActive(false);
    }

    public void LoadAzureSpatialAnchors()
    {
        // CloudSpatialAnchorセッションを初期化するための処理を実行
        ASAInitializeSession();
        Debug.Log("ASA Info: 保存したアンカーを検索します。");
        // 保存したAnchorを検索するWatcherを生成する
        AnchorLocateCriteria criteria = new AnchorLocateCriteria();
        criteria.Identifiers = new string[] { identifierEntity.Identifier };
        cloudSpatialAnchorSession.CreateWatcher(criteria);
        Debug.Log("ASA Info: Watcherを生成しました。 ActiveなWatcherの数: " + cloudSpatialAnchorSession.GetActiveWatchers().Count);
    }
}

/// <summary>
/// ObjectIDとIdentifierを格納するクラス
/// </summary>
[System.Serializable]
public class IdentifierEntity
{
    public string ObjectID;
    public string Identifier;
}
