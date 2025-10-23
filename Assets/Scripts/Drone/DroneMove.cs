using DG.Tweening;
using UnityEngine;

public class DroneMove : MonoBehaviour
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _startSpeedMoveX;
    [SerializeField] private float _speedMoveX;
    [SerializeField] private float _speedMoveFinish;

    private void Start()
    {
        transform.DOLocalMoveX(-_offsetX, _startSpeedMoveX).OnComplete(() => MoveX()).SetEase(Ease.Linear);
    }

    public void MoveToFinish()
    {
        transform.DOLocalMoveZ(20f, _speedMoveFinish).OnComplete(() => gameObject.SetActive(false));
    }

    private void MoveX()
    {
        transform.DOLocalMoveX(_offsetX, _speedMoveX).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        Invoke(nameof(MoveToFinish), _moveTime);
    }
}