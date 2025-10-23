using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Pizza : Food
{
    [SerializeField, Min(0)] private int _numberBounces;
    [SerializeField] private float _radiusBounces;
    [SerializeField, Min(0)] private float _timeToFly;
    private List<Enemy> _enemies = new List<Enemy>();
    private Sequence _moveFoodTween;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy follower) && _enemies.Contains(follower) == false && _enemies.Count < _numberBounces)
        {
            Attack(follower);
        }
        if (_enemies.Count >= _numberBounces)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy follower) && _enemies.Contains(follower) == false && _enemies.Count < _numberBounces)
        {
            this.GetComponent<MeshCollider>().isTrigger = true;
            Attack(follower);
        }
        if (_enemies.Count >= _numberBounces)
            Destroy(gameObject);
    }

    private void Attack(Enemy follower)
    {
        if (_moveFoodTween != null)
            _moveFoodTween.Kill();
        _enemies.Add(follower);
        AttackEnemy(follower);
        var colliderInRaduisBonces = Physics.OverlapSphere(transform.position, _radiusBounces);
        var enemys = colliderInRaduisBonces
            .Where(x => x.gameObject.GetComponent<Enemy>() != null)
            .Select(x => x.GetComponent<Enemy>())
            .Where(enemy => (_enemies.Contains(enemy) == false) && enemy.IsDie == false)
            .OrderBy(x => (x.gameObject.transform.position - transform.position).magnitude)
            .ToList();

        if (enemys.Count != 0)
        {
            transform.parent = enemys[0].transform;
            var coliderPosition = enemys[0].GetComponent<CapsuleCollider>().center;
            transform.LookAt(enemys[0].transform.position + coliderPosition);
            _moveFoodTween = DOTween.Sequence();

            _moveFoodTween.Insert(0, transform.DOLocalMove(Vector3.zero + coliderPosition, _timeToFly));
            _moveFoodTween.Insert(0, transform.DOLookAt(enemys[0].transform.position + coliderPosition, _timeToFly));
            _moveFoodTween.OnComplete(() => transform.parent = enemys[0].transform.parent);
            _moveFoodTween.SetEase(Ease.Linear);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

