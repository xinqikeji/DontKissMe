using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Traps.Effect
{
    public class DamageEffect : TrapEffectStarter
    {
        [SerializeField] private EnemyFallowCase _enemyFallowCase;
        [SerializeField] private Food _food;
        [Range(0, 1)]
        [SerializeField] private float _weight;

        public override void StartEffect()
        {
            foreach (var enemy in _enemyFallowCase.Enemys.ToList())
            {
                enemy.TakeDamage(_food);
            }
        }
        private void OnValidate() => _enemyFallowCase = FindObjectOfType<EnemyFallowCase>();
    }
}
