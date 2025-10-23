using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Traps.Effect
{
    public class SpeedEffect : TrapEffectStarter
    {
        [SerializeField] private Weapon[] _weapons;
        [Range(0, 1)]
        [SerializeField,] private float _speedBostPercent;
        [SerializeField] private float _time;
        private float _speedbostSpeed = 4;

        public override void StartEffect()
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i].gameObject.activeInHierarchy == false)
                    continue;
                StartCoroutine(ResetStartSpeed(_weapons[i], _weapons[i].GetTimeToShoot()));
            }
        }

        private IEnumerator ResetStartSpeed(Weapon weapon, float _startSpeed)
        {
            SetTimeToShoots(weapon, _speedBostPercent * _startSpeed);
            weapon.StartSpeedAnimation();
            weapon.SetSpeedShoot(_speedbostSpeed);
            yield return new WaitForSeconds(_time);
            SetTimeToShoots(weapon, _startSpeed);
            weapon.StopSpeedAnimation();
            weapon.SetDefaultSpeed();
        }

        public void SetTimeToShoots(Weapon weapons, float timeToShoot) => weapons.SetTimeToShoot(timeToShoot);
    }
}
