using UnityEngine;

[System.Serializable]
public class EnemyAnimator
{
    [SerializeField] private Animator _animator;

    public void Run() => _animator.SetTrigger(Params.Run);

    public void SlowRun() => _animator.SetTrigger(Params.SlowRun);

    public void Fall() => _animator.SetTrigger(Params.Fall);

    public void Kiss() => _animator.Play(Params.Kiss);

    public class Params
    {
        public const string Run = nameof(Run);
        public const string SlowRun = nameof(SlowRun);
        public const string Fall = nameof(Fall);
        public const string Kiss = nameof(Kiss);
    }
}

