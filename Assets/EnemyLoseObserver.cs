using UnityEngine;

[RequireComponent(typeof(Finisher))]
public class EnemyLoseObserver : MonoBehaviour
{
    [SerializeField] private EnemyFallowCase _enemyFallow;
    [SerializeField] private CameraTranslater _cameraTranslater;
    [SerializeField] private Car _car;

    private Finisher _finisher;
    private bool _isEnemyLose;

    private void Awake()
    {
        _finisher = GetComponent<Finisher>();
    }
    private void OnEnable()
    {
        _enemyFallow.EnemyAdded += Subscribe;
        _enemyFallow.EnemyRemoved += UnSubscribe;
    }
    private void OnDisable()
    {
        _enemyFallow.EnemyAdded -= Subscribe;
        _enemyFallow.EnemyRemoved -= UnSubscribe;
    }

    private void UnSubscribe(Enemy enemy) => enemy.EnemyMover.ImageFiller.ImageFilledOn -= CameraTransit;

    private void Subscribe(Enemy enemy) => enemy.EnemyMover.ImageFiller.ImageFilledOn += CameraTransit;

    private void CameraTransit(Enemy enemy)
    {
        if (_isEnemyLose == false && _finisher.IsFinish == false)
        {
            _cameraTranslater.SelectTarget(enemy);
            _cameraTranslater.CameraTransit();
            _car.StopShoot();
            _isEnemyLose = true;
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_cameraTranslater == null || _enemyFallow == null || _car == null)
            Debug.LogAssertion("Aaaaaa нужна крипт а его нет.");
    }
#endif
}
