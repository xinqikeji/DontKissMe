using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    private const string SHOOT = "Shoot";
    private const string SpeedShoot = nameof(SpeedShoot);

    [SerializeField] private Animator _animator;

    public void Shoot() => _animator.SetTrigger(SHOOT);
    public void SpeedUpShoot(float speed) => _animator.SetFloat(SpeedShoot,speed );
}