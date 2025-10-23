using DG.Tweening;
using UnityEngine;

public class DirectionMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _finishTarget;

    public void Move()
    {
        float distance = Vector3.Distance(transform.position, _finishTarget.position);
        transform.DOLocalMove(_finishTarget.position, distance / _speed);
    }
}
