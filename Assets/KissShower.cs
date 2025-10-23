using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KissShower : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Image> _kiss;
    [Range(0, 1)]
    [SerializeField] private float _nextAlpha;
    [SerializeField] private float _timeToShow;

    private const int MaxAlpha = 1;
    public Tween ShowKiss()
    {
        var color = _image.color;
        Show();
        return _image.DOColor(new Color(color.r, color.g, color.b, _nextAlpha), _timeToShow);
    }

    public void Show()
    {
        var timeToOne = _timeToShow / _kiss.Count;

        for (int i = 0; i < _kiss.Count; i++)
        {
            var color = _kiss[i].color;
            _kiss[i].DOColor(new Color(color.r, color.g, color.b, MaxAlpha), timeToOne)
                .SetDelay(timeToOne * i);
        }
    }

    public void Hide() => _kiss.ForEach(kiss => kiss.gameObject.SetActive(false));
}
