using Dreamteck.Splines;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���������࣬������������� spline ·���ϵ��ƶ��Լ�����¼�����
/// </summary>
[RequireComponent(typeof(SplineFollower))] // ǿ��Ҫ����� SplineFollower ���
public class Car : MonoBehaviour
{
    // �ؿ�����������
    [SerializeField] private Starter _starter;
    // �ؿ�����������
    [SerializeField] private Finisher _finisher;
    // �����ƶ�����������
    [SerializeField] private DirectionMover _directionMover;
    // �������������
    [SerializeField] private FinishCamera _finishCamera;

    // spline�������������
    private SplineFollower _splineFollower;
    // �������е������������
    private Weapon[] _weapons;

    /// <summary>
    /// ��Awake�׶λ�ȡ�����������е��������
    /// </summary>
    public void Awake()
    {
        _weapons = GetComponentsInChildren<Weapon>();
    }

    /// <summary>
    /// ������ʱ���ĸ����¼�
    /// </summary>
    private void OnEnable()
    {
        // ���Ĺؿ���ʼ�¼������������ƶ�
        _starter.LevelStart += Move;
        // ���Ĺؿ�����¼������������ƶ�
        _finisher.FinishLevel += _directionMover.Move;
        // ���Ĺؿ�����¼�������չʾ���������
        _finisher.FinishLevel += _finishCamera.Show;
        // ���Ĺؿ�����¼�������spline������
        _finisher.FinishLevel += FollowerDisable;
        // ���Ĺؿ�����¼���ֹͣ���
        _finisher.FinishLevel += StopShoot;
    }

    /// <summary>
    /// �ڽ���ʱȡ�����ĸ����¼�����ֹ�ڴ�й©
    /// </summary>
    private void OnDisable()
    {
        // ȡ�����Ĺؿ���ʼ�¼�
        _starter.LevelStart -= Move;
        // ȡ�����Ĺؿ�����¼��ĸ��ִ�����
        _finisher.FinishLevel -= _directionMover.Move;
        _finisher.FinishLevel -= _finishCamera.Show;
        _finisher.FinishLevel -= FollowerDisable;
        _finisher.FinishLevel -= StopShoot;
    }

    /// <summary>
    /// ֹͣ�����������
    /// </summary>
    public void StopShoot() => _weapons.ToList().ForEach(weapon => weapon.isWandShoot = false);

    /// <summary>
    /// ��ʼ��spline·���ƶ�
    /// </summary>
    private void Move()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _splineFollower.follow = true; // ���ø���ģʽ
    }

    /// <summary>
    /// ����spline������
    /// </summary>
    private void FollowerDisable() => _splineFollower.enabled = false;
}
