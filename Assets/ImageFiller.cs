using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Image _image;
    [SerializeField] private float _timeToFull = 2;
    [SerializeField] private float _scalePersent = 1.2f;
    [SerializeField, Min(0)] private float _time;
    [SerializeField] private AnimationCurve _animationCurveSize;
    [SerializeField] private float _waitAfterParticles = 0.5f;
    [SerializeField] private ParticleSystem[] _particleSystems;
    private float _currentSlise = 0;
    private bool _isDied;

    public bool IsFullFill { get; private set; } = false;

    public event UnityAction ImageFilled;
    public event UnityAction<Enemy> ImageFilledOn;

    private void OnEnable() => _enemy.Died += Hide;

    private void OnDisable() => _enemy.Died -= Hide;
    public void UpdateFilled() => ChangeSlise(Time.deltaTime);

    private void ChangeSlise(float time)
    {
        _currentSlise += time / _timeToFull;
        _currentSlise = Mathf.Clamp01(_currentSlise);
        IsFullFill = _currentSlise >= 1 && IsFullFill == false;
        if (IsFullFill)
            AnimateImage();
        _image.fillAmount = _currentSlise;
    }

    private void AnimateImage()
    {
        var a = _image.transform.DOScale(_scalePersent * _image.transform.localScale, _time)
            .SetEase(_animationCurveSize).OnComplete(() => StartImage());
    }

    public void StartImage()
    {
        if (_isDied == false)
        {
            _image.fillAmount = 0;
            _particleSystems[0].Play();
            ImageFilled.Invoke();
            ImageFilledOn.Invoke(_enemy);
        }
    }

    private void Hide(Enemy enemy)
    {
        _isDied = true;
        _image.gameObject.SetActive(false);
    }

    public void UseParticles(params ParticleSystem[] particles) => particles.ToList().ForEach(x => x.Play());

}
