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
    /// �����ݽṹ�������л������浽PlayerPrefs
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    /// <param name="key">�洢����</param>
    /// <param name="data">Ҫ��������ݶ���</param>
    public void SaveData<T>(string key, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        SaveString(key, jsonData);
        
    }

    /// <summary>
    /// ��PlayerPrefs�ж�ȡ�������л�Ϊ���ݽṹ����
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    /// <param name="key">�洢����</param>
    /// <param name="defaultData">Ĭ�����ݶ���</param>
    /// <returns>��ȡ�����ݶ����Ĭ�϶���</returns>
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
    /// �洢��������
    /// </summary>
    /// <param name="key">����</param>
    /// <param name="value">Ҫ�洢��ֵ</param>
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save(); // �������浽����
    }

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    /// <param name="key">����</param>
    /// <param name="defaultValue">Ĭ��ֵ</param>
    /// <returns>�洢��ֵ��Ĭ��ֵ</returns>
    public int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    /// <summary>
    /// �洢����������
    /// </summary>
    /// <param name="key">����</param>
    /// <param name="value">Ҫ�洢��ֵ</param>
    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ��ȡ����������
    /// </summary>
    /// <param name="key">����</param>
    /// <param name="defaultValue">Ĭ��ֵ</param>
    /// <returns>�洢��ֵ��Ĭ��ֵ</returns>
    public float LoadFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    /// <summary>
    /// �洢�ַ�������
    /// </summary>
    /// <param name="key">����</param>
    /// <param name="value">Ҫ�洢��ֵ</param>
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ��ȡ�ַ�������
    /// </summary>
    /// <param name="key">����</param>
    /// <param name="defaultValue">Ĭ��ֵ</param>
    /// <returns>�洢��ֵ��Ĭ��ֵ</returns>
    public string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    /// <summary>
    /// ����Ƿ����ָ������������
    /// </summary>
    /// <param name="key">����</param>
    /// <returns>�Ƿ����</returns>
    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    /// <summary>
    /// ɾ��ָ������������
    /// </summary>
    /// <param name="key">����</param>
    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ɾ������PlayerPrefs����
    /// </summary>
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    /// <summary>
    /// �ֶ����������޸�
    /// </summary>
    public void Save()
    {
        PlayerPrefs.Save();
    }
}
