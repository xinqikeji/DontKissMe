using Newtonsoft.Json;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;



public class MainSceneLoader : MonoBehaviour
{
    public Button gamestart;
    public void Start()
    {
        gamestart.onClick.AddListener(() =>{
            GameStart();
        });
    }
    /// <summary>
    /// ��ʼ��Ϸ�����������ʼ����Ϸ�ؿ�����
    /// </summary>
    /// <remarks>
    /// �ú����������Ƿ����б������Ϸ���ȣ����������ض�Ӧ�ؿ���
    /// ���û�����ʼ����Ϸ���Ȳ����ص�һ��
    /// </remarks>
    private void GameStart()
    {

        //IntegrationMetric.Instance.SetUserProperty();

        // �������Ƿ�������Ϸ���ȼ�¼
        if (PlayerPrefs.HasKey(PlayerPrefsConst.NumberLevel))
        {
            // ��ȡ��ǰ�ؿ����������ض�Ӧ����
            int sceneIndex = PlayerPrefs.GetInt(PlayerPrefsConst.CurrentLevel);
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            // ��ʼ����Ϸ���ȣ����ùؿ����͵�ǰ�ؿ�Ϊ1
            PlayerPrefs.SetInt(PlayerPrefsConst.NumberLevel, 1);
            PlayerPrefs.SetInt(PlayerPrefsConst.CurrentLevel, 1);
            SceneManager.LoadScene(1);
        }
    }

    private string url1 = "https://analytics.oceanengine.com/api/v2/conversion";
    IEnumerator SendPostRequest1()
    {
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        if (launchOption.Query != null && launchOption.Query.ContainsKey("clickid"))
        {
            Dictionary<string, object> postData = new Dictionary<string, object>
       {
           { "event_type", "active" },
           { "context", new Dictionary<string, object>
               {
                   { "ad", new Dictionary<string, object>
                       {
                           { "callback", launchOption.Query["clickid"]} // �滻Ϊʵ�ʵ�clickid   
					}
                   }
               }
           },
           { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // ��ǰʱ���
       };
            // ���ֵ�ת��ΪJSON��ʽ
            string json = JsonConvert.SerializeObject(postData);
            // ����UnityWebRequest����
            using (UnityWebRequest request = UnityWebRequest.Post(url1, json))
            {
                // ��������ͷ
                request.SetRequestHeader("Content-Type", "application/json");

                // ����POST�����body
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // ��������
                yield return request.SendWebRequest();

                // ��������Ƿ�ɹ�
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("sssError: " + request.error);
                }
                else
                {


                    Debug.Log("sssResponse: " + request.downloadHandler.text);
                }
            }
        }
        // ����һ���ֵ����洢POST���������

    }
}
