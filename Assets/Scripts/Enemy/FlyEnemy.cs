using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FlyEnemy : Enemy
{
    [SerializeField] private float _descentTime = 1;
    [SerializeField] private float _minDefaultY = 0.5f;
    [SerializeField] private float _dieHight = -0.5f;

    private float _startHight;
    private bool isCoroutineWork;
    protected override void Start()
    {
        base.Start();
        _startHight = transform.localPosition.y;
    }

    protected override void UseOrChangeWeight() => ChangeHeight();

    protected override void Die()
    {
        base.Die();
        transform.DOLocalMoveY(_dieHight, _descentTime);
    }

    private void ChangeHeight()
    {
        float invertWeight = 1 - Weight;
        transform.DOLocalMoveY(_startHight * invertWeight + Weight * _minDefaultY, _descentTime);
        if (isCoroutineWork)
            StopCoroutine(_slowEffectCorutine);
        else
            Animator.SlowRun();
        Eat();
        _slowEffectCorutine = StartCoroutine(ReternStartSpeed(TimeEat));
    }

    private IEnumerator ReternStartSpeed(float time)
    {
        isCoroutineWork = true;
        yield return new WaitForSeconds(time);
        Animator.Run();
        StopEat();
        isCoroutineWork = false;
    }
}
