using Assets.Scripts;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraTranslater : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private float _waitLips;

    public event UnityAction CameraTransited;
    private const int CameraPriority = 1000000000;

    public void SelectTarget(Enemy enemy)
    {
        _cinemachineVirtualCamera.LookAt = enemy.transform;
        enemy.LipsMover.Init(this);
        StartCoroutine(CoroutineHelper.WaitTimeAndUse(_waitLips, () => enemy.LipsMover.Move(), () => enemy.Animator.Kiss()));

    }

    public void CameraTransit()
    {
        _cinemachineVirtualCamera.Priority = CameraPriority;
        CameraTransited?.Invoke();
    }
}
