using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Starter _starter;
    [SerializeField] private EnemyFallowCase _enemyFallowCase;
    [SerializeField] private List<Enemy> _enemyFollowToStart;
    private List<Enemy> enemies = new List<Enemy>();

    private void OnEnable() => _starter.LevelStart += Follows;

    private void OnDisable() => _starter.LevelStart -= Follows;

    private void Start() => _enemyFollowToStart.ForEach(enemy => enemies.Add(enemy));

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) && enemies.Contains(enemy) == false && enemy.IsDie == false)
        {
            enemy.Follow();
            enemies.Add(enemy);
            _enemyFallowCase.AddEnemy(enemy);
        }
    }
    private void Follows() => _enemyFollowToStart.ForEach(enemy => enemy.Follow());
}
