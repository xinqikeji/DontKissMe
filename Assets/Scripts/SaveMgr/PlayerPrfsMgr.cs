using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrfsMgr 
{
    public static PlayerPrfsMgr Instance => instance;
    private static PlayerPrfsMgr instance = new PlayerPrfsMgr();
    private PlayerPrfsMgr()
    {
        
    }
    /// <summary>
    /// 将数据结构对象序列化并保存到PlayerPrefs
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="key">存储键名</param>
    /// <param name="data">要保存的数据对象</param>
    public void SaveData<T>(string key, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        SaveString(key, jsonData);
        
    }

    /// <summary>
    /// 从PlayerPrefs中读取并反序列化为数据结构对象
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="key">存储键名</param>
    /// <param name="defaultData">默认数据对象</param>
    /// <returns>读取的数据对象或默认对象</returns>
    public T LoadData<T>(string key, T defaultData = default(T))
    {
        if (!HasKey(key))
        {
            return defaultData;
        }

        string jsonData = LoadString(key);
        if (string.IsNullOrEmpty(jsonData))
        {
            return defaultData;
        }

        try
        {
            return JsonUtility.FromJson<T>(jsonData);
        }
        catch
        {
            return defaultData;
        }
    }
    /// <summary>
    /// 存储整型数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="value">要存储的值</param>
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save(); // 立即保存到磁盘
    }

    /// <summary>
    /// 读取整型数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>存储的值或默认值</returns>
    public int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    /// <summary>
    /// 存储浮点型数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="value">要存储的值</param>
    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 读取浮点型数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>存储的值或默认值</returns>
    public float LoadFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    /// <summary>
    /// 存储字符串数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="value">要存储的值</param>
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 读取字符串数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>存储的值或默认值</returns>
    public string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    /// <summary>
    /// 检查是否存在指定键名的数据
    /// </summary>
    /// <param name="key">键名</param>
    /// <returns>是否存在</returns>
    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    /// <summary>
    /// 删除指定键名的数据
    /// </summary>
    /// <param name="key">键名</param>
    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 删除所有PlayerPrefs数据
    /// </summary>
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 手动保存所有修改
    /// </summary>
    public void Save()
    {
        PlayerPrefs.Save();
    }
}
