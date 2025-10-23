using Assets.Scripts;
using DG.Tweening;
using UnityEngine;

public class LipsMover : MonoBehaviour
{
    [SerializeField] private float _time = 2;
    [SerializeField] private float _scale = 2;
    [SerializeField] private float _timeToEnableMesh;

    private CameraTranslater _cameraTransiter;

    public void Init(CameraTranslater cameraTranslater) => _cameraTransiter = cameraTranslater;

    public void Move()
    {
        transform.parent = _cameraTransiter.transform.parent;
        transform.LookAt(_cameraTransiter.transform);
        transform.DOLocalMove(_cameraTransiter.transform.localPosition, _time);
        Scale();
        StartCoroutine(CoroutineHelper.WaitTimeAndUse(_timeToEnableMesh, () => this.GetComponent<MeshRenderer>().enabled = false));
    }

    public void Scale() => transform.DOScale(_scale, _time);

}
