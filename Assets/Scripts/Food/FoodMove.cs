using UnityEngine;

public class FoodMove : MonoBehaviour
{
    private float _force = 50f;
    private readonly float _minForceExplosion = 8f;
    private readonly float _mazForceExplosion = 11f;
    private Rigidbody _rigidbody;
    private readonly float _timeToDestroy = 5f;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void Start() => Invoke(nameof(Die), _timeToDestroy);

    public void Explode(Vector3 direction) => _rigidbody.AddForce(direction * Random.Range(_minForceExplosion, _mazForceExplosion), ForceMode.Impulse);

    public void SetForce(float force) => _force = force;

    public void AddForce(Vector3 velocity) => _rigidbody.AddForce(velocity * _force);

    public void Fly() => _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);

    private void Die() => Destroy(gameObject);
}