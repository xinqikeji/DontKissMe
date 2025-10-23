using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class SizeChanger
{
    [SerializeField] private float _timeChangeSize = 1;
    [SerializeField] private SkinnedMeshRenderer _body;
    [SerializeField] private SkinnedMeshRenderer _jeance;
    [SerializeField] private SkinnedMeshRenderer _sneaker;
    [SerializeField] private SkinnedMeshRenderer _shirt;
    [SerializeField] private float _scaleUp = 0.1f;
    [SerializeField] private float _scaleRight = 0.2f;
    [SerializeField] private AnimationCurve _animationCurveY;
    [SerializeField] private AnimationCurve _animationCurveX;
    [SerializeField] private AnimationCurve _BlendShapeAmplityde;
    [SerializeField] private float _maxBledShape = 190;
    [SerializeField] private float _minBlendShape = -15;

    private Transform _transform;
    private Vector3 _startScale;
    private Sequence _sequence;

    private float _currentSize = 0;

    public void Init(Transform transform)
    {
        _transform = transform;
        _startScale = transform.lossyScale;
    }

    public void Change(float size)
    {
        float newSize = Mathf.Clamp01(size) * (_maxBledShape + Mathf.Abs(_minBlendShape));
        _currentSize = Mathf.Clamp(_currentSize, 0, 100);

        if (_sequence != null)
            _sequence.Kill();
        _sequence = DOTween.Sequence();
        _sequence
            .Insert(0, _transform.DOScaleY(_startScale.y + _scaleUp, _timeChangeSize).SetEase(_animationCurveY))
            .Insert(0, _transform.DOScaleX(_startScale.x + _scaleRight, _timeChangeSize).SetEase(_animationCurveX))
            .Insert(0, DOTween.To(SetBlentValue, _currentSize, newSize, _timeChangeSize).SetEase(_BlendShapeAmplityde));
    }

    private void SetBlentValue(float currentSize)
    {
        _currentSize = currentSize;

        _body.SetBlendShapeWeight((int)_body.GetBlendShapeWeight(1), currentSize + _minBlendShape);
        _jeance.SetBlendShapeWeight((int)_jeance.GetBlendShapeWeight(1), currentSize + _minBlendShape);
        _sneaker.SetBlendShapeWeight((int)_sneaker.GetBlendShapeWeight(1), currentSize + _minBlendShape);
        _shirt.SetBlendShapeWeight((int)_shirt.GetBlendShapeWeight(1), currentSize + _minBlendShape);
    }
}