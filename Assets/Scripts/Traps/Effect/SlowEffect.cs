using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Traps.Effect
{
    internal class SlowEffect : TrapEffectStarter
    {
        [SerializeField] private EnemyFallowCase _enemyFallowCase;
        
        [SerializeField,Min(0)] private float _time;
        [Range(0,1)]
        [SerializeField] private float _percent;

        public override void StartEffect()
        {
            foreach (var enemy in _enemyFallowCase.Enemys)
                enemy.Slow(_percent, _time);
        }

        private void OnValidate() => _enemyFallowCase = FindObjectOfType<EnemyFallowCase>();
    }
}
