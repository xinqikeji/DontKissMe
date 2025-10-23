using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Enemy.Eat
{
    [System.Serializable]
    public class HotDogEater : Eater
    {
        [SerializeField] private AnimationCurve _animateCureEat;
        [SerializeField] private ParticleSystem[] _eatParticles;
        [SerializeField] private GameObject _eats;
        [SerializeField] private Transform _lastEatPosition;

        private Quaternion _startRotation;
        private Vector3 _startPosition;

        private Sequence _moveAnimation;
        [field: SerializeField] public override Food Food { get; protected set; }

        public override void Init()
        {
            _startPosition = _eats.transform.localPosition;
            _startRotation = _eats.transform.localRotation;
        }

        public override void StartEat(float time)
        {
            foreach (ParticleSystem particle in _eatParticles)
                particle.Play();

            _eats.SetActive(true);
            _eats.transform.localPosition = _startPosition;
            _eats.transform.localRotation = _startRotation;

            if (_moveAnimation != null)
                _moveAnimation.Kill();
            AnimateMove(time);
        }

        public override void StopEat()
        {
            foreach (ParticleSystem particle in _eatParticles)
                particle.Stop();
            _eats.SetActive(false);
            _moveAnimation.Kill();
        }

        public override void Show()
        {
            _eats.transform.localPosition = _startPosition;
            _eats.SetActive(true);
        }

        public override void Hide() => _eats.SetActive(false);

        private void AnimateMove(float time)
        {
            _moveAnimation = DOTween.Sequence();
            _moveAnimation.Insert(0, _eats.transform.DOLocalMove(_lastEatPosition.localPosition, time));
            _moveAnimation.Insert(0, _eats.transform.DOLocalRotate(_lastEatPosition.localRotation.eulerAngles, time));
            _moveAnimation.SetEase(_animateCureEat);
        }
    }
}
