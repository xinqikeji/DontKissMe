using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyInfo 
{
    /// <summary>
    /// 能量容量
    /// </summary>
   public int energyVolumes;
    
    /// <summary>
    /// 当前能量数量
    /// </summary>
    
   public int nowEnergyNumber;
    /// <summary>
    /// 是否正在恢复
    /// </summary>
   public bool isRecover;
   
    /// <summary>
    /// 恢复时间间隔，单位 秒
    /// </summary>
    public float recoverTimeDistance;

    /// <summary>
    /// 构造函数初始化
    /// </summary>
    public EnergyInfo()
    {
        energyVolumes = 5;
        nowEnergyNumber = 5;
        isRecover = false;
        recoverTimeDistance = 300;
    }
}
