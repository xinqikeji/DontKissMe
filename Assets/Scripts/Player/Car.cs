using Dreamteck.Splines;
using System.Linq;
using UnityEngine;

/// <summary>
/// 汽车控制类，负责管理汽车在 spline 路径上的移动以及相关事件处理
/// </summary>
[RequireComponent(typeof(SplineFollower))] // 强制要求挂载 SplineFollower 组件
public class Car : MonoBehaviour
{
    // 关卡启动器引用
    [SerializeField] private Starter _starter;
    // 关卡结束器引用
    [SerializeField] private Finisher _finisher;
    // 方向移动控制器引用
    [SerializeField] private DirectionMover _directionMover;
    // 结束摄像机引用
    [SerializeField] private FinishCamera _finishCamera;

    // spline跟随器组件引用
    private SplineFollower _splineFollower;
    // 子物体中的所有武器组件
    private Weapon[] _weapons;

    /// <summary>
    /// 在Awake阶段获取子物体中所有的武器组件
    /// </summary>
    public void Awake()
    {
        _weapons = GetComponentsInChildren<Weapon>();
    }

    /// <summary>
    /// 在启用时订阅各种事件
    /// </summary>
    private void OnEnable()
    {
        // 订阅关卡开始事件，触发汽车移动
        _starter.LevelStart += Move;
        // 订阅关卡完成事件，触发方向移动
        _finisher.FinishLevel += _directionMover.Move;
        // 订阅关卡完成事件，触发展示结束摄像机
        _finisher.FinishLevel += _finishCamera.Show;
        // 订阅关卡完成事件，禁用spline跟随器
        _finisher.FinishLevel += FollowerDisable;
        // 订阅关卡完成事件，停止射击
        _finisher.FinishLevel += StopShoot;
    }

    /// <summary>
    /// 在禁用时取消订阅各种事件，防止内存泄漏
    /// </summary>
    private void OnDisable()
    {
        // 取消订阅关卡开始事件
        _starter.LevelStart -= Move;
        // 取消订阅关卡完成事件的各种处理函数
        _finisher.FinishLevel -= _directionMover.Move;
        _finisher.FinishLevel -= _finishCamera.Show;
        _finisher.FinishLevel -= FollowerDisable;
        _finisher.FinishLevel -= StopShoot;
    }

    /// <summary>
    /// 停止所有武器射击
    /// </summary>
    public void StopShoot() => _weapons.ToList().ForEach(weapon => weapon.isWandShoot = false);

    /// <summary>
    /// 开始沿spline路径移动
    /// </summary>
    private void Move()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _splineFollower.follow = true; // 启用跟随模式
    }

    /// <summary>
    /// 禁用spline跟随器
    /// </summary>
    private void FollowerDisable() => _splineFollower.enabled = false;
}
