using Dreamteck.Splines;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _startSpeedBoost = 1.5f;
    [SerializeField] private float _RandomTargetOffset = 1.5f;
    [SerializeField] private float _distantToReductionsSpeed = 5;
    [SerializeField] private float _distantToLouse = 3;
    [SerializeField] private ImageFiller _imagefiler;

    public ImageFiller ImageFiller => _imagefiler;

    private Vector3 _randomTargetOffset;
    private Coroutine _followCorutine;

    private Car _target;
    private float _startTargetSpeed;
    private float _speed;
    private float _slowIncrease = 1;

    public event UnityAction CaughtUp;

    public NavMeshAgent _navMeshAgent { get; private set; }
    public float Speed
    {
        get => _speed; private set
        {
            if (value < 0)
                Debug.LogAssertion("Speed need > 0");
            _speed = value;
            _navMeshAgent.speed = value;
        }
    }

    private void Awake()
    {
        _randomTargetOffset = Vector3.right * Random.Range(-_RandomTargetOffset, _RandomTargetOffset);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = FindObjectOfType<Car>();
        SplineFollower splineFollower = _target.GetComponent<SplineFollower>();
        _startTargetSpeed = splineFollower.CalculateLength() / splineFollower.followDuration;
    }

    public void Move(Vector3 position) => _navMeshAgent.destination = position;

    public void Stop()
    {
        if (_navMeshAgent.enabled == false)
            return;

        _navMeshAgent.isStopped = true;
        if (_followCorutine != null)
        {
            StopCoroutine(_followCorutine);
        }
        _navMeshAgent.enabled = false;
    }
    public void Follow() => _followCorutine = StartCoroutine(FollowCorutine());

    public void Slow(float percent, float time) => StartCoroutine(SlowCorutine(percent, time));

    private IEnumerator SlowCorutine(float percent, float time)
    {
        _slowIncrease *= percent;
        yield return new WaitForSeconds(time);
        _slowIncrease /= percent;

    }

    public IEnumerator FollowCorutine()
    {
        while (true)
        {
            if (GetDistant() > _distantToReductionsSpeed && GetDistant() != 0)
                Speed = _startTargetSpeed * _startSpeedBoost * _slowIncrease;
            //else
            //    Speed = _startTargetSpeed * _slowIncrease * (GetDistant() / (_distantToReductionsSpeed - 1f));
               

            if (GetDistant() < _distantToReductionsSpeed && GetDistant() != 0)
            {
                Speed = _startTargetSpeed * _slowIncrease;
                if (_imagefiler.IsFullFill == false)
                    _imagefiler.UpdateFilled();
            }

            Move(_target.transform.position + _randomTargetOffset);
            yield return null;
        }
    }

    private float GetDistant() => _navMeshAgent.remainingDistance;
}