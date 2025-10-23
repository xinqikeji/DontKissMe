using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Assets.Scripts;

public class ItemProgress : MonoBehaviour
{
    private const string PROGRESS = "Progress";
    private const string IS_SHOW = "Is_Show";

    [SerializeField] private float _stepChage;
    [SerializeField] private Image _slider;
    [SerializeField] private Image _noActive;
    [SerializeField] private TMP_Text _precent;
    [SerializeField] private Button _button;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private GameObject _capacity;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private DistantUI _distantUI;

    private float _maxValue = 1f;
    private float _minValue = 0f;
    private float _currentProgress;
    private float _currentPrecent;

    private void Start()
    {
        IntegrationMetric.Instance.OnLevelComplete(PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel),
                                                   PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel) + "_DKM",
                                                   PlayerPrefs.GetInt(PlayerPrefsConst.LevelCount),
                                                   PlayerPrefs.GetInt(PlayerPrefsConst.LevelLoop),
                                                   false,
                                                   (int)Mathf.Abs(_sceneLoader.StartTime - Time.time),
                                                   100,
                                                   "win");
        if (PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel) - 1 == 20)
        {
            PlayerPrefs.SetString(IS_SHOW, "false");
        }

        if (PlayerPrefs.HasKey(IS_SHOW))
        {
            if (PlayerPrefs.GetString(IS_SHOW) == "false")
            {
                _capacity.SetActive(false);
                return;
            }
        }

        _button.gameObject.SetActive(false);
        _currentProgress = PlayerPrefs.GetFloat(PROGRESS);
        _currentPrecent = _currentProgress * 100;
        _precent.text = _currentPrecent.ToString();
        _slider.fillAmount = _currentProgress;

        _sceneLoader.SaveLevel();
        float targetValue = _currentProgress + _stepChage;
        StartCoroutine(ChangedValue(targetValue));
        PlayerPrefs.SetFloat(PROGRESS, targetValue);
    }

    public void SetSlider(Sprite slider, Sprite noActive)
    {
        _slider.sprite = slider;
        _noActive.sprite = noActive;
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(PROGRESS);
    }

    private IEnumerator ChangedValue(float targetValue)
    {
        float speed = .2f;
        targetValue = Mathf.Clamp01(targetValue);

        while (_currentProgress < targetValue)
        {
            _currentProgress = Mathf.MoveTowards(_currentProgress, targetValue, speed * Time.deltaTime);
            _slider.fillAmount = _currentProgress;

            _currentPrecent = (_currentProgress * 100f);
            int precent = (int)_currentPrecent;
            _precent.text = precent.ToString();

            yield return null;
        }

        _button.gameObject.SetActive(true);

        if (_currentProgress == _maxValue)
        {
            _precent.gameObject.SetActive(false);
            float step = .2f;
            Vector3 targetScale = new Vector2(_slider.transform.localScale.x + step, _slider.transform.localScale.y + step);
            _effect.Play();
            _slider.transform.DOScale(targetScale, .4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            _noActive.transform.DOScale(targetScale, .4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            _currentProgress = _minValue;
        }
    }
}