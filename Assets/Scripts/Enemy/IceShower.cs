using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    internal class IceShower : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _IceParticles;
        [SerializeField] private GameObject _IсeBlock;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _time;
        private const float _minIceSize = 40;
        private const float _maxIceSize = 80;

        public void ChangeSize01(float size)
        {
            Mathf.Clamp01(size);
            var tempSize = (_maxIceSize - _minIceSize) * size + _minIceSize;
            IncreaseAnimatios(tempSize);
        }

        public void ShowIce()
        {
            foreach (ParticleSystem particle in _IceParticles)
                particle.Play();

            _IсeBlock.SetActive(true);
        }

        public void StopShowIce()
        {
            foreach (ParticleSystem particle in _IceParticles)
                particle.Stop();
            _IсeBlock.SetActive(false);
        }

        private void IncreaseAnimatios(float scale)
        {
            _IсeBlock.transform.DOScaleX(scale, _time).SetEase(_animationCurve);
            _IсeBlock.transform.DOScaleZ(scale, _time).SetEase(_animationCurve);
        }

        public void Show() => _IсeBlock.SetActive(true);

        public void Hide() => _IсeBlock.SetActive(false);
    }
}
