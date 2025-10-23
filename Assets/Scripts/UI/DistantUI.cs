using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.UI;

public class DistantUI : MonoBehaviour
{
    [SerializeField] private Image _slider;
    
    private SplineFollower _splineFollower;
    public float _percentagePassing => _slider.fillAmount;

    private void Start()
    {
        _splineFollower = FindObjectOfType<SplineFollower>();
    }

    private void Update() => _slider.fillAmount = (float)_splineFollower.GetPercent();
}