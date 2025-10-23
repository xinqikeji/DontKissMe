using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class IntegrationMetric
{
    public static readonly IntegrationMetric Instance = new IntegrationMetric();
    private const string SessionCountName = "sessionCount";
    private const string _regDay = "regDay";
    private const string ProfileId = "ProfileId";
    private const int ProfileIdLength = 10;
    private string _profileId;
    private int _sessionCount;

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());

        AppMetrica.Instance.ReportEvent("game_start", count);
    }

    public void OnLevelStart(int levelNumber, string levelName, int levelCount, int levelLoop, bool levelRandom)
    {
        Dictionary<string, object> levelProperty = new Dictionary<string, object>()
        {
            { "level_number", levelNumber },
            {"level_name", levelName},
            {"level_count", levelCount },
            {"level_loop", levelLoop },
            {"level_random",levelRandom }

        };
        AppMetrica.Instance.ReportEvent("level_start", levelProperty);
        AppMetrica.Instance.SendEventsBuffer();
        ;
    }

    public void OnLevelComplete(int levelNumber, string levelName, int levelCount, int levelLoop, bool levelRandom, int time, int progress, string result)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object>()
        {
            { "level_number", levelNumber },
            {"level_name", levelName},
            {"level_count", levelCount },
            {"level_loop", levelLoop },
            {"level_random",levelRandom },
            { "time", time },
            { "result", result },
            { "progress", Mathf.Clamp(progress, 0, 100) }
        };

        AppMetrica.Instance.ReportEvent("level_finish", userInfo);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void OnLevelFail(int levelFailTime, int levelIndex, string lostCouse)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime }, { "reason", lostCouse } };

        AppMetrica.Instance.ReportEvent("fail", userInfo);
    }

    public void OnRestartLevel(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);

        AppMetrica.Instance.ReportEvent("restart", levelProperty);
    }

    public void OnSoftCurrencySpend(string type, string name, int currencySpend)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "type", type }, { "name", name }, { "amount", currencySpend } };

        AppMetrica.Instance.ReportEvent("soft_spent", userInfo);
    }

    public void OnAbiltyUsed(string name)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "name", name } };

        AppMetrica.Instance.ReportEvent("ability_used", userInfo);
    }

    public void SetUserProperty()
    {
        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("session_count").WithDelta(_sessionCount));
        ReportUserProfile(userProfile);
        if (PlayerPrefs.HasKey(_regDay) == false)
        {
            RegDay();
        }
        else
        {
            int firstDay = PlayerPrefs.GetInt(_regDay);
            int daysInGame = DateTime.Now.Day - firstDay;

            DaysInGame(daysInGame);
        }
    }

    private void RegDay()
    {
        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomString("reg_day").WithValue(DateTime.Now.ToString()));
        ReportUserProfile(userProfile);

        PlayerPrefs.SetInt(_regDay, DateTime.Now.Day);
    }

    private void DaysInGame(int daysInGame)
    {
        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
        ReportUserProfile(userProfile);
    }

    private async void ReportUserProfile(YandexAppMetricaUserProfile userProfile)
    {
        var profile = GetProfileId();
        await Task.Run(() => AppMetrica.Instance.SetUserProfileID(profile));
        await Task.Run(() => AppMetrica.Instance.ReportUserProfile(userProfile));
    }

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(ProfileId))
        {
            _profileId = PlayerPrefs.GetString(ProfileId);
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(ProfileId, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        var random = new System.Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }

    private Dictionary<string, object> CreateLevelProperty(int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(SessionCountName))
        {
            count = PlayerPrefs.GetInt(SessionCountName);
            count++;
        }

        PlayerPrefs.SetInt(SessionCountName, count);
        _sessionCount = count;

        return count;
    }
}