using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Food[] _foods;
    [SerializeField] private float _timeToShoot;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private Image _aim;
    [SerializeField] private float _force;
    [SerializeField] private float _forceShootY;
    [SerializeField] private float _amplitudeRotate = 75f;
    [SerializeField] private float _shootMoveZ = .2f;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private GameObject _drum;
    [SerializeField] private float _speedDrumRotate;
    [SerializeField] private float _drumRotate;
    [SerializeField] private List<ParticleSystem> _speedParticle;

    private float _currentTime;
    private RaycastHit _raycast;
    private GunAnimator _gunAnimator;
    private const float DefaultSpeed = 2.5f;
    public bool isWandShoot = true;
    private void Start()
    {
        _gunAnimator = GetComponent<GunAnimator>();
    }

    private void Update()
    {
        if (isWandShoot == false)
            return;

        if (Input.GetMouseButton(0))
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _timeToShoot)
            {
                Shoot();
                _currentTime = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentTime = _timeToShoot;
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        _shootEffect.Play();

        _shootEffect.transform.position = _shootPoint[0].position; //??

        if (_drum != null)
            _drum.transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y + _drumRotate, transform.rotation.z), .25f, RotateMode.FastBeyond360).OnComplete(() => _drum.transform.rotation = new Quaternion(0, 0, 0, 0));

        _gunAnimator.Shoot();

        bool isTarget = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _raycast, 100, 1, QueryTriggerInteraction.Ignore);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 direction = _raycast.point - _shootPoint[0].transform.position; //??
        Vector3 force = (direction.normalized * _force + new Vector3(0, _forceShootY, 0)) * 0.01f;

        List<FoodMove> foodMoves = new List<FoodMove>();

        for (int i = 0; i < _shootPoint.Length; i++)
        {
            Food food = Instantiate(_foods[Random.Range(0, _foods.Length - 1)], _shootPoint[i].position, _shootPoint[i].transform.rotation);
            foodMoves.Add(food.GetComponent<FoodMove>());

            if (isTarget)
            {
                foreach (var foodMove in foodMoves)
                {
                    foodMove.AddForce(force);
                }
            }
            else
            {
                foreach (var foodMove in foodMoves)
                {
                    foodMove.Fly();
                }
            }
        }
    }

    public void SetTimeToShoot(float timeToShoot) => _timeToShoot = timeToShoot;

    public float GetTimeToShoot() => _timeToShoot;
    public void StartSpeedAnimation() => _speedParticle.ForEach(particle => particle.Play());
    public void StopSpeedAnimation() => _speedParticle.ForEach(particle => particle.Stop());
    public bool CheckAvailabilityGunAnimator() => _gunAnimator != null;
    public void SetSpeedShoot(float speed) => _gunAnimator.SpeedUpShoot(speed);
    public void SetDefaultSpeed() => _gunAnimator.SpeedUpShoot(DefaultSpeed);
}