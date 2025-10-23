using Assets.Scripts.Traps;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject _targetDestroy;
    [SerializeField] private TrapEffectStarter _trapEffectStart;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out Food food))
        {
            _trapEffectStart.StartEffect();
            gameObject.SetActive(false);
            _targetDestroy.SetActive(true);
        }
    }
}
