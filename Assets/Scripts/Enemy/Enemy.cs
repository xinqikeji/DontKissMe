using Assets.Scripts;
using Assets.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _rose;
    [SerializeField] private SizeChanger _sizeChanger;
    [SerializeField] private ParticleSystem _kissEffect;
    [SerializeField] private IceShower _iceShower;
    [SerializeField] private float _slowInShoot = 0.9f;

    protected Coroutine _slowEffectCorutine;
    private bool isMaxWeight;
    public bool IsDie { get; private set; } = false;

    public event UnityAction<Enemy> Died;
    public float Weight { get; private set; } = 0;
    [field: SerializeField] public EatEater EatEater { get; private set; }
    [field: SerializeField] public LipsMover LipsMover { get; private set; }
    [field: SerializeField] public EnemyAnimator Animator { get; private set; }
    [field: SerializeField] public EnemyMover EnemyMover { get; private set; }
    [field: SerializeField] protected float TimeEat { get; private set; } = 2;

    protected virtual void Start() => _sizeChanger.Init(transform);

    public void Follow()
    {
        _rose.SetActive(true);
        _kissEffect.Play();
        EnemyMover.Follow();
        Animator.Run();
    }

    public void StopFollow() => EnemyMover.Stop();

    public void TakeDamage(Food food)
    {
        EatEater.SetCurrentEats(food);
        _rose.SetActive(false);

        if (Weight == 1 && IsDie == false)
            Die();
        if (isMaxWeight == false)
            AddWeight01(food.Weight);
    }

    public void Eat() => EatEater.StartEat(TimeEat);

    public void StopEat() => EatEater.StopEat();

    public void Slow(float percent, float time)
    {
        EnemyMover.Slow(percent, time);
        _iceShower.ChangeSize01(Weight);
        _iceShower.ShowIce();
        StartCoroutine(CoroutineHelper.WaitTimeAndUse(time, _iceShower.StopShowIce));
    }

    protected virtual void UseOrChangeWeight() => SlowAfterShoot();

    protected virtual void Die()
    {
        StopFollow();
        Animator.Fall();
        StopEat();
        if (_slowEffectCorutine != null)
            StopCoroutine(_slowEffectCorutine);
        EatEater.Show();
        Died.Invoke(this);
        IsDie = true;
    }

    private void AddWeight01(float value)
    {
        Weight += Mathf.Clamp01(value);
        if (Weight >= 1)
        {
            Weight = 1;
            isMaxWeight = true;
        }
        _sizeChanger.Change(Weight);
        UseOrChangeWeight();
    }

    private void SlowAfterShoot()
    {
        EnemyMover.Slow(_slowInShoot, TimeEat);
        if (_slowEffectCorutine != null)
            StopCoroutine(_slowEffectCorutine);
        else
            Animator.SlowRun();
        StopEat();
        Eat();
        _slowEffectCorutine = StartCoroutine(CoroutineHelper.WaitTimeAndUse(TimeEat, () => Animator.Run(), () => StopEat()));
    }
}