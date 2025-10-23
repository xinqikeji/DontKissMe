using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFallowCase : MonoBehaviour
{
    private List<Enemy> _enemys = new List<Enemy>();
    public IReadOnlyList<Enemy> Enemys => _enemys;

    public event UnityAction<Enemy> EnemyAdded;
    public event UnityAction<Enemy> EnemyRemoved;

    public void AddEnemy(Enemy enemy)
    {
        if (_enemys.Contains(enemy))
            Debug.LogAssertion(enemy + "Есть в списке");
        else
            _enemys.Add(enemy);
        Subscribe(enemy);
        EnemyAdded?.Invoke(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_enemys.Contains(enemy))
            _enemys.Remove(enemy);
        else
            Debug.LogAssertion(enemy + "Нет в списке");

        EnemyRemoved?.Invoke(enemy);
    }

    private void Subscribe(Enemy enemy) => enemy.Died += RemoveEnemy;
}
