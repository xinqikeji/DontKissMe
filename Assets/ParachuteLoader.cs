using DG.Tweening;
using UnityEngine;

public class ParachuteLoader : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _nextParachutePosition;
    [SerializeField] private float _time;

    private void OnEnable() => _enemy.Died += Loader;

    private void OnDisable() => _enemy.Died -= Loader;

    private void Loader(Enemy arg0)
    {
        transform.DOLocalMove(_nextParachutePosition.localPosition, _time);
        transform.DOLocalRotate(_nextParachutePosition.localRotation.eulerAngles, _time);
    }
}
