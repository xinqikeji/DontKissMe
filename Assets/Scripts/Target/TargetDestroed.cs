using System.Collections.Generic;
using UnityEngine;

public class TargetDestroed : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _rigidbodies;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private DroneMove _droneMove;

    private float _force = 150f;

    private void Start()
    {
        transform.parent = null;
        //_droneMove.MoveToFinish();

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.AddExplosionForce(_force, transform.position, 50f);
        }

        Invoke(nameof(DestroyRigidbodies), _timeToDestroy);
    }

    private void DestroyRigidbodies()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.gameObject.SetActive(false);
        }
    }
}