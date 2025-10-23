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
    /// 开始游戏函数，负责初始化游戏关卡加载
    /// </summary>
    /// <remarks>
    /// 该函数检查玩家是否已有保存的游戏进度，如果有则加载对应关卡，
    /// 如果没有则初始化游戏进度并加载第一关
    /// </remarks>
    private void GameStart()
    {

        //IntegrationMetric.Instance.SetUserProperty();

        // 检查玩家是否已有游戏进度记录
        if (PlayerPrefs.HasKey(PlayerPrefsConst.NumberLevel))
        {
            // 获取当前关卡索引并加载对应场景
            int sceneIndex = PlayerPrefs.GetInt(PlayerPrefsConst.CurrentLevel);
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            // 初始化游戏进度，设置关卡数和当前关卡为1
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
                           { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
					}
                   }
               }
           },
           { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
       };
            // 将字典转换为JSON格式
            string json = JsonConvert.SerializeObject(postData);
            // 创建UnityWebRequest对象
            using (UnityWebRequest request = UnityWebRequest.Post(url1, json))
            {
                // 设置请求头
                request.SetRequestHeader("Content-Type", "application/json");

                // 设置POST请求的body
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // 发送请求
                yield return request.SendWebRequest();

                // 检查请求是否成功
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
        // 创建一个字典来存储POST请求的数据

    }
}
