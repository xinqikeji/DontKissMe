using Assets.Scripts;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private DistantUI _distantUI;

    [SerializeField] private KissShower _kissShower;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private UiPassingRoad _uiPassingRoad;
    [SerializeField] private TapToStart _tapToStart;

    [SerializeField] private float _timeBeforeShowWinPanel;

    private Starter _starter;
    private Finisher _finisher;
    private EnemyFallowCase _enemyCase;

    private float _waitAferShowKiss = 1.6f;
    private bool _isLoose = false;
    private bool _isWin = false;

    private void OnEnable()
    {
        _starter = FindObjectOfType<Starter>();
        _finisher = FindObjectOfType<Finisher>();
        _enemyCase = FindObjectOfType<EnemyFallowCase>();

        _enemyCase.EnemyAdded += Subscribe;
        _enemyCase.EnemyRemoved += UnSubscribe;
        _starter.LevelStart += _uiPassingRoad.Show;
        _finisher.FinishLevel += _uiPassingRoad.Hide;
        _finisher.FinishLevel += WaitAndShow;
        _starter.LevelStart += _tapToStart.Hide;
    }
    private void OnDisable()
    {
        _enemyCase.EnemyAdded -= Subscribe;
        _enemyCase.EnemyRemoved -= UnSubscribe;
        _starter.LevelStart -= _uiPassingRoad.Show;
        _finisher.FinishLevel -= _uiPassingRoad.Hide;
        _finisher.FinishLevel -= WaitAndShow;
        _starter.LevelStart -= _tapToStart.Hide;
    }

    private void WaitAndShow() => StartCoroutine(Wait(_timeBeforeShowWinPanel, _winPanel.Show));

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        if (_isLoose == false)
        {
            action();
            _isWin = true;
        }
    }

    private void Subscribe(Enemy enemy) => enemy.EnemyMover.ImageFiller.ImageFilled += ShowKiss;

    private void UnSubscribe(Enemy enemy) => enemy.EnemyMover.ImageFiller.ImageFilled -= ShowKiss;

    public void ShowKiss()
    {
        if (_isWin == false && _isLoose == false)
        {
            _isLoose = true;
            StartCoroutine(CoroutineHelper.WaitTimeAndUse(_waitAferShowKiss, () => _kissShower.ShowKiss()
            .OnComplete(() => EnableKiss()), () => _uiPassingRoad.Hide()));
            IntegrationMetric.Instance.OnLevelComplete(PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel),
                                                       PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel) + "_DKM",
                                                       PlayerPrefs.GetInt(PlayerPrefsConst.LevelCount),
                                                       PlayerPrefs.GetInt(PlayerPrefsConst.LevelLoop),
                                                       false,
                                                       (int)Mathf.Abs(_sceneLoader.StartTime - Time.time),
                                                       (int)(100 * _distantUI._percentagePassing),
                                                       "lose");
        }
    }

    public void EnableKiss() => _losePanel.ShowAndStopTime();
}